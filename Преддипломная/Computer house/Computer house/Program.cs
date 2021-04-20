﻿using Computer_house.DataBase.Entities.Warehouse;
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
            object locker = new object();
            bool login = false;
            Users user = new Users();
            try
            {
                lock(locker)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        if (db.Users.Count() != 0)
                        {
                            foreach (Users u in db.Users)
                            {
                                //нужно сделать к диплому отдельные категории работников как склада так и магазина, 
                                //чтобы не было конфликта с авторизацией
                                if (u.Authorization_status == true)
                                {
                                    login = true;
                                    user = new Users(u.ID, u.Login, u.Password);
                                }
                            }
                        }

                    }
                }
            }
            catch(Exception)
            {
                Application.Run(new AuthentificationForm());
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
