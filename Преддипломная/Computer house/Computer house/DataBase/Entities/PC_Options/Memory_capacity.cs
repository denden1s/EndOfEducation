namespace Computer_house.DataBase.Entities.PC_Options
{
  public class Memory_capacity
  {
    public int Product_ID { get; set; }
    public int Capacity { get; set; }

    public Memory_capacity() { }
    public Memory_capacity(int _ID, int _capacity)
    {
      Product_ID = _ID;
      Capacity = _capacity;
    }
  }
}