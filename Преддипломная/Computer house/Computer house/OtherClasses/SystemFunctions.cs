using System.Drawing;
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

    //Нужен для блокировки или разблокировки компонентов формы
    public static void ChangeEnable(bool status, params Control[] objects)
    {
      foreach (Control i in objects)
        i.Enabled = status;
    }

    public static void ClearComboBoxes(params ComboBox[] comboBoxes)
    {
      foreach (ComboBox i in comboBoxes) 
        i.SelectedItem = null;
    }

    public static bool CheckNullForComboBox(params ComboBox[] comboBoxes)
    {
      foreach (ComboBox i in comboBoxes)
        if (i.SelectedItem == null)
          return true;

      return false;
    }

    public static void Clear(params ComboBox[] comboBoxes)
    {
      foreach(ComboBox c in comboBoxes)
      {
        c.Items.Clear();
        c.BackColor = Color.White;
      }
        
    }
    public static void Clear(params ListBox[] listBoxes)
    {
      foreach(ListBox l in listBoxes)
        l.Items.Clear();
    }


    public static void Clear(params RichTextBox[] textBoxes)
    {
      foreach(RichTextBox t in textBoxes)
        t.Clear();
    }
  }
}