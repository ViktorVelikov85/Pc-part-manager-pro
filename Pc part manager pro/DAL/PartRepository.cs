using MySql.Data.MySqlClient;
using Pc_part_manager_pro.Models;
using Pc_part_manager_pro.Models.PcParts;
using System;
using System.Collections.Generic;
using System.Data;

namespace Pc_part_manager_pro.DAL
{
    public class PartRepository
    {
        // ==========================================
        // 1. CREATE (ДОБАВЯНЕ С ТРАНЗАКЦИЯ)
        // ==========================================
        public void Add(ComputerPart part)
        {
            if (part == null) throw new ArgumentNullException(nameof(part));
            ValidatePart(part);

            using (var conn = DbConfig.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Етап 1: Запис в основната таблица
                        string baseSql = @"INSERT INTO computer_parts (name, manufacturer, price, quantity, category_id, part_type) 
                                           VALUES (@name, @manufacturer, @price, @quantity, @categoryId, @partType);";

                        int insertedId = 0;
                        using (var cmd = new MySqlCommand(baseSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@name", part.Name);
                            cmd.Parameters.AddWithValue("@manufacturer", part.Manufacturer);
                            cmd.Parameters.AddWithValue("@price", part.Price);
                            cmd.Parameters.AddWithValue("@quantity", part.Quantity);
                            cmd.Parameters.AddWithValue("@categoryId", part.PartCategory.Id);
                            cmd.Parameters.AddWithValue("@partType", GetPartTypeString(part));
                            cmd.ExecuteNonQuery();

                            // Взимаме автоматично генерираното ID
                            cmd.CommandText = "SELECT LAST_INSERT_ID();";
                            insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                            part.Id = insertedId;
                        }

                        // Етап 2: Запис в специфичната таблица според типа на обекта
                        string specSql = GetInsertSpecSql(part);
                        using (var cmd = new MySqlCommand(specSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@partId", insertedId);
                            AddSpecParameters(cmd, part);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Всичко е наред, записваме окончателно
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Грешка! Връщаме базата в изходно състояние
                        throw;
                    }
                }
            }
        }

        // ==========================================
        // 2. READ (ИЗВЕЖДАНЕ ЧРЕЗ LEFT JOIN)
        // ==========================================
        private string GetBaseSelectQuery()
        {
            return @"SELECT p.*, c.name AS category_name,
                            cpu.cores, cpu.socket AS cpu_socket, cpu.clock_ghz, cpu.tdp_watts,
                            gpu.vram_size_gb, gpu.memory_type, gpu.clock_mhz,
                            ram.capacity_gb AS ram_capacity, ram.speed_mhz AS ram_speed, ram.generation AS ram_gen, ram.cas_latency,
                            mb.chipset, mb.form_factor AS mb_form, mb.socket AS mb_socket, mb.supported_ram_gen,
                            ssd.capacity_gb AS ssd_capacity, ssd.form_factor AS ssd_form, ssd.read_speed_mb
                     FROM computer_parts p
                     JOIN categories c ON p.category_id = c.id
                     LEFT JOIN part_cpus cpu ON p.id = cpu.part_id
                     LEFT JOIN part_gpus gpu ON p.id = gpu.part_id
                     LEFT JOIN part_rams ram ON p.id = ram.part_id
                     LEFT JOIN part_motherboards mb ON p.id = mb.part_id
                     LEFT JOIN part_ssds ssd ON p.id = ssd.part_id";
        }

        public List<ComputerPart> GetAll()
        {
            List<ComputerPart> list = new List<ComputerPart>();
            DataTable table = DbConfig.GetTable(GetBaseSelectQuery());

            foreach (DataRow row in table.Rows)
            {
                list.Add(MapRowToPart(row));
            }
            return list;
        }

        // ==========================================
        // 3. UPDATE (РЕДАКТИРАНЕ С ТРАНЗАКЦИЯ)
        // ==========================================
        public void Update(ComputerPart part)
        {
            if (part == null) throw new ArgumentNullException(nameof(part));
            ValidatePart(part);

            using (var conn = DbConfig.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Редакция в основната таблица
                        string baseSql = @"UPDATE computer_parts 
                                           SET name = @name, manufacturer = @manufacturer, price = @price, quantity = @quantity 
                                           WHERE id = @id;";
                        using (var cmd = new MySqlCommand(baseSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", part.Id);
                            cmd.Parameters.AddWithValue("@name", part.Name);
                            cmd.Parameters.AddWithValue("@manufacturer", part.Manufacturer);
                            cmd.Parameters.AddWithValue("@price", part.Price);
                            cmd.Parameters.AddWithValue("@quantity", part.Quantity);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Редакция в специфичната таблица
                        string specSql = GetUpdateSpecSql(part);
                        using (var cmd = new MySqlCommand(specSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@partId", part.Id);
                            AddSpecParameters(cmd, part);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ==========================================
        // 4. DELETE (ИЗТРИВАНЕ - MariaDB CASCADE се грижи за останалото)
        // ==========================================
        public void Delete(int id)
        {
            string sql = "DELETE FROM computer_parts WHERE id = @id;";
            using (var conn = DbConfig.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ==========================================
        // 5. ТЪРСЕНЕ И ФИЛТРИРАНЕ
        // ==========================================
        public List<ComputerPart> Search(string keyword)
        {
            List<ComputerPart> list = new List<ComputerPart>();
            string sql = GetBaseSelectQuery() + " WHERE p.name LIKE @key OR p.manufacturer LIKE @key;";
            MySqlParameter[] ps = { new MySqlParameter("@key", $"%{keyword}%") };

            DataTable table = DbConfig.GetTable(sql, ps);
            foreach (DataRow row in table.Rows) list.Add(MapRowToPart(row));
            return list;
        }

        public List<ComputerPart> FilterByCategory(int categoryId)
        {
            List<ComputerPart> list = new List<ComputerPart>();
            string sql = GetBaseSelectQuery() + " WHERE p.category_id = @catId;";
            MySqlParameter[] ps = { new MySqlParameter("@catId", categoryId) };

            DataTable table = DbConfig.GetTable(sql, ps);
            foreach (DataRow row in table.Rows) list.Add(MapRowToPart(row));
            return list;
        }

        // ==========================================
        // 🛠️ ПОМОЩНИ ОПЕРАЦИИ И МАПВАНЕ (ПОЛИМОРФИЗЪМ)
        // ==========================================
        private ComputerPart MapRowToPart(DataRow row)
        {
            string type = row["part_type"].ToString();
            ComputerPart part;

            switch (type)
            {
                case "CPU":
                    part = new Cpu
                    {
                        Cores = Convert.ToInt32(row["cores"]),
                        Socket = row["cpu_socket"].ToString(),
                        ClockGhz = Convert.ToDecimal(row["clock_ghz"]),
                        TdpWatts = Convert.ToInt32(row["tdp_watts"])
                    };
                    break;
                case "GPU":
                    part = new Gpu
                    {
                        VramSizeGb = Convert.ToInt32(row["vram_size_gb"]),
                        MemoryType = row["memory_type"].ToString(),
                        ClockMhz = Convert.ToInt32(row["clock_mhz"])
                    };
                    break;
                case "RAM":
                    part = new Ram
                    {
                        CapacityGb = Convert.ToInt32(row["ram_capacity"]),
                        SpeedMhz = Convert.ToInt32(row["ram_speed"]),
                        Generation = row["ram_gen"].ToString(),
                        CasLatency = row["cas_latency"].ToString()
                    };
                    break;
                case "Motherboard":
                    part = new Motherboard
                    {
                        Chipset = row["chipset"].ToString(),
                        FormFactor = row["mb_form"].ToString(),
                        Socket = row["mb_socket"].ToString(),
                        SupportedRamGen = row["supported_ram_gen"].ToString()
                    };
                    break;
                case "SSD":
                    part = new Ssd
                    {
                        CapacityGb = Convert.ToInt32(row["ssd_capacity"]),
                        FormFactor = row["ssd_form"].ToString(),
                        ReadSpeedMb = Convert.ToInt32(row["read_speed_mb"])
                    };
                    break;
                default:
                    throw new Exception("Непознат тип хардуер!");
            }

            // Общи свойства
            part.Id = Convert.ToInt32(row["id"]);
            part.Name = row["name"].ToString();
            part.Manufacturer = row["manufacturer"].ToString();
            part.Price = Convert.ToDecimal(row["price"]);
            part.Quantity = Convert.ToInt32(row["quantity"]);
            part.PartCategory.Id = Convert.ToInt32(row["category_id"]);
            part.PartCategory.Name = row["category_name"].ToString();

            return part;
        }

        private string GetPartTypeString(ComputerPart part)
        {
            if (part is Cpu) return "CPU";
            if (part is Gpu) return "GPU";
            if (part is Ram) return "RAM";
            if (part is Motherboard) return "Motherboard";
            if (part is Ssd) return "SSD";
            return "UNKNOWN";
        }

        private string GetInsertSpecSql(ComputerPart part)
        {
            if (part is Cpu) return "INSERT INTO part_cpus (part_id, cores, socket, clock_ghz, tdp_watts) VALUES (@partId, @cores, @socket, @clock, @tdp);";
            if (part is Gpu) return "INSERT INTO part_gpus (part_id, vram_size_gb, memory_type, clock_mhz) VALUES (@partId, @vram, @memType, @clock);";
            if (part is Ram) return "INSERT INTO part_rams (part_id, capacity_gb, speed_mhz, generation, cas_latency) VALUES (@partId, @cap, @speed, @gen, @cas);";
            if (part is Motherboard) return "INSERT INTO part_motherboards (part_id, chipset, form_factor, socket, supported_ram_gen) VALUES (@partId, @chipset, @form, @socket, @ramGen);";
            if (part is Ssd) return "INSERT INTO part_ssds (part_id, capacity_gb, form_factor, read_speed_mb) VALUES (@partId, @cap, @form, @readSpeed);";
            throw new Exception("Невалиден тип обект за инсерт.");
        }

        private string GetUpdateSpecSql(ComputerPart part)
        {
            if (part is Cpu) return "UPDATE part_cpus SET cores = @cores, socket = @socket, clock_ghz = @clock, tdp_watts = @tdp WHERE part_id = @partId;";
            if (part is Gpu) return "UPDATE part_gpus SET vram_size_gb = @vram, memory_type = @memType, clock_mhz = @clock WHERE part_id = @partId;";
            if (part is Ram) return "UPDATE part_rams SET capacity_gb = @cap, speed_mhz = @speed, generation = @gen, cas_latency = @cas WHERE part_id = @partId;";
            if (part is Motherboard) return "UPDATE part_motherboards SET chipset = @chipset, form_factor = @form, socket = @socket, supported_ram_gen = @ramGen WHERE part_id = @partId;";
            if (part is Ssd) return "UPDATE part_ssds SET capacity_gb = @cap, form_factor = @form, read_speed_mb = @readSpeed WHERE part_id = @partId;";
            throw new Exception("Невалиден тип обект за редакция.");
        }

        private void AddSpecParameters(MySqlCommand cmd, ComputerPart part)
        {
            if (part is Cpu cpu)
            {
                cmd.Parameters.AddWithValue("@cores", cpu.Cores);
                cmd.Parameters.AddWithValue("@socket", cpu.Socket);
                cmd.Parameters.AddWithValue("@clock", cpu.ClockGhz);
                cmd.Parameters.AddWithValue("@tdp", cpu.TdpWatts);
            }
            else if (part is Gpu gpu)
            {
                cmd.Parameters.AddWithValue("@vram", gpu.VramSizeGb);
                cmd.Parameters.AddWithValue("@memType", gpu.MemoryType);
                cmd.Parameters.AddWithValue("@clock", gpu.ClockMhz);
            }
            else if (part is Ram ram)
            {
                cmd.Parameters.AddWithValue("@cap", ram.CapacityGb);
                cmd.Parameters.AddWithValue("@speed", ram.SpeedMhz);
                cmd.Parameters.AddWithValue("@gen", ram.Generation);
                cmd.Parameters.AddWithValue("@cas", ram.CasLatency);
            }
            else if (part is Motherboard mb)
            {
                cmd.Parameters.AddWithValue("@chipset", mb.Chipset);
                cmd.Parameters.AddWithValue("@form", mb.FormFactor);
                cmd.Parameters.AddWithValue("@socket", mb.Socket);
                cmd.Parameters.AddWithValue("@ramGen", mb.SupportedRamGen);
            }
            else if (part is Ssd ssd)
            {
                cmd.Parameters.AddWithValue("@cap", ssd.CapacityGb);
                cmd.Parameters.AddWithValue("@form", ssd.FormFactor);
                cmd.Parameters.AddWithValue("@readSpeed", ssd.ReadSpeedMb);
            }
        }

        private void ValidatePart(ComputerPart part)
        {
            if (string.IsNullOrWhiteSpace(part.Name)) throw new ArgumentException("Името не може да е празно!");
            if (string.IsNullOrWhiteSpace(part.Manufacturer)) throw new ArgumentException("Производителят не може да е празен!");
            if (part.Price <= 0) throw new ArgumentException("Цената трябва да е над 0 лв.!");
            if (part.Quantity < 0) throw new ArgumentException("Количеството не може да бъде отрицателно!");
        }
    }
}