
namespace Computer_house.DataBase.Entities.Warehouse
{
  public class Price_list
  {
    public int Product_ID { get; set; }
    public decimal Purchasable_price { get; set; }
    public int Markup_percent { get; set; }


    public Price_list() { }
    public Price_list(decimal _price, int _markupPrecent)
    {
      Purchasable_price = _price;
      Markup_percent = _markupPrecent;
    }
    public Price_list(int _productID, decimal _price, int _markupPrecent) 
    {
      Product_ID = _productID;
      Purchasable_price = _price;
      Markup_percent = _markupPrecent;
    }
  }
}
