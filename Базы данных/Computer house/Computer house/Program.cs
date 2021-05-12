using Computer_house.DataBase.Entities.Warehouse;
using System;
using System.Linq;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
  static class Program
  {
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    static void Main()
    {
      bool login = false;
      Users user = new Users();
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          foreach (Users u in db.Users)
          {
            if (u.Authorization_status == true)
            {
              login = true;
              user = new Users(u.ID, u.Login, u.Password);
              break;
            }
          }
        }
      }
      catch(Exception)
      {
        Application.Run(new AuthentificationForm());
      }

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
          Application.Run(new AuthorizedForm(user, true));
        else
          Application.Run(new AuthentificationForm());
      }
    }
  }
}
