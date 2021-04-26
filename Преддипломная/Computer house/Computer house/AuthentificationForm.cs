using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            Task.Run(() => LoadUsersFromDB());
        }
        
        private void LoadUsersFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                    if (db.Users.Count() != 0)
                    {
                        foreach (Users u in db.Users)
                            u.Authorization_status_in_shop = false;

                        db.SaveChanges();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BAuthentificate_Click(object sender, EventArgs e)
        {
            SystemFunctions.ChangeEnable(false, BAuthentificate, LoginInfo, PasswordInfo);
            Authorization();
            SystemFunctions.ChangeEnable(true, BAuthentificate, LoginInfo, PasswordInfo);
        }

        //Нужен для обработки авторизации
        private void Authorization()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Users user = db.Users.Single(i => i.Login == LoginInfo.Text);
                    if (user != null)
                    {
                        if (!user.Authorization_status)
                        {
                            if(user.Password == PasswordInfo.Text)
                            {
                                user.Authorization_status_in_shop = true;
                                db.SaveChanges();
                                authorizedForm = new AuthorizedForm(user);
                                authorizedForm.Show();
                                this.Hide();
                            }
                            else MessageBox.Show("Введённый пароль неверный.");

                        }
                        else MessageBox.Show("Вход в систему уже выполнен.");
                    }
                    else MessageBox.Show("Пользователь не найден.");
                }
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