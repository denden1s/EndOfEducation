namespace Computer_house.DataBase.Entities.PC_Options
{
  public class Memory_types : ProductWithOnlyName
  {
    public int ID { get; set; }
    public string Device_type { get; set; }
    
    public Memory_types() { }
    public Memory_types(string _name, string _deviceType)
    {
      Name = _name;
      Device_type = _deviceType;
    }
    public Memory_types(int _id, string _name, string _deviceType)
    {
      ID = _id;
      Name = _name;
      Device_type = _deviceType;
    }
  }
}