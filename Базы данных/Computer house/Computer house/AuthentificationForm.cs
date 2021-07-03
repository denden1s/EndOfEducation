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
                u.Authorization_status = false;
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
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          var user = db.Users.Single(i => i.Login == LoginInfo.Text);
          if (user != null)
          {
            if(!user.Authorization_status_in_shop)
            {
              if(user.Password == PasswordInfo.Text)
              {
                user.Authorization_status = true;
                db.SaveChanges();
                Authentification_logs log = new Authentification_logs(user.ID, true);
                SQLRequests.CreateAuthentificationLog(log);
                authorizedForm = new AuthorizedForm(user, true);
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