
namespace Computer_house.DataBase.Entities.PC_Options
{
    public class Power_connectors
    {
        public int ID { get; set; }
        public string Connectors { get; set; }

        public Power_connectors() { }
        public Power_connectors(string _connectors)
        {
            Connectors = _connectors;
        }
        public Power_connectors(int _id, string _connectors)
        {
            ID = _id;
            Connectors = _connectors;
        }
    }
}

