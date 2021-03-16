using System;
using System.Windows.Forms;
using System.Xml;

namespace Computer_house.OtherClasses
{
    class SetupIP
    {
        private string ip;
        private string fbPath = Application.StartupPath;
        private string fileName = "IP_adress.xml";
        private XmlDocument xmlDoc;


        public SetupIP()
        {
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
    

        public SetupIP(string _ip)
        {
            ip = _ip;
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
            MessageBox.Show(ip);
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

        //Необходим на случай если xml файл внезапно удалится
        public void CreateXMLFile()
        {

        }
    }
}
