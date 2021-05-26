using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.Warehouse
{
  public class Purchases
  {
    public int ID { get; set; }
    public DateTime Time { get; set; }
    public decimal Price { get; set; }
    public int Product_ID { get; set; }
    public int Count { get; set; }

    public Purchases() 
    {
      Time = DateTime.Now;
    }
    public Purchases(int productId, decimal price, int count)
    {
      Product_ID = productId;
      Price = price;
      Time = DateTime.Now;
      Count = count;
    }

  }
}
