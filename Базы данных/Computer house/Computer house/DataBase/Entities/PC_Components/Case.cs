
namespace Computer_house.DataBase.Entities
{
    class Case
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Power_supply_unit { get; set; }
        public int Form_factor_ID { get; set; }
        public bool Gaming { get; set; }
        public string Material { get; set; }
        public string Compatible_motherboard { get; set; }
        public string PSU_position { get; set; }
        public string Cooling_type { get; set; }
        public bool Cooler_in_set { get; set; }
        public bool Water_cooling_support { get; set; }
        public bool Sound_isolation { get; set; }
        public int Storage_sections_count { get; set; }
        public int Expansion_slots_count { get; set; }
        public bool Dust_filter { get; set; }
        public int Max_GPU_length { get; set; }
        public int Max_CPU_cooler_height { get; set; }
        public int Max_PSU_length { get; set; }
        public float Weight { get; set; }
    }
}
