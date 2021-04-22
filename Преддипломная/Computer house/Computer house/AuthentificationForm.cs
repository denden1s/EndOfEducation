using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Linq;
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
                            u.Authorization_status_in_shop = false;
                        }
                        db.SaveChanges();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BAuthentificate_Click(object sender, EventArgs e)
        {
            BAuthentificate.Enabled = false;
            LoginInfo.Enabled = false;
            PasswordInfo.Enabled = false;
            //Обработка событий авторизации на форме
            Authorization();
            BAuthentificate.Enabled = true;
            LoginInfo.Enabled = true;
            PasswordInfo.Enabled = true;
        }

        private void Authorization()
        {
            bool findUser = false;
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var user = db.Users.Single(i => i.Login == LoginInfo.Text);
                    if (user != null)
                    {
                        if (user.Password == PasswordInfo.Text)
                        {
                            findUser = true;
                            user.Authorization_status_in_shop = true;
                            db.SaveChanges();
                            authorizedForm = new AuthorizedForm(user);
                            authorizedForm.Show();
                            this.Hide();
                        }
                    }

                }
                if (!findUser)
                    MessageBox.Show("Пользователь не найден либо пароль не верный.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AuthentificationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void настроитьIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemFunctions.SetNewDataBaseAdress();
        }
    }
}