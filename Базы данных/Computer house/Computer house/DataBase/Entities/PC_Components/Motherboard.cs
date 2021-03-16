
namespace Computer_house.DataBase.Entities.PC_Components
{
    class Motherboard
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Supported_CPU { get; set; }
        public int Socket_ID { get; set; }
        public int Chipset_ID { get; set; }
        public int Form_factor_ID { get; set; }
        public int RAM_type_ID { get; set; }
        public int Count_of_memory_slots { get; set; }
        public int RAM_chanels_ID { get; set; }
        public string Expansion_slots { get; set; }
        public string Storage_interfaces { get; set; }
        public bool SLI_support { get; set; }
        public bool Integrated_graphic { get; set; }
        public string Connectors { get; set; }
    }
}
