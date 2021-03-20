using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_house.OtherClasses
{
    static class SystemFunctions
    {
        //Нужен потому, что соединение может пропасть в любой момент
        public static void SetNewDataBaseAdress(Exception _ex)
        {
            MessageBox.Show("Ошибка подключения к бд " + _ex.Message);
            string newIP = Microsoft.VisualBasic.Interaction.InputBox("Введите IP-адрес сервера БД:",
                "Установка IP");
            SetupIP setIP = new SetupIP(newIP);
            setIP.ChangeXmlFile();
           // Application.Restart();
        }
        public static void SetNewDataBaseAdress()
        {
            string newIP = Microsoft.VisualBasic.Interaction.InputBox("Введите IP-адрес сервера БД:",
                "Установка IP");
            SetupIP setIP = new SetupIP(newIP);
            setIP.ChangeXmlFile();
            // Application.Restart();
        }

        //Нужен для проверки ввода в текстовые поля отрицательные значения
        public static string CheckNumException(string _textboxText)
        {
            try
            {
                int num = Convert.ToInt32(_textboxText);
                if (num < 0)
                    return "0";
                else
                    return _textboxText;
            }
            catch (Exception)
            {

                MessageBox.Show("Введённое значение не является числовым");
                return "0";
            }
            
        }
    }
}
