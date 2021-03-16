
namespace Computer_house.DataBase.Entities
{
    class GPU
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Interface_ID { get; set; }
        public string Manufacturer { get; set; }
        public bool Overclocking { get; set; }
        public int GPU_type_ID { get; set; }
        public int Bus_width { get; set; }
        public string DirectX { get; set; }
        public bool SLI_support { get; set; }
        public string External_interfaces { get; set; }
        public int Power_type_ID { get; set; }
        public int Coolers_count { get; set; }
        public int Cooling_system_thikness { get; set; }
    }
}
