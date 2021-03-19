
namespace Computer_house.DataBase.Entities.PC_Options
{
    class Sockets
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Sockets() { }

        public Sockets(string _name)
        {
            Name = _name;
        }
    }
}
