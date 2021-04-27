
namespace Computer_house.DataBase.Entities
{
  public class CPU_series : ProductWithOnlyName
  {
    public int ID { get; set; }

    public CPU_series() { }
    public CPU_series(string _name)
    {
      Name = _name;
    }
    public CPU_series(int _id, string _name)
    {
      ID = _id;
      Name = _name;
    }
  }
}