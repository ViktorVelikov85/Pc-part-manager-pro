using MySql.Data.MySqlClient;
using System.Data;

namespace Pc_part_manager_pro.DAL
{
    public static class DbConfig
    {
        public static string ConnString = "server=localhost;database=PcPartsProDb;port=3306;uid=root;pwd=;charset=utf8;";

        public static MySqlConnection GetConnection() => new MySqlConnection(ConnString);

        // Помощен метод за SELECT (Връща DataTable, перфектен за WinForms Grid)
        public static DataTable GetTable(string sql, MySqlParameter[] parameters = null)
        {
            DataTable table = new DataTable();
            using (var conn = GetConnection())
            {
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }
    }
}