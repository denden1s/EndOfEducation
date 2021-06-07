using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Computer_house.OtherClasses
{
  public class SetupIP
  {
    private string ip;
    private string fbPath = Application.StartupPath;
    private string fileName = "IP_adress.xml";
    private string initialXMLFileContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                            "<root>\n<SetIP Name=\"127.0.0.1\"/>\n</root>";
    private XmlDocument xmlDoc;

    public SetupIP()
    {
        if (!File.Exists(fbPath + @"\" + fileName))
            CreateXMLFile();

        xmlDoc = new XmlDocument();
        xmlDoc.Load(fbPath + @"\" + fileName);
        XmlElement xRoot = xmlDoc.DocumentElement;
        foreach (XmlNode xnode in xRoot)
        {
            // получаем атрибут name
            if (xnode.Attributes.Count == 1)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("Name");
                if (attr != null)
                {
                    ip = attr.Value;
                }
                       
            }
        }
    }
    
    //Нужен для установки нового ip адреса
    public SetupIP(string _ip)
    {
        if (!File.Exists(fbPath + @"\" + fileName))
            CreateXMLFile();

        ip = ChangeIncorrectIP(_ip);
    }

    //Нужен в случае если окно ввода IP будет заполнено некорректно
    private string ChangeIncorrectIP(string _ip)
    {
      int numerator = 0;
      int dotsCount = 0;
      int numsSeries = 0;
      bool numsInSeries = false;
      bool dotsInSeries = false;
      char dot = '.';
      string standartAdress = "127.0.0.1";
      //15 - макс длина IP адреса
      if((_ip.Length > 0)&&(_ip.Length <= 15))
      {
        char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        for (int i = 0; i < _ip.Length; i++)
        {
          for (int j = 0; j < 10; j++)
          {
            if (_ip[i] == nums[j])
            {
              numerator++;
              numsSeries++;
            }
          }
          if (numsSeries > 3)
            numsInSeries = true;
          else
            numsSeries = 0;

          if (_ip[i] == dot)
          {
            dotsCount++;
            if(i < _ip.Length - 1)
            {
              if (_ip[i + 1] == dot)
                dotsInSeries = true;
            }
          }
        }
        if ((_ip.Length == numerator + dotsCount)&&
            (dotsCount == 3) && (!dotsInSeries) && (!numsInSeries))
        {
          standartAdress = _ip;
        }
      }       
      return standartAdress;
    }

    //Необходим на случай если xml файл внезапно удалится
    private void CreateXMLFile()
    {
      string path = fbPath + @"\" + fileName;
      using (FileStream fStream = new FileStream(path, FileMode.Create))
      {
        byte[] array = System.Text.Encoding.Default.GetBytes(initialXMLFileContent);
        fStream.Write(array, 0, array.Length);
      }
    }

    //Необходим при сбое конфигурации IP адреса
    public void ChangeXmlFile()
    {
      xmlDoc = new XmlDocument();
      xmlDoc.Load(fbPath + @"\" + fileName);
      XmlElement xRoot = xmlDoc.DocumentElement;
      xRoot.RemoveAll();
      XmlElement setIP = xmlDoc.CreateElement("SetIP");
      XmlAttribute nameAttr = xmlDoc.CreateAttribute("Name");
      XmlNode node = xmlDoc.CreateTextNode(ip);
      nameAttr.AppendChild(node);
      setIP.Attributes.Append(nameAttr);
      xRoot.AppendChild(setIP);
      xmlDoc.Save(fbPath + @"\" + fileName);
    }

    //Необходим для установки IP в строку подключения
    public string GetIP()
    {
      return ip;
    }  
  }
}
