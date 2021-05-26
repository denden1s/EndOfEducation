using System;
using System.Linq;

namespace Computer_house.DataBase.Entities.Warehouse
{
  public class ShopRequests
  {
    public int ID { get; set; }
    public int Product_ID { get; set; }
    public int Count { get; set; }
    public bool Status { get; set; } = false;
    public int User_ID { get; set; }
    public DateTime Time { get; set; }

    internal string ProductName { get; set; }

    public ShopRequests() { }
    public ShopRequests(int _productID, int _count, int _userID, DateTime _time)
    {
      Product_ID = _productID;
      Count = _count;
      User_ID = _userID;
      Time = _time;
    }

    public void GetDataFromDB()
    {
      Warehouse_info warehouseInfo = new Warehouse_info(Product_ID);
      warehouseInfo.SetName();
      ProductName = warehouseInfo.ProductName;
    }
  }
}