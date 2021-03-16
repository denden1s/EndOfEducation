using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Computer_house.OtherClasses
{
    class SetupIP
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
            {
                MessageBox.Show("Файла не существует");
                CreateXMLFile();
            }
            else
                MessageBox.Show("Существует");

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
            string path = fbPath + @"\" + fileName;
            using (FileStream fStream = new FileStream(path, FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(initialXMLFileContent);
                fStream.Write(array, 0, array.Length);
            }
        }
    }
}
