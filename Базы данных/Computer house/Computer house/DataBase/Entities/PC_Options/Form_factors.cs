
namespace Computer_house.DataBase.Entities
{
    class Form_factors
    {
        public int ID { get; set; }
        public string Device_type { get; set; }
        public string Name { get; set; }

        public Form_factors() { }
        public Form_factors(string _name, string _deviceType)
        {
            Name = _name;
            Device_type = _deviceType;
        }
        public Form_factors(int _id, string _name, string _deviceType)
        {
            ID = _id;
            Name = _name;
            Device_type = _deviceType;
        }
    }
}
