using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.Warehouse;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house.OtherClasses
{
  static class SystemFunctions
  {

    //Нужен для настройки ip-адреса
    public static void SetNewDataBaseAdress()
    {
      string newIP = Microsoft.VisualBasic.Interaction.InputBox("Введите IP-адрес сервера БД:", "Установка IP");
      SetupIP setIP = new SetupIP(newIP);
      setIP.ChangeXmlFile();
    }

    public static void ViewHoldingDocsInDataGrid(DataGridView dataGrid, List<Holding_document> list)
    {
      dataGrid.Rows.Clear();
      foreach(var i in list)
      {
        i.GetDataFromDB();
        string name;
        using(ApplicationContext db = new ApplicationContext())
          name = db.Users.Single(b => b.ID == i.User_ID).Name;

        dataGrid.Rows.Add(i.ID, i.Product_name, i.Time, i.State, i.Items_count_in_move, name, i.Location_name);
      }
    }
  }
}