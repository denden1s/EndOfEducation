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
        Thread[] threads = new Thread[10];
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

        //Компоненты ПК
        private List<Warehouse_info> WarehouseInfo = new List<Warehouse_info>();
        private List<CPU> CPUList = new List<CPU>();
        //сведения о процессоре


        public ComponentsOptionsForm()
        {
            InitializeComponent();
        }
        public ComponentsOptionsForm(Users _user,List<Warehouse_info> _wareHouse, List<CPU> _cpus)
        {
            InitializeComponent();
            WarehouseInfo = _wareHouse;
            CPUList = _cpus;
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
            ActToComponent.Enabled = false;
            ActToComponent.Text = "";
            ActToComponent.BackColor = Color.Transparent;
            FindCPUIDButton.Enabled = false;
            ActWithCPU.Enabled = false;
            ActWithCPU.Text = "";
            ActWithCPU.BackColor = Color.Transparent;

            //Запуск потоков на выгрузку данных из БД
            threads[0] = new Thread(new ThreadStart(LoadCPUSeriesFromDB));
            threads[1] = new Thread(new ThreadStart(LoadCPUCodeNameFromDB));
            threads[2] = new Thread(new ThreadStart(LoadSocketInfoFromDB));
            threads[3] = new Thread(new ThreadStart(LoadChipsetInfoFromDB));
            threads[4] = new Thread(new ThreadStart(LoadChanelInfoFromDB));
            threads[5] = new Thread(new ThreadStart(LoadFrequensyInfoFromDB));
            threads[6] = new Thread(new ThreadStart(LoadFormFactorsFromDB));
            threads[7] = new Thread(new ThreadStart(LoadMemoryTypesFromDB));
            threads[8] = new Thread(new ThreadStart(LoadConnectionInterfacesFromDB));
            threads[9] = new Thread(new ThreadStart(LoadPowerConnectorsFromDB));
            foreach (var thread in threads)
            {
                thread.Start();
            }
            ViewCPUInfoInDataGrid();
            LoadSeriesInfoInComboBox();
            LoadCodeNameInfoInComboBox();
            LoadSocketInfoInComboBox(CPUSocketComboBox);
            LoadMemoryTypeInComboBox(CPUMemoryTypeComboBox, "RAM");
            LoadMemoryChanelsInComboBox(CPUChanelsComboBox);
            LoadRamFrequencyInComboBox(CPURamFrequencyComboBox);
        }

        private void LoadCPUSeriesFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.CPU_series)
                {
                    SeriesList.Add(new CPU_series(I.ID, I.Name));  
                }
            }
            threads[0].Interrupt();
        }
        private void LoadCPUCodeNameFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.CPU_codename)
                {
                    CPUCodeNamesList.Add(new CPU_codename(I.ID,I.Name));
                }
            }
            threads[1].Interrupt();
        }
        private void LoadSocketInfoFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Sockets)
                {
                    SocketsList.Add(new Sockets(I.ID, I.Name));
                }
            }
            threads[2].Interrupt();
        }
        private void LoadChipsetInfoFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Chipset)
                {
                    ChipsetsList.Add(new Chipset(I.ID, I.Name));
                }
            }
            threads[2].Interrupt();
        }
        private void LoadChanelInfoFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.RAM_chanels)
                {
                    RAMChanelsList.Add(new RAM_chanels(I.ID, I.Name));
                }
            }
            threads[4].Interrupt();
        }
        private void LoadFrequensyInfoFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.RAM_frequency)
                {
                    RAMFrequencyList.Add(new RAM_frequency(I.ID, I.Frequency));
                }
            }
            threads[5].Interrupt();
        }
        private void LoadFormFactorsFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Form_factors)
                {
                    FormFactorsList.Add(new Form_factors(I.ID, I.Name, I.Device_type));
                }
            }
            threads[6].Interrupt();
        }
        private void LoadMemoryTypesFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Memory_types)
                {
                    MemoryTypesList.Add(new Memory_types(I.ID, I.Name, I.Device_type));
                }
            }
            threads[7].Interrupt();
        }
        private void LoadConnectionInterfacesFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Connection_interfaces)
                {
                    ConnectionInterfacesList.Add(new Connection_interfaces(I.ID, I.Name));
                }
            }
            threads[8].Interrupt();
        }
        private void LoadPowerConnectorsFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var I in db.Power_connectors)
                {
                    PowerConnectorsList.Add(new Power_connectors(I.ID, I.Connectors));
                }
            }
            threads[9].Interrupt();
        }

        //Нужны для того чтобы не увеличивая форму вместить как можно больше элементов управления
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            this.AutoScroll = false;
        }
        private void tabPage1_MouseEnter(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        //Нужен для динамического изменения списка элементов
        private void TypesOfComponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVisible(false);
            ComponentNameTextBox.Clear();
            AddNewComponent.Checked = false;
            EditComponent.Checked = false;
            //textBox1.Enabled = true;
            ActToComponent.Enabled = false;
            ActToComponent.Text = "";
            ActToComponent.BackColor = Color.Transparent;
            switch (TypesOfComponentComboBox.SelectedIndex)
            {
                case 0:
                    ViewCPUSeriesToListBox();
                    break;
                case 1:
                    ViewCodeNameToListBox();
                    break;
                case 2:
                    ViewSocketInfoToListBox();
                    break;
                case 3:
                    ViewChipsetInfoToListBox();
                    break;
                case 4:
                    ViewChanelInfoToListBox();
                    break;
                case 5:
                    ViewFrequencyInfoToListBox();
                    break;
                case 6:
                    ViewFormFactorsToListBox();
                    SetVisible(true);
                    ComponentTypeComboBox.Items.Clear();
                    ComponentTypeComboBox.Items.AddRange(new string[] { "Motherboard", "Case", "HDD", "SSD" });
                    break;
                case 7:
                    ViewMemoryTypesToListBox();
                    SetVisible(true);
                    ComponentTypeComboBox.Items.Clear();
                    ComponentTypeComboBox.Items.AddRange(new string[] { "RAM", "GPU"});
                    break;
                case 8:
                    ViewConnectionInterfacesToListBox();
                    break;
                case 9:
                    ViewPowerConnectorsToListBox();
                    break;
                default: 
                    SetVisible(false);
                    ComponentsListBox.Items.Clear();
                    break;
            }
        }

        private void SetVisible(bool _state)
        {
            label4.Visible = _state;
            ComponentTypeComboBox.Visible = _state;
        }

        private void EditComponent_CheckedChanged(object sender, EventArgs e)
        {
           
            if (TypesOfComponentComboBox.Text == "")
            {
                EditComponent.Checked = false;
            }
            else
            {
                ActToComponent.Enabled = true;
                ActToComponent.Text = "Изменить";
                ActToComponent.BackColor = Color.BlueViolet;
            }
            LockButtonInSecondTab();
        }

        private void AddNewComponent_CheckedChanged(object sender, EventArgs e)
        {
            if (TypesOfComponentComboBox.Text == "")
            {
                AddNewComponent.Checked = false;
            }
            else
            {
                ActToComponent.Text = "Добавить";
                ActToComponent.Enabled = true;
                ActToComponent.BackColor = Color.Green;
            }
            LockButtonInSecondTab();
        }

        private void ActToComponent_Click(object sender, EventArgs e)
        {
            bool exception = false;
            if (ComponentNameTextBox.Text != "")
            {
                if (AddNewComponent.Checked)
                {
                    switch (TypesOfComponentComboBox.SelectedIndex)
                    {
                        case 0:
                            CPU_series newSeries = new CPU_series(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newSeries.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddCPUSeries(newSeries, true);
                                SeriesList.Add(newSeries);
                                ViewCPUSeriesToListBox();
                            }

                            break;
                        case 1:
                            CPU_codename newCodeName = new CPU_codename(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newCodeName.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddCPUCodeName(newCodeName, true);
                                CPUCodeNamesList.Add(newCodeName);
                                ViewCodeNameToListBox();
                            }
                            break;
                        case 2:
                            Sockets newSocket = new Sockets(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newSocket.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddSocket(newSocket, true);
                            }
                            break;
                        case 3:
                            Chipset newChipset = new Chipset(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newChipset.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddChipset(newChipset, true);
                                ChipsetsList.Add(newChipset);
                                ViewChipsetInfoToListBox();
                            }
                            break;
                        case 4:
                            RAM_chanels newChanel = new RAM_chanels(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newChanel.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddChanel(newChanel, true);
                                RAMChanelsList.Add(newChanel);
                                ViewChanelInfoToListBox();
                            }
                            break;
                        case 5:
                            //frequency - int 
                            int res;
                            bool isInt = Int32.TryParse(ComponentNameTextBox.Text, out res);
                            if (isInt)
                            {
                                //Везде где наименование атрибута отлично от Name возникают проблемы
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
                            Form_factors newForm = new Form_factors(ComponentNameTextBox.Text, ComponentTypeComboBox.Text);
                            if (ComponentsListBox.Items.Contains(newForm.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddFormFactor(newForm, true);
                                FormFactorsList.Add(newForm);

                                ViewFormFactorsToListBox();
                            }
                            break;
                        case 7:
                            Memory_types newMType = new Memory_types(ComponentNameTextBox.Text, ComponentTypeComboBox.Text);
                            if (ComponentsListBox.Items.Contains(newMType.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddMemoryType(newMType, true);
                                MemoryTypesList.Add(newMType);
                                ViewFormFactorsToListBox();
                            }
                            break;
                        case 8:
                            Connection_interfaces newInterface = new Connection_interfaces(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newInterface.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddConnectionInterface(newInterface, true);
                                ConnectionInterfacesList.Add(newInterface);
                                ViewConnectionInterfacesToListBox();
                            }
                            break;
                        case 9:
                            Power_connectors newConnector = new Power_connectors(ComponentNameTextBox.Text);
                            var find = PowerConnectorsList.Single(i => i.Connectors == newConnector.Connectors);
                            if (find != null)
                                exception = true;
                            else
                            {
                                
                                SQLRequests.AddPowerConnector(newConnector, true);
                                PowerConnectorsList.Add(newConnector);
                                ViewPowerConnectorsToListBox();
                            }
                            break;
                        default:

                            break;
                    }
                    if (exception)
                        MessageBox.Show("Данный элемент присутствует в базе данных");

                    ComponentNameTextBox.Clear();
                }
                else if ((ComponentsListBox.SelectedItem != null))
                {
                    switch (TypesOfComponentComboBox.SelectedIndex)
                    {
                        case 0:
                            SeriesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeSeries(SeriesList[ComponentsListBox.SelectedIndex], false);
                            ViewCPUSeriesToListBox();

                            break;
                        case 1:
                            CPUCodeNamesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeCodeName(CPUCodeNamesList[ComponentsListBox.SelectedIndex], false);
                            ViewCodeNameToListBox();
                            break;
                        case 2:
                            SocketsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeSocketInfo(SocketsList[ComponentsListBox.SelectedIndex], false);
                            ViewSocketInfoToListBox();
                            break;
                        case 3:
                            ChipsetsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeChipsetInfo(ChipsetsList[ComponentsListBox.SelectedIndex], false);
                            ViewChipsetInfoToListBox();
                            break;
                        case 4:
                            RAMChanelsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeChanelInfo(RAMChanelsList[ComponentsListBox.SelectedIndex], false);
                            ViewChanelInfoToListBox();
                            break;
                        case 5:
                            int res;
                            bool isInt = Int32.TryParse(ComponentNameTextBox.Text, out res);
                            if (isInt)
                            {
                                RAMFrequencyList[ComponentsListBox.SelectedIndex].Frequency = res;
                                SQLRequests.ChangeFrequencyInfo(RAMFrequencyList[ComponentsListBox.SelectedIndex], false);
                                ViewFrequencyInfoToListBox();
                            }
                            else
                            {
                                MessageBox.Show("Строка не является числом");
                            }
                            break;
                        case 6:
                            FormFactorsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            FormFactorsList[ComponentsListBox.SelectedIndex].Device_type = ComponentTypeComboBox.Text;
                            SQLRequests.ChangeFormFactors(FormFactorsList[ComponentsListBox.SelectedIndex], false);
                            ViewFormFactorsToListBox();
                            break;
                        case 7:
                            MemoryTypesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            MemoryTypesList[ComponentsListBox.SelectedIndex].Device_type = ComponentTypeComboBox.Text;
                            SQLRequests.ChangeMemoryType(MemoryTypesList[ComponentsListBox.SelectedIndex], false);
                            ViewMemoryTypesToListBox();
                            break;
                        case 8:
                            ConnectionInterfacesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeConnectionInterface(ConnectionInterfacesList[ComponentsListBox.SelectedIndex], false);
                            ViewConnectionInterfacesToListBox();
                            break;
                        case 9:
                            PowerConnectorsList[ComponentsListBox.SelectedIndex].Connectors = ComponentNameTextBox.Text;
                            SQLRequests.ChangePowerConnector(PowerConnectorsList[ComponentsListBox.SelectedIndex], false);
                            ViewPowerConnectorsToListBox();
                            break;
                        default:
                            break;
                    }
                    ComponentNameTextBox.Clear();
                }
                else
                    MessageBox.Show("Элемент для изменения не выбран!");

                ComponentNameTextBox.Clear();
            }
            else MessageBox.Show("Поле для ввода не заполнено.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LockButtonInSecondTab();
        }

        private void ComponentsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(EditComponent.Checked)
            {
                ComponentNameTextBox.Text = Convert.ToString(ComponentsListBox.Items[ComponentsListBox.SelectedIndex]);
                ComponentTypeComboBox.Items.Clear();
                if (TypesOfComponentComboBox.SelectedIndex == 6)
                {
                    ComponentTypeComboBox.Items.AddRange(new string[] { "Motherboard", "Case", "HDD", "SSD" });
                    switch (FormFactorsList[ComponentsListBox.SelectedIndex].Device_type)
                    {
                        case "Motherboard":
                            ComponentTypeComboBox.SelectedIndex = 0;
                            break;
                        case "Case":
                            ComponentTypeComboBox.SelectedIndex = 1;
                            break;
                        case "HDD":
                            ComponentTypeComboBox.SelectedIndex = 2;
                            break;
                        case "SSD":
                            ComponentTypeComboBox.SelectedIndex = 3;
                            break;
                        default:
                            break;
                    }
                }
                if (TypesOfComponentComboBox.SelectedIndex == 7)
                {
                    ComponentTypeComboBox.Items.AddRange(new string[] { "RAM", "GPU" });
                    if (MemoryTypesList[ComponentsListBox.SelectedIndex].Device_type == "RAM")
                        ComponentTypeComboBox.SelectedIndex = 0;
                    else
                        ComponentTypeComboBox.SelectedIndex = 1;
                }
            }
                
          
        }
            
        
        private void LockButtonInSecondTab()
        {
            if ((ComponentNameTextBox.Text == "")||((AddNewComponent.Checked == false)&&(EditComponent.Checked == false)))
                ActToComponent.Enabled = false;
            else
                ActToComponent.Enabled = true;
        }


        private void ViewCPUSeriesToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in SeriesList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewCodeNameToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in CPUCodeNamesList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewSocketInfoToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in SocketsList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewChipsetInfoToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in ChipsetsList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewChanelInfoToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in RAMChanelsList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewFrequencyInfoToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in RAMFrequencyList)
            {
                ComponentsListBox.Items.Add(Convert.ToString(i.Frequency));
            }
        }
        private void ViewFormFactorsToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in FormFactorsList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewMemoryTypesToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in MemoryTypesList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewConnectionInterfacesToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in ConnectionInterfacesList)
            {
                ComponentsListBox.Items.Add(i.Name);
            }
        }
        private void ViewPowerConnectorsToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in PowerConnectorsList)
            {
                ComponentsListBox.Items.Add(i.Connectors);
            }
        }

        //Нужен для загрузки сведений о процесорах в таблицу
        private void ViewCPUInfoInDataGrid()
        {
            dataGridView1.Rows.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                int numerator = 0;
                foreach (Warehouse_info wi in WarehouseInfo)
                {
                    dataGridView1.Rows.Add(CPUList[numerator].ID, wi.ProductName);
                    numerator++;
                }
            }
        }

        private void AddCPURadio_CheckedChanged(object sender, EventArgs e)
        {
           
            ActWithCPU.Text = "Добавить";
            ActWithCPU.BackColor = Color.Green;
            FindCPUIDButton.Enabled = true;

            ActWithCPU.Enabled = true;
        }

        private void ChangeCPURadio_CheckedChanged(object sender, EventArgs e)
        {
            ActWithCPU.Enabled = true;
            ActWithCPU.Text = "Изменить";
            ActWithCPU.BackColor = Color.BlueViolet;
            FindCPUIDButton.Enabled = true;
        }

        private void FindCPUIDButton_Click(object sender, EventArgs e)
        {
            bool findCPU = false;
            foreach (CPU cp in CPUList)
            {
                if (CPUIDTextBox.Text == cp.ID)
                    findCPU = true;
            }
            if (findCPU)
            {
                ActWithCPU.Enabled = false;
                MessageBox.Show("Такой серийный номер присутствует в базе");
            }
            else
            {
                MessageBox.Show("Такой серийный номер отсутствует в базе");
                ActWithCPU.Enabled = true;
            }    
        }

        private void CPUIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((AddCPURadio.Checked)||(ChangeCPURadio.Checked))
            {
                if (CPUIDTextBox.Text == "")
                    FindCPUIDButton.Enabled = false;
                else
                {
                    FindCPUIDButton.Enabled = true;
                    FindCPU();
                }
            }    
        }

        //Необходим для проверки наличия процессора в базе по уникальному идентификатору
        private void FindCPU()
        {
            bool findCPU = false;
            foreach (CPU cp in CPUList)
            {
                if (CPUIDTextBox.Text == cp.ID)
                    findCPU = true;
            }
            if (findCPU)
                ActWithCPU.Enabled = false;
            else
                ActWithCPU.Enabled = true;
        }

        //Необходим для проверки на заполненность всех полей для работы с процессором
        private bool CheckNull()
        {
            if(CPUIDTextBox.TextLength == 0)
                return true;
            if(CPUNameTextBox.TextLength == 0)
                return true;
            if(CPUSeriesComboBox.Text == "")
                return true;
            if (DeliveryTypeTextBox.TextLength == 0)
                return true;
            if (CPUCodeNameComboBox.Text == "")
                return true;
            if(CPUSocketComboBox.Text == "")
                return true;
            if (CPUCoresTextBox.TextLength == 0)
                return true;
            if (CPUBaseStateTextBox.TextLength == 0)
                return true;
            if (CPUMaxState.TextLength == 0)
                return true;
            if (CPUMemoryTypeComboBox.Text == "")
                return true;
            if (CPUChanelsComboBox.Text == "")
                return true;
            if (CPURamFrequencyComboBox.Text == "")
                return true;
            if (CPUTDPTextBox.TextLength == 0)
                return true;
            if (CPUTechprocessTextBox.TextLength == 0)
                return true;
            if (CPUBuyingPriceTextBox.TextLength == 0)
                return true;
            if (CPUSalePercentTextBox.TextLength == 0)
                return true;
            return false;
        }
        //Нужен для проверки на наличие числовых значений в необходимых полях
        private bool CheckIntParse()
        {
            int res;
            bool isInt = Int32.TryParse(CPUCoresTextBox.Text, out res);
            //true - если распарсит
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUBaseStateTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUMaxState.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUTDPTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUTechprocessTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUBuyingPriceTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUSalePercentTextBox.Text, out res);
            if (!isInt)
                return false;
            return true;
        }

        private void ActWithCPU_Click(object sender, EventArgs e)
        {
            bool exception = false;
            if (CheckNull())
            {
                exception = true;
                MessageBox.Show("Не все поля заполнены");
            }
            if(!CheckIntParse())
            {
                exception = true;
                MessageBox.Show("Не все значения можно привести к числовому виду");
            }

            if(!exception)
            {
                if (AddCPURadio.Checked)
                {
                    //Добавление процессора
                }
                else
                {
                    //изменение
                }
            }
        }

        //Необходимы для загрузки данных из списков в выпадающие списки (для CPU)
        private void LoadSeriesInfoInComboBox()
        {
            CPUSeriesComboBox.Items.Clear();
            foreach (var i in SeriesList)
            {
                CPUSeriesComboBox.Items.Add(i.Name);
            }
        }
        private void LoadCodeNameInfoInComboBox()
        {
            CPUCodeNameComboBox.Items.Clear();
            foreach (var i in CPUCodeNamesList)
            {
                CPUCodeNameComboBox.Items.Add(i.Name);
            }
        }
        private void LoadSocketInfoInComboBox(ComboBox _comboBox)
        {
            _comboBox.Items.Clear();
            foreach (var i in SocketsList)
            {
                _comboBox.Items.Add(i.Name);
            }
        }
        private void LoadMemoryTypeInComboBox(ComboBox _comboBox, string _deviceType)
        {
            _comboBox.Items.Clear();
            List<Memory_types> tempMemoryTypes = (from b in MemoryTypesList
                                                  where b.Device_type == _deviceType
                                                  select b).ToList();
            foreach (var i in tempMemoryTypes)
            {
                _comboBox.Items.Add(i.Name);
            }
        }
        private void LoadMemoryChanelsInComboBox(ComboBox _comboBox)
        {
            _comboBox.Items.Clear();
            foreach (var i in RAMChanelsList)
            {
                _comboBox.Items.Add(i.Name);
            }
        }
        private void LoadRamFrequencyInComboBox(ComboBox _comboBox)
        {
            _comboBox.Items.Clear();
            foreach (var i in RAMFrequencyList)
            {
                _comboBox.Items.Add(i.Frequency);
            }
        }
    }
}