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
    private string login = "root";
    private string password = "root";

    public AuthentificationForm()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        
    }

    private void BAuthentificate_Click(object sender, EventArgs e)
    {
      BAuthentificate.Enabled = false;
      LoginInfo.Enabled = false;
      PasswordInfo.Enabled = false;
      //Обработка событий авторизации на форме
      if((LoginInfo.Text == login) && (PasswordInfo.Text == password))
      {
        authorizedForm = new AuthorizedForm();
        this.Hide();
        authorizedForm.Show();
      }
      else
        MessageBox.Show("Введены некорректные данные");

      BAuthentificate.Enabled = true;
      LoginInfo.Enabled = true;
      PasswordInfo.Enabled = true;
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