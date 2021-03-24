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
        //Нужен для настройки ip-адреса
        public static void SetNewDataBaseAdress()
        {
            string newIP = Microsoft.VisualBasic.Interaction.InputBox("Введите IP-адрес сервера БД:",
                "Установка IP");
            SetupIP setIP = new SetupIP(newIP);
            setIP.ChangeXmlFile();
        }
    }
}
