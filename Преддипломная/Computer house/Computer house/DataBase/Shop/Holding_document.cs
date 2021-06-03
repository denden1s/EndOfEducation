using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
  public  class Holding_document
  {
    public int ID { get; set; }
    public int Product_ID { get; set; }
    public string State { get; set; }
    public int Items_count_in_move { get; set; }
    public DateTime Time { get; set; } //Console.WriteLine("f: {0:f}", now); нужное форматирование
    public int User_ID { get; set; }
    public int Location_ID { get; set; }

    //Доп данные из БД
    internal string Product_ID_InString { get; set; }
    internal string Product_name { get; set; }
    internal string Location_name { get; set; }

    public Holding_document() { }
    public Holding_document(int _productID, string _state, int _itemsCountInMove,
      int _userID, int _locationID) 
    {
      Product_ID = _productID;
      State = _state;
      Items_count_in_move = _itemsCountInMove;
      Time = DateTime.Now;
      User_ID = _userID;
      Location_ID = _locationID;
    }
  }
}