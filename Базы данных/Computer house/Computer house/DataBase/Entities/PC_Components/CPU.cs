
namespace Computer_house.DataBase.Entities
{
    class CPU
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Series_ID { get; set; }
        public string Delivery_type { get; set; }
        public int Codename_ID { get; set; }
        public int Socket_ID { get; set; }
        public int Сores_count { get; set; }
        public bool Multithreading { get; set; }
        public int RAM_frequency_ID { get; set; }
        public int RAM_type_ID { get; set; }
        public int RAM_chanels_ID { get; set; }
        public bool Integrated_graphic { get; set; }
        public int Technical_process { get; set; }
    }
}
