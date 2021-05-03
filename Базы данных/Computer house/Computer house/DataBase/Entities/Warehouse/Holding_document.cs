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
      GetDataFromDB();
    }
    //Выборка данных из БД
    public void GetDataFromDB()
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          var findProduct = db.Mediator.Single(i => i.ID == Product_ID);
          switch (findProduct.Components_type)
          {
            case "CPU":
              Product_ID_InString = findProduct.CPU_ID;
              Product_name = db.CPU.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "GPU":
              Product_ID_InString = findProduct.GPU_ID;
              Product_name = db.GPU.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "Motherboard":
              Product_ID_InString = findProduct.Motherboard_ID;
              Product_name = db.Motherboard.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "Case":
              Product_ID_InString = findProduct.Case_ID;
              Product_name = db.Case.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "RAM":
              Product_ID_InString = findProduct.RAM_ID;
              Product_name = db.RAM.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "Cooling system":
              Product_ID_InString = findProduct.Cooling_system_ID;
              Product_name = db.Cooling_system.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "PSU":
              Product_ID_InString = findProduct.PSU_ID;
              Product_name = db.PSU.Single(i => i.ID == Product_ID_InString).Name;
              break;
            case "SD":
              Product_ID_InString = findProduct.SD_ID;
              Product_name = db.Storage_devices.Single(i => i.ID == Product_ID_InString).Name;
              break;
            default:
              break;
          }
          Location_name = db.Locations_in_warehouse.Single(i => i.ID == Location_ID).Location_label;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }     
    }
  }
}
