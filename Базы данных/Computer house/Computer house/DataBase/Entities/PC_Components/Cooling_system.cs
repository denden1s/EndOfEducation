
namespace Computer_house.DataBase.Entities
{
    class Cooling_system
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Supported_sockets { get; set; }
        public int Count_of_heat_pipes { get; set; }
        public bool Evaporation_chamber { get; set; }
        public string Type_of_bearing { get; set; }
        public bool Rotation_speed_control { get; set; }
        public int Power_type_ID { get; set; }
        public float Noise_level { get; set; }

    }
}
