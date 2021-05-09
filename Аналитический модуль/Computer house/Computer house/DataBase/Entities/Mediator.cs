namespace Computer_house.DataBase.Entities
{
    public class Mediator
    {
        public int ID { get; set; }
        public string Components_type { get; set; }
        public string CPU_ID { get; set; }
        public string GPU_ID { get; set; }
        public string Case_ID { get; set; }
        public string RAM_ID { get; set; }
        public string Cooling_system_ID { get; set; }
        public string PSU_ID { get; set; }
        public string SD_ID { get; set; }
        public string Motherboard_ID { get; set; }

        public Mediator() { }
    }
}
