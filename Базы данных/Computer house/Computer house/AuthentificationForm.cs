using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
    public partial class AuthentificationForm : Form
    {
        private AuthorizedForm authorizedForm;

        public AuthentificationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (db.Users.Count() != 0)
                    {
                        foreach (Users u in db.Users)
                        {
                            u.Authorization_status = false;
                        }
                        db.SaveChanges();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void BAuthentificate_Click(object sender, EventArgs e)
        {
            BAuthentificate.Enabled = false;
            LoginInfo.Enabled = false;
            PasswordInfo.Enabled = false;
            //Обработка событий авторизации на форме
            Authorization();
        }

        private void Authorization()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var user = db.Users.Single(i => i.Login == LoginInfo.Text);
                    if (user != null)
                    {
                        if (user.Password == PasswordInfo.Text)
                        {
                            user.Authorization_status = true;
                            db.SaveChanges();
                            authorizedForm = new AuthorizedForm(user);
                            authorizedForm.Show();
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void AuthentificationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}