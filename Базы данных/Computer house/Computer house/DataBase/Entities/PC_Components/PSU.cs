
namespace Computer_house.DataBase.Entities.PC_Components
{
    class PSU
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string PSU_standart { get; set; }
        public int Line_plus_twelve_V_count { get; set; }
        public int Max_amperage_on_line_plus_twelve { get; set; }
        public int Efficiency { get; set; }
        public int Modularity { get; set; }
        public int Power_motherboard_type_ID { get; set; }
        public string Power_CPU { get; set; }
        public string Power_PCIe { get; set; }
        public string Power_IDE { get; set; }
        public int Sata_power_count { get; set; }
        public bool Power_USB { get; set; }
    }
}
