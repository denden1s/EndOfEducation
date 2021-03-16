
namespace Computer_house.DataBase.Entities.PC_Components
{
    class RAM
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Kit { get; set; }
        public int RAM_type_ID { get; set; }
        public int RAM_frequency_ID { get; set; }
        public float Voltage { get; set; }
        public bool XMP_profile { get; set; }
        public bool Cooling { get; set; }
        public bool Low_profile_module { get; set; }
    }
}
