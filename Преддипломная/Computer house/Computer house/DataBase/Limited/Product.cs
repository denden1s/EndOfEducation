namespace Computer_house.DataBase
{
  public class Product
  {
    public string ID { get; set; }
    public string Name { get; set; }

    public Product() { }
    public Product(string _id)
    {
      ID = _id;
    }

    public virtual void GetDataFromDB(){}
  }
}