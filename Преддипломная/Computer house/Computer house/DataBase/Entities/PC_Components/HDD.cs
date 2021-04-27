using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
  public class HDD
  {
    public string ID { get; set; }
    public string Name { get; set; }
    public int Speed_of_spindle { get; set; }
    public float Noise_level { get; set; }    

    //Доп информация из БД

    public HDD() { }
    public HDD(string _id)
    {
        ID = _id;
    }

    //Выборка данных из БД
    public void GetInfoFromDB()
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          var hddInfo = db.HDD.Single(i => i.ID == ID);
          Name = hddInfo.Name;
          Speed_of_spindle = hddInfo.Speed_of_spindle;
          Noise_level = hddInfo.Noise_level;
          //SetStorageOptions("HDD");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
  }
}