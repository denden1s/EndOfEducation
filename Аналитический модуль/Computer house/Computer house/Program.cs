﻿using System;
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
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new AuthentificationForm());
    }
  }
}
