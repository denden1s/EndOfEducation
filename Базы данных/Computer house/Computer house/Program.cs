﻿using Computer_house.DataBase;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    if (db.Users.Count() != 0)
                    {
                        foreach (Users u in db.Users)
                        {
                            if(u.Authorization_status == true)
                            {
                                login = true;
                                user = new Users(u.Login, u.Password);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(login)
            {           
                Application.Run(new AuthorizedForm(user));
            }
            else
            {
                Application.Run(new AuthentificationForm());
            }
        }
    }
}
