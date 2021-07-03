using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house.OtherClasses
{
  public class Authentification_logs
  {
    public int ID { get; set; }
    public int User_ID { get; set; }
    public bool Authentification_status { get; set; }
    public DateTime Time { get; set; }
    public string Place { get; set; }
    internal string userName = "";
    public string status = "";

    public Authentification_logs() { }
    public void GetData()
    {

      using(ApplicationContext db = new ApplicationContext())
        userName = db.Users.Single(i => i.ID == User_ID).Name;
      status = Authentification_status ? "Вход" : "Выход";
    }
  }
}
