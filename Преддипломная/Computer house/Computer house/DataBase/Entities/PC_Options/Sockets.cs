
namespace Computer_house.DataBase.Entities.PC_Options
{
    public class Sockets : ProductWithOnlyName
    {
        public int ID { get; set; }
        //public string Name { get; set; }
        public Sockets() { }

        public Sockets(string _name)
        {
            Name = _name;
        }
        public Sockets(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }
    }
}
