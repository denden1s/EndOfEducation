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
        //Нужен для проверки на отрицательные значения
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
