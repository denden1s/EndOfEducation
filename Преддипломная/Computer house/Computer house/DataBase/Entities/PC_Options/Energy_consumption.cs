namespace Computer_house.DataBase.Entities.PC_Options
{
  public class Energy_consumption
  {
    public int Product_ID { get; set; }
    public int Consumption { get; set; }

    public Energy_consumption() { }
    public Energy_consumption(int _ID, int _consumption)
    {
      Product_ID = _ID;
      Consumption = _consumption;
    }
  }
}