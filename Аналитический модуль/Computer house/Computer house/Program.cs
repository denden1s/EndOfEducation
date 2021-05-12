using System;
using System.Linq;
using System.Windows.Forms;

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
        Application.Run(new AuthentificationForm());
      }
    }
  }
}
