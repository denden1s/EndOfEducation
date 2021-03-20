
namespace Computer_house.DataBase.Entities
{
    class Connection_interfaces
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Connection_interfaces() { }
        public Connection_interfaces(string _name)
        {
            Name = _name;
        }

        public Connection_interfaces(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }
    }
}
