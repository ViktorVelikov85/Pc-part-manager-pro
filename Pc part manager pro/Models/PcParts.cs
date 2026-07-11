namespace Pc_part_manager_pro.Models.PcParts
{
    public class Cpu : ComputerPart
    {
        public int Cores { get; set; }
        public string Socket { get; set; }
        public decimal ClockGhz { get; set; }
        public int TdpWatts { get; set; }

        public override string GetSpecifications()
        {
            return $"CPU: {Cores} Cores, {ClockGhz}GHz, Socket {Socket}, TDP: {TdpWatts}W";
        }
    }

    public class Gpu : ComputerPart
    {
        public int VramSizeGb { get; set; }
        public string MemoryType { get; set; }
        public int ClockMhz { get; set; }

        public override string GetSpecifications()
        {
            return $"GPU: {VramSizeGb}GB {MemoryType}, Core Clock: {ClockMhz}MHz";
        }
    }

    public class Ram : ComputerPart
    {
        public int CapacityGb { get; set; }
        public int SpeedMhz { get; set; }
        public string Generation { get; set; }
        public string CasLatency { get; set; }

        public override string GetSpecifications()
        {
            return $"RAM: {CapacityGb}GB {Generation} @ {SpeedMhz}MHz, Latency: {CasLatency}";
        }
    }

    public class Motherboard : ComputerPart
    {
        public string Chipset { get; set; }
        public string FormFactor { get; set; }
        public string Socket { get; set; }
        public string SupportedRamGen { get; set; }

        public override string GetSpecifications()
        {
            return $"Motherboard: Chipset {Chipset}, Form: {FormFactor}, Socket {Socket}, Supports {SupportedRamGen}";
        }
    }

    public class Ssd : ComputerPart
    {
        public int CapacityGb { get; set; }
        public string FormFactor { get; set; }
        public int ReadSpeedMb { get; set; }

        public override string GetSpecifications()
        {
            return $"SSD: {CapacityGb}GB, Form: {FormFactor}, Read Speed: {ReadSpeedMb}MB/s";
        }
    }
}