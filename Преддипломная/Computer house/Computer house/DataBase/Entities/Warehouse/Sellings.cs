using System;

namespace Computer_house.DataBase.Entities.Warehouse
{
  public class Sellings
  {
    public int ID { get; set; }
    public int Product_ID { get; set; }

    public string Payment_method { get; set; }

    public DateTime Selling_date { get; set; } = DateTime.Now;

    public decimal Price { get; set; }

    public int Worker_ID { get; set; }

    public Sellings() { }
    public Sellings(int _productID, string _paymentMethod, int _workerID, decimal _price)
    {
      Product_ID = _productID;
      Payment_method = _paymentMethod;
      Worker_ID = _workerID;
      Price = _price;
    }
  }
}