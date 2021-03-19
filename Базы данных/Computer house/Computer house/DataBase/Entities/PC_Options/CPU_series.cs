
namespace Computer_house.DataBase.Entities
{
    class CPU_series
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CPU_series() { }
        public CPU_series(string _name)
        {
            Name = _name;
        }
    }
}
