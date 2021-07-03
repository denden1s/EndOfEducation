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
    public DateTime Time { get; set; } = DateTime.Now;
    public string Place { get; set; } = "Warehouse";

    internal string userName = "";

    public Authentification_logs() { }
    public Authentification_logs(int userID, bool status)
    {
      User_ID = userID;
      using(ApplicationContext db = new ApplicationContext())
        userName = db.Users.Single(i => i.ID == userID).Name;
      Authentification_status = status;
    }
  }
}
