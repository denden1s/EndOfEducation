using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
    public partial class ComponentsOptionsForm : Form
    {
        Thread firstThread;
        Thread secondThread;
        Thread thirdThread;
        Thread fourthThread;
        Thread fifthThread;
        Thread sixthThread;
        Thread seventhThread;
        Thread eightThread;
        Thread ninthThread;
        Thread tenthThread;
        private AuthorizedForm authorizedForm;
        private Users user;  
        //Списки для получения данных из БД
        private List<CPU_series> SeriesList = new List<CPU_series>();
        private List<CPU_codename> CPUCodeNamesList = new List<CPU_codename>();
        private List<Sockets> SocketsList = new List<Sockets>();
        private List<Chipset> ChipsetsList = new List<Chipset>();
        private List<RAM_chanels> RAMChanelsList = new List<RAM_chanels>();
        private List<RAM_frequency> RAMFrequencyList = new List<RAM_frequency>();
        private List<Form_factors> FormFactorsList = new List<Form_factors>();
        private List<Memory_types> MemoryTypesList = new List<Memory_types>();
        private List<Connection_interfaces> ConnectionInterfacesList = new List<Connection_interfaces>();
        private List<Power_connectors> PowerConnectorsList = new List<Power_connectors>();

        public ComponentsOptionsForm()
        {
            InitializeComponent();
        }
        public ComponentsOptionsForm(Users _user)
        {
            InitializeComponent(); ;
            user = _user;
        }

        private void AddComponentsOptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            authorizedForm = new AuthorizedForm(user);
            this.Hide();
            authorizedForm.Show();
        }

        private void AddComponentsOptionsForm_Load(object sender, EventArgs e)
        {
            //Настройки перед загрузкой формы
            SetVisible(false);
            button1.Enabled = false;
            button1.Text = "";
            button1.BackColor = Color.Transparent;
            //Запуск потоков на выгрузку данных из БД
            firstThread = new Thread(new ThreadStart(LoadCPUSeries));
            secondThread = new Thread(new ThreadStart(LoadCPUCodeName));
            thirdThread = new Thread(new ThreadStart(LoadSocketInfo));
            fourthThread = new Thread(new ThreadStart(LoadChipsetInfo));
            fifthThread = new Thread(new ThreadStart(LoadChanelInfo));
            sixthThread = new Thread(new ThreadStart(LoadFrequensyInfo));
            seventhThread = new Thread(new ThreadStart(LoadFormFactors));
            eightThread = new Thread(new ThreadStart(LoadMemoryTypes));
            ninthThread = new Thread(new ThreadStart(LoadConnectionInterfaces));
            tenthThread = new Thread(new ThreadStart(LoadPowerConnectors));
            firstThread.Start();
            secondThread.Start();
            thirdThread.Start();
            fourthThread.Start();
            fifthThread.Start();
            sixthThread.Start();
            seventhThread.Start();
            eightThread.Start();
            ninthThread.Start();
            tenthThread.Start();
        }

        //Загрузка сведений из БД в список Series
        private void LoadCPUSeries()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.CPU_series)
                {
                    SeriesList.Add(new CPU_series(I.ID, I.Name));  
                }
            }
            firstThread.Interrupt();
        }
        //Загрузка сведений из БД в список CPUCodeName
        private void LoadCPUCodeName()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.CPU_codename)
                {
                    CPUCodeNamesList.Add(new CPU_codename(I.ID,I.Name));
                }
            }
            secondThread.Interrupt();
        }

        private void LoadSocketInfo()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Sockets)
                {
                    SocketsList.Add(new Sockets(I.ID, I.Name));
                }
            }
            thirdThread.Interrupt();
        }

        private void LoadChipsetInfo()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Chipset)
                {
                    ChipsetsList.Add(new Chipset(I.ID, I.Name));
                }
            }
            fourthThread.Interrupt();
        }

        private void LoadChanelInfo()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.RAM_chanels)
                {
                    RAMChanelsList.Add(new RAM_chanels(I.ID, I.Name));
                }
            }
            fifthThread.Interrupt();
        }

        private void LoadFrequensyInfo()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.RAM_frequency)
                {
                    RAMFrequencyList.Add(new RAM_frequency(I.ID, I.Frequency));
                }
            }
            sixthThread.Interrupt();
        }
        private void LoadFormFactors()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Form_factors)
                {
                    FormFactorsList.Add(new Form_factors(I.ID, I.Name, I.Device_type));
                }
            }
            seventhThread.Interrupt();
        }
        private void LoadMemoryTypes()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Memory_types)
                {
                    MemoryTypesList.Add(new Memory_types(I.ID, I.Name, I.Device_type));
                }
            }
            eightThread.Interrupt();
        }
        private void LoadConnectionInterfaces()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Connection_interfaces)
                {
                    ConnectionInterfacesList.Add(new Connection_interfaces(I.ID, I.Name));
                }
            }
            ninthThread.Interrupt();
        }
        private void LoadPowerConnectors()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Power_connectors)
                {
                    PowerConnectorsList.Add(new Power_connectors(I.ID, I.Connectors));
                }
            }
            tenthThread.Interrupt();
        }

        //Нужен для динамического изменения списка элементов
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVisible(false);
            textBox1.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            //textBox1.Enabled = true;
            button1.Enabled = false;
            button1.Text = "";
            button1.BackColor = Color.Transparent;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    ViewCPUSeries();
                    break;
                case 1:
                    ViewCodeName();
                    break;
                case 2:
                    ViewSocketInfo();
                    break;
                case 3:
                    ViewChipsetInfo();
                    break;
                case 4:
                    ViewChanelInfo();
                    break;
                case 5:
                    ViewFrequencyInfo();
                    break;
                case 6:
                    ViewFormFactors();
                    SetVisible(true);
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new string[] { "Motherboard", "Case", "HDD", "SSD" });
                    break;
                case 7:
                    ViewMemoryTypes();
                    SetVisible(true);
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new string[] { "RAM", "GPU"});
                    break;
                case 8:
                    ViewConnectionInterfaces();
                    break;
                case 9:
                    ViewPowerConnectors();
                    break;
                default: 
                    SetVisible(false);
                    listBox1.Items.Clear();
                    break;
            }
        }

        private void SetVisible(bool _state)
        {
            label4.Visible = _state;
            comboBox2.Visible = _state;
        }

        //Установка режима изменения
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
            if (comboBox1.Text == "")
            {
                radioButton2.Checked = false;
            }
            else
            {
                button1.Enabled = true;
                button1.Text = "Изменить";
                button1.BackColor = Color.BlueViolet;
            }
            LockButton();
        }

        //Установка режима добавления
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                radioButton1.Checked = false;
            }
            else
            {
                button1.Text = "Добавить";
                button1.Enabled = true;
                button1.BackColor = Color.Green;
            }
            LockButton();
        }

        //Обработка полученных данных в элементах пк
        private void button1_Click(object sender, EventArgs e)
        {
            bool exception = false;
            if (textBox1.Text != "")
            {
                if (radioButton1.Checked)
                {
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            CPU_series newSeries = new CPU_series(textBox1.Text);
                            if (listBox1.Items.Contains(newSeries.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddCPUSeries(newSeries, true);
                                SeriesList.Add(newSeries);
                                ViewCPUSeries();
                            }

                            break;
                        case 1:
                            CPU_codename newCodeName = new CPU_codename(textBox1.Text);
                            if (listBox1.Items.Contains(newCodeName.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddCPUCodeName(newCodeName, true);
                                CPUCodeNamesList.Add(newCodeName);
                                ViewCodeName();
                            }
                            break;
                        case 2:
                            Sockets newSocket = new Sockets(textBox1.Text);
                            if (listBox1.Items.Contains(newSocket.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddSocket(newSocket, true);
                            }
                            break;
                        case 3:
                            Chipset newChipset = new Chipset(textBox1.Text);
                            if (listBox1.Items.Contains(newChipset.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddChipset(newChipset, true);
                                ChipsetsList.Add(newChipset);
                                ViewChipsetInfo();
                            }
                            break;
                        case 4:
                            RAM_chanels newChanel = new RAM_chanels(textBox1.Text);
                            if (listBox1.Items.Contains(newChanel.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddChanel(newChanel, true);
                                RAMChanelsList.Add(newChanel);
                                ViewChanelInfo();
                            }
                            break;
                        case 5:
                            //frequency - int 
                            int res;
                            bool isInt = Int32.TryParse(textBox1.Text, out res);
                            if (isInt)
                            {
                                
                                var find1 = RAMFrequencyList.Single(i => i.Frequency == res);
                                if (find1!=null)
                                    exception = true;
                                else
                                {
                                    RAM_frequency newFrequency = new RAM_frequency(res);
                                    SQLRequests.AddFrequency(newFrequency, true);
                                    RAMFrequencyList.Add(newFrequency);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Строка не является числом");
                            }
                            break;
                        case 6:
                            Form_factors newForm = new Form_factors(textBox1.Text, comboBox2.Text);
                            if (listBox1.Items.Contains(newForm.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddFormFactor(newForm, true);
                                FormFactorsList.Add(newForm);

                                ViewFormFactors();
                            }
                            break;
                        case 7:
                            Memory_types newMType = new Memory_types(textBox1.Text, comboBox2.Text);
                            if (listBox1.Items.Contains(newMType.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddMemoryType(newMType, true);
                                MemoryTypesList.Add(newMType);
                                ViewFormFactors();
                            }
                            break;
                        case 8:
                            Connection_interfaces newInterface = new Connection_interfaces(textBox1.Text);
                            if (listBox1.Items.Contains(newInterface.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddConnectionInterface(newInterface, true);
                                ConnectionInterfacesList.Add(newInterface);
                                ViewConnectionInterfaces();
                            }
                            break;
                        case 9:
                            
                            Power_connectors newConnector = new Power_connectors(textBox1.Text);
                            var find = PowerConnectorsList.Single(i => i.Connectors == newConnector.Connectors);
                            if (find != null)
                                exception = true;
                            else
                            {
                                
                                SQLRequests.AddPowerConnector(newConnector, true);
                                PowerConnectorsList.Add(newConnector);
                                ViewPowerConnectors();
                            }
                            break;
                        default:

                            break;
                    }
                    if (exception)
                        MessageBox.Show("Данный элемент присутствует в базе данных");

                    textBox1.Clear();
                }
                else if ((listBox1.SelectedItem != null))
                {
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            SeriesList[listBox1.SelectedIndex].Name = textBox1.Text;
                            SQLRequests.ChangeSeries(SeriesList[listBox1.SelectedIndex], false);
                            ViewCPUSeries();

                            break;
                        case 1:
                            CPUCodeNamesList[listBox1.SelectedIndex].Name = textBox1.Text;
                            SQLRequests.ChangeCodeName(CPUCodeNamesList[listBox1.SelectedIndex], false);
                            ViewCodeName();
                            break;
                        case 2:
                            SocketsList[listBox1.SelectedIndex].Name = textBox1.Text;
                            SQLRequests.ChangeSocketInfo(SocketsList[listBox1.SelectedIndex], false);
                            ViewSocketInfo();
                            break;
                        case 3:
                            ChipsetsList[listBox1.SelectedIndex].Name = textBox1.Text;
                            SQLRequests.ChangeChipsetInfo(ChipsetsList[listBox1.SelectedIndex], false);
                            ViewChipsetInfo();
                            break;
                        case 4:
                            RAMChanelsList[listBox1.SelectedIndex].Name = textBox1.Text;
                            SQLRequests.ChangeChanelInfo(RAMChanelsList[listBox1.SelectedIndex], false);
                            ViewChanelInfo();
                            break;
                        case 5:
                            int res;
                            bool isInt = Int32.TryParse(textBox1.Text, out res);
                            if (isInt)
                            {
                                RAMFrequencyList[listBox1.SelectedIndex].Frequency = res;
                                SQLRequests.ChangeFrequencyInfo(RAMFrequencyList[listBox1.SelectedIndex], false);
                                ViewFrequencyInfo();
                            }
                            else
                            {
                                MessageBox.Show("Строка не является числом");
                            }
                            break;
                        case 6:
                            FormFactorsList[listBox1.SelectedIndex].Name = textBox1.Text;
                            FormFactorsList[listBox1.SelectedIndex].Device_type = comboBox2.Text;
                            SQLRequests.ChangeFormFactors(FormFactorsList[listBox1.SelectedIndex], false);
                            ViewFormFactors();
                            break;
                        case 7:
                            MemoryTypesList[listBox1.SelectedIndex].Name = textBox1.Text;
                            MemoryTypesList[listBox1.SelectedIndex].Device_type = comboBox2.Text;
                            SQLRequests.ChangeMemoryType(MemoryTypesList[listBox1.SelectedIndex], false);
                            ViewMemoryTypes();
                            break;
                        case 8:
                            ConnectionInterfacesList[listBox1.SelectedIndex].Name = textBox1.Text;
                            SQLRequests.ChangeConnectionInterface(ConnectionInterfacesList[listBox1.SelectedIndex], false);
                            ViewConnectionInterfaces();
                            break;
                        case 9:
                            PowerConnectorsList[listBox1.SelectedIndex].Connectors = textBox1.Text;
                            SQLRequests.ChangePowerConnector(PowerConnectorsList[listBox1.SelectedIndex], false);
                            ViewPowerConnectors();
                            break;
                        default:
                            break;
                    }
                    textBox1.Clear();
                }
                else
                    MessageBox.Show("Элемент для изменения не выбран!");

                textBox1.Clear();
            }
            else MessageBox.Show("Поле для ввода не заполнено.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LockButton();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            if (comboBox1.SelectedIndex == 6)
            {
                comboBox2.Items.AddRange(new string[] { "Motherboard", "Case", "HDD", "SSD" });
                switch (FormFactorsList[listBox1.SelectedIndex].Device_type)
                {
                    case "Motherboard":
                        comboBox2.SelectedIndex = 0;
                        break;
                    case "Case":
                        comboBox2.SelectedIndex = 1;
                        break;
                    case "HDD":
                        comboBox2.SelectedIndex = 2;
                        break;
                    case "SSD":
                        comboBox2.SelectedIndex = 3;
                        break;
                    default:
                        break;
                }
            }
            if (comboBox1.SelectedIndex == 7)
            {
                comboBox2.Items.AddRange(new string[] { "RAM", "GPU" });
                if (MemoryTypesList[listBox1.SelectedIndex].Device_type == "RAM")
                    comboBox2.SelectedIndex = 0;
                else
                    comboBox2.SelectedIndex = 1;
            }
        }
            
        
        private void LockButton()
        {
            if ((textBox1.Text == "")||((radioButton1.Checked == false)&&(radioButton2.Checked == false)))
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        //Отображение сведений в списке
        private void ViewCPUSeries()
        {
            listBox1.Items.Clear();
            foreach (var i in SeriesList)
            {
                listBox1.Items.Add(i.Name);
            }
        }
        private void ViewCodeName()
        {
            listBox1.Items.Clear();
            foreach (var i in CPUCodeNamesList)
            {
                listBox1.Items.Add(i.Name);
            }
        }
        private void ViewSocketInfo()
        {
            listBox1.Items.Clear();
            foreach (var i in SocketsList)
            {
                listBox1.Items.Add(i.Name);
            }
        }
        private void ViewChipsetInfo()
        {
            listBox1.Items.Clear();
            foreach (var i in ChipsetsList)
            {
                listBox1.Items.Add(i.Name);
            }
        }
        private void ViewChanelInfo()
        {
            listBox1.Items.Clear();
            foreach (var i in RAMChanelsList)
            {
                listBox1.Items.Add(i.Name);
            }
        }

        private void ViewFrequencyInfo()
        {
            listBox1.Items.Clear();
            foreach (var i in RAMFrequencyList)
            {
                listBox1.Items.Add(Convert.ToString(i.Frequency));
            }
        }
        private void ViewFormFactors()
        {
            listBox1.Items.Clear();
            foreach (var i in FormFactorsList)
            {
                listBox1.Items.Add(i.Name);
            }
        }

        private void ViewMemoryTypes()
        {
            listBox1.Items.Clear();
            foreach (var i in MemoryTypesList)
            {
                listBox1.Items.Add(i.Name);
            }
        }

        private void ViewConnectionInterfaces()
        {
            listBox1.Items.Clear();
            foreach (var i in ConnectionInterfacesList)
            {
                listBox1.Items.Add(i.Name);
            }
        }
        private void ViewPowerConnectors()
        {
            listBox1.Items.Clear();
            foreach (var i in PowerConnectorsList)
            {
                listBox1.Items.Add(i.Connectors);
            }
        }
    }
}