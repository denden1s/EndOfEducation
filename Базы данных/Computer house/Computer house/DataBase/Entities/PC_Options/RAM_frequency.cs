
namespace Computer_house.DataBase.Entities.PC_Options
{
    class RAM_frequency
    {
        public int ID { get; set; }
        public int Frequency { get; set; }
        public RAM_frequency() { }
        public RAM_frequency(int _frequency)
        {
            Frequency = _frequency;
        }
    }
}
