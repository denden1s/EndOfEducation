
namespace Computer_house.DataBase.Entities.PC_Options
{
    class RAM_chanels
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public RAM_chanels() { }
        public RAM_chanels(string _name)
        {
            Name = _name;
        }
    }
}
