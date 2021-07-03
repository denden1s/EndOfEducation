using Computer_house.DataBase.Entities.Warehouse;
using System;
using System.Linq;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      bool login = false;
      Users user = new Users();
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          if (db.Users.Count() != 0)
            foreach (Users u in db.Users)
              if (u.Authorization_status_in_shop == true)
              {
                login = true;
                user = new Users(u.ID, u.Login, u.Password);
              }
        }
      }
      catch(Exception)
      {
        Application.Run(new AuthentificationForm());
      }
      try
      {
        //Есть необходимость в запуске единственного экземпляра программы
        if(System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
        {
          MessageBox.Show("Экземпляр программы уже запущен!!!");
          System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        else
        {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          if(login)
            Application.Run(new AuthorizedForm(user));
          else
            Application.Run(new AuthentificationForm());
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        MessageBox.Show(ex.InnerException.Message);
      }
    }
  }
}