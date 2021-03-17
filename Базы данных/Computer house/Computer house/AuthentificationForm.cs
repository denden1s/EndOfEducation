using Computer_house.DataBase;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
    public partial class AuthentificationForm : Form
    {
        private SetupIP setIP;
        private List<Users> Users;

        public AuthentificationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void BAuthentificate_Click(object sender, EventArgs e)
        {
            //Обработка событий авторизации на форме
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (db.Users.Count() != 0)
                    {
                        foreach (Users u in db.Users)
                        {
                            MessageBox.Show(u.Login);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
    }
}