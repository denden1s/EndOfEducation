using Computer_house.DataBase;
using Computer_house.OtherClasses;
using System;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
    public partial class AuthentificationForm : Form
    {
        private SetupIP setIP;

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

                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к бд");
                string newIP = Microsoft.VisualBasic.Interaction.InputBox("Введите IP-адрес сервера БД:",
                    "Установка IP");
                setIP = new SetupIP(newIP);
                setIP.ChangeXmlFile();
                Application.Restart();
            }
        }
    }
}
