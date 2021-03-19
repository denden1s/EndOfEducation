
namespace Computer_house.DataBase.Entities.PC_Options
{
    class Memory_types
    {
        public int ID { get; set; }
        public string Device_type { get; set; }
        public string Name { get; set; }

        public Memory_types() { }
        public Memory_types(string _name, string _deviceType)
        {
            Name = _name;
            Device_type = _deviceType;
        }
    }
}
