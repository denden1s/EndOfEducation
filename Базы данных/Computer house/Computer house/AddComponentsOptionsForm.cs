using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
    public partial class ComponentsOptionsForm : Form
    {
        Thread[] threads = new Thread[11];
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
        private List<GPU> GPUList = new List<GPU>();
        private List<Motherboard> MotherBoardList = new List<Motherboard>();
        private List<Case> CaseList = new List<Case>();
        private List<Holding_document> HoldingDocuments = new List<Holding_document>();

        private string GPUpowerTypeComboBoxText = "";
        private string GPUTypeComboBoxText = "";
        private string GPUconnectionInterfaceComboBoxText = "";

        private string CPUSeriesComboBoxText = "";
        private string CPUCodeNameComboBoxText = ""; 
        private string CPUSocketComboBoxText = "";
        private string CPUMemoryTypeComboBoxText = "";
        private string CPUChanelsComboBoxText = "";
        private string CPUMemoryFrequencyComboBoxText = "";

        private string MotherboardSocketComboBoxText = "";
        private string MotherboardChipsetComboBoxText = "";
        private string MotherboardFormFactorComboBoxText = "";
        private string MotherboardSupportedMemoryComboBoxText = "";
        private string MotherboardMemoryChanelsComboBoxText = "";
        private string MotherboardRAMFrequencyComboBoxText = "";

        private string CaseFormFactorComboBoxText = "";

        private bool firstLoad = true;

        public ComponentsOptionsForm()
        {
            InitializeComponent();
        }
        public ComponentsOptionsForm(Users _user,List<Warehouse_info> _wareHouse, List<CPU> _cpus, List<GPU> _gpus,
            List<Motherboard> _motherboards, List<Case> _cases)
        {
            InitializeComponent();
            //WarehouseInfo = _wareHouse;
            GPUList = _gpus;
            CPUList = _cpus;
            MotherBoardList = _motherboards;
            user = _user;
            CaseList = _cases;
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
            SystemFunctions.SetVisibleLables(this, false);
            FindCPUIDButton.Enabled = false;
            FindGPUIDButton.Enabled = false;
            FindMotherboardIDButton.Enabled = false;
            FindCaseIDButton.Enabled = false;
            SystemFunctions.SetButtonsDefaultOptions(ActToComponent, ActWithCPU, ActWithGPU,ActWithMotherboard,
                ActWithCase);

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
            threads[10] = new Thread(new ThreadStart(LoadHoldingDocsFromDB));
            foreach (var thread in threads)
            {
                thread.Start();
            }
            //отображение данных о видеокартах
            // + сделать потоки на эти методы
            ViewInfoInDataGrid<CPU>(CPU_DatagridView,CPUList);
            ViewInfoInDataGrid<GPU>(GPU_DatagridView, GPUList);
            ViewInfoInDataGrid<Motherboard>(Motherboard_DatagridView, MotherBoardList);
            ViewInfoInDataGrid<Case>(Case_DatagridView, CaseList);

            LoadInfoFromListToCombobox<CPU_series>(SeriesList, CPUSeriesComboBox);
            LoadInfoFromListToCombobox<CPU_codename>(CPUCodeNamesList, CPUCodeNameComboBox);
            LoadInfoFromListToCombobox<Sockets>(SocketsList, CPUSocketComboBox);
            LoadMemoryTypeInComboBox(CPUMemoryTypeComboBox, "RAM");
            LoadMemoryTypeInComboBox(MotherboardSupportedRAMComboBox, "RAM");
            LoadInfoFromListToCombobox<RAM_chanels>(RAMChanelsList, CPUChanelsComboBox);
            LoadInfoFromListToCombobox<Sockets>(SocketsList, MotherboardSocketComboBox);
            LoadInfoFromListToCombobox<Chipset>(ChipsetsList, MotherboardChipsetComboBox);
            LoadInfoFromListToCombobox<RAM_chanels>(RAMChanelsList, MotherBoardRAMChanelsComboBox);
            LoadRamFrequencyInComboBox(MotherboardRamFrequencyComboBox);
            LoadFormFactorsInComboBox(MotherboardFormFactorComboBox, "Motherboard");
            LoadFormFactorsInComboBox(CaseFormFactorComboBox, "Case");
            LoadRamFrequencyInComboBox(CPURamFrequaencyComboBox);
            SystemFunctions.ChangeCPUTextBoxesEnable(this, false);
            SystemFunctions.ChangeMotherboardTextBoxesEnable(this, false);
            SystemFunctions.ChangeCaseTextBoxesEnable(this, false);
            SystemFunctions.ChangeGPUTextBoxesEnable(this,false);
            ViewDocsInDataGrid();
        }

        //Методы для загрузки данных из БД
        private void LoadCPUSeriesFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.CPU_series)
                    {
                        SeriesList.Add(new CPU_series(I.ID, I.Name));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            threads[0].Interrupt();
        }
        private void LoadCPUCodeNameFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.CPU_codename)
                    {
                        CPUCodeNamesList.Add(new CPU_codename(I.ID, I.Name));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            } 
            threads[1].Interrupt();
        }
        private void LoadSocketInfoFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.Sockets)
                    {
                        SocketsList.Add(new Sockets(I.ID, I.Name));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            threads[2].Interrupt();
        }
        private void LoadChipsetInfoFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.Chipset)
                    {
                        ChipsetsList.Add(new Chipset(I.ID, I.Name));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }          
            threads[2].Interrupt();
        }
        private void LoadChanelInfoFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.RAM_chanels)
                    {
                        RAMChanelsList.Add(new RAM_chanels(I.ID, I.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            threads[4].Interrupt();
        }
        private void LoadFrequensyInfoFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.RAM_frequency)
                    {
                        RAMFrequencyList.Add(new RAM_frequency(I.ID, I.Frequency));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            threads[5].Interrupt();
        }
        private void LoadFormFactorsFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.Form_factors)
                    {
                        FormFactorsList.Add(new Form_factors(I.ID, I.Name, I.Device_type));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            threads[6].Interrupt();
        }
        private void LoadMemoryTypesFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.Memory_types)
                    {
                        MemoryTypesList.Add(new Memory_types(I.ID, I.Name, I.Device_type));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            threads[7].Interrupt();
        }
        private void LoadConnectionInterfacesFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.Connection_interfaces)
                    {
                        ConnectionInterfacesList.Add(new Connection_interfaces(I.ID, I.Name));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            threads[8].Interrupt();
        }
        private void LoadPowerConnectorsFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var I in db.Power_connectors)
                    {
                        PowerConnectorsList.Add(new Power_connectors(I.ID, I.Connectors));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            threads[9].Interrupt();
        }
        private void LoadHoldingDocsFromDB()
        {
            HoldingDocuments.Clear();
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var i in db.Holding_document)
                    {
                        HoldingDocuments.Add(i);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            threads[10].Interrupt();
        }

        //Нужен для отключения автоскрола и загрузки данных из комбобоксов
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            this.AutoScroll = false;
            //загрузка данных из комбобоксов
            GPUpowerTypeComboBoxText = GPUPowerTypeComboBox.Text;
            GPUTypeComboBoxText = GPUMemoryTypeComboBox.Text;
            GPUconnectionInterfaceComboBoxText = GPUInterfacesComboBox.Text;


            CPUSeriesComboBoxText = CPUSeriesComboBox.Text;
            CPUCodeNameComboBoxText = CPUCodeNameComboBox.Text;
            CPUSocketComboBoxText = CPUSocketComboBox.Text;
            CPUMemoryTypeComboBoxText = CPUMemoryTypeComboBox.Text;
            CPUChanelsComboBoxText = CPUChanelsComboBox.Text;
            CPUMemoryFrequencyComboBoxText = CPURamFrequaencyComboBox.Text;

            MotherboardSocketComboBoxText = MotherboardSocketComboBox.Text;
            MotherboardChipsetComboBoxText = MotherboardChipsetComboBox.Text;
            MotherboardFormFactorComboBoxText = MotherboardFormFactorComboBox.Text;
            MotherboardSupportedMemoryComboBoxText = MotherboardSupportedRAMComboBox.Text;
            MotherboardMemoryChanelsComboBoxText = MotherBoardRAMChanelsComboBox.Text;
            MotherboardRAMFrequencyComboBoxText = MotherboardRamFrequencyComboBox.Text;

            CaseFormFactorComboBoxText = CaseFormFactorComboBox.Text;
            //Вопрос в том очищать ли данные при переходе между страницами

        }

        //Нужен для динамического изменения списка элементов
        private void TypesOfComponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemFunctions.SetVisibleLables(this, false);
            ComponentNameTextBox.Clear();
            AddNewComponent.Checked = false;
            EditComponent.Checked = false;
            SystemFunctions.SetButtonsDefaultOptions(ActToComponent);
            switch (TypesOfComponentComboBox.SelectedIndex)
            {
                case 0:
                    ViewInfoInListBox<CPU_series>(ComponentsListBox, SeriesList);
                    break;
                case 1:
                    ViewInfoInListBox<CPU_codename>(ComponentsListBox, CPUCodeNamesList);
                    break;
                case 2:
                    ViewInfoInListBox<Sockets>(ComponentsListBox, SocketsList);
                    break;
                case 3:
                    ViewInfoInListBox<Chipset>(ComponentsListBox, ChipsetsList);
                    break;
                case 4:
                    ViewInfoInListBox<RAM_chanels>(ComponentsListBox, RAMChanelsList);
                    break;
                case 5:
                    ViewFrequencyInfoToListBox();
                    break;
                case 6:
                    ViewInfoInListBox<Form_factors>(ComponentsListBox, FormFactorsList);
                    SystemFunctions.SetVisibleLables(this, true);
                    ComponentTypeComboBox.Items.Clear();
                    ComponentTypeComboBox.Items.AddRange(new string[] { "Motherboard", "Case", "HDD", "SSD" });
                    break;
                case 7:
                    ViewInfoInListBox<Memory_types>(ComponentsListBox, MemoryTypesList);
                    SystemFunctions.SetVisibleLables(this, true);
                    ComponentTypeComboBox.Items.Clear();
                    ComponentTypeComboBox.Items.AddRange(new string[] { "RAM", "GPU"});
                    break;
                case 8:
                    ViewInfoInListBox<Connection_interfaces>(ComponentsListBox, ConnectionInterfacesList);
                    break;
                case 9:
                    ViewPowerConnectorsToListBox();
                    break;
                default:
                    SystemFunctions.SetVisibleLables(this, false);
                    ComponentsListBox.Items.Clear();
                    break;
            }
        }

        private void EditComponent_CheckedChanged(object sender, EventArgs e)
        {
            if(EditComponent.Checked)
            {
                if (TypesOfComponentComboBox.Text == "")
                {
                    EditComponent.Checked = false;
                }
                else
                {
                    SystemFunctions.SetEditOrAddButtonMode(ActToComponent, false);
                    if (ComponentsListBox.SelectedItem != null)
                        ComponentsList_SelectedIndexChanged(sender, e);
                    else
                        ComponentsListBox.SelectedIndex = 0;

                }
                SystemFunctions.LockButtonInSecondTab(this);
            }
        }

        private void AddNewComponent_CheckedChanged(object sender, EventArgs e)
        {
            if(AddNewComponent.Checked)
            {
                if (TypesOfComponentComboBox.Text == "")
                {
                    AddNewComponent.Checked = false;
                }
                else
                {
                    SystemFunctions.SetEditOrAddButtonMode(ActToComponent, true);
                    ComponentTypeComboBox.SelectedItem = null;
                    ComponentNameTextBox.Clear();

                }
                SystemFunctions.LockButtonInSecondTab(this);
            }
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
                                ViewInfoInListBox<CPU_series>(ComponentsListBox, SeriesList);
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
                                ViewInfoInListBox<CPU_codename>(ComponentsListBox, CPUCodeNamesList);
                            }
                            break;
                        case 2:
                            Sockets newSocket = new Sockets(ComponentNameTextBox.Text);
                            if (ComponentsListBox.Items.Contains(newSocket.Name))
                                exception = true;
                            else
                            {
                                SQLRequests.AddSocket(newSocket, true);
                                ViewInfoInListBox<Sockets>(ComponentsListBox, SocketsList);
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
                                ViewInfoInListBox<Chipset>(ComponentsListBox, ChipsetsList);
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
                                ViewInfoInListBox<RAM_chanels>(ComponentsListBox, RAMChanelsList);
                            }
                            break;
                        case 5:
                            //frequency - int 
                            int res;
                            bool isInt = Int32.TryParse(ComponentNameTextBox.Text, out res);
                            if (isInt)
                            {
                                //Везде где наименование атрибута отлично от Name возникают проблемы
                                var find1 = RAMFrequencyList.Where(i => i.Frequency == res);
                                if (find1.Count() != 0)
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

                                ViewInfoInListBox<Form_factors>(ComponentsListBox, FormFactorsList);
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
                                ViewInfoInListBox<Memory_types>(ComponentsListBox, MemoryTypesList);
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
                                ViewInfoInListBox<Connection_interfaces>(ComponentsListBox, ConnectionInterfacesList);
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

                    ComponentTypeComboBox.SelectedItem = null;
                    ComponentNameTextBox.Clear();
                }
                else if ((ComponentsListBox.SelectedItem != null))
                {
                    switch (TypesOfComponentComboBox.SelectedIndex)
                    {
                        case 0:
                            SeriesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeSeries(SeriesList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<CPU_series>(ComponentsListBox, SeriesList);
                            break;
                        case 1:
                            CPUCodeNamesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeCodeName(CPUCodeNamesList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<CPU_codename>(ComponentsListBox, CPUCodeNamesList);
                            break;
                        case 2:
                            SocketsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeSocketInfo(SocketsList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<Sockets>(ComponentsListBox, SocketsList);
                            break;
                        case 3:
                            ChipsetsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeChipsetInfo(ChipsetsList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<Chipset>(ComponentsListBox, ChipsetsList);
                            break;
                        case 4:
                            RAMChanelsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeChanelInfo(RAMChanelsList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<RAM_chanels>(ComponentsListBox, RAMChanelsList);
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
                            ViewInfoInListBox<Form_factors>(ComponentsListBox, FormFactorsList);
                            break;
                        case 7:
                            MemoryTypesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            MemoryTypesList[ComponentsListBox.SelectedIndex].Device_type = ComponentTypeComboBox.Text;
                            SQLRequests.ChangeMemoryType(MemoryTypesList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<Memory_types>(ComponentsListBox, MemoryTypesList);
                            break;
                        case 8:
                            ConnectionInterfacesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
                            SQLRequests.ChangeConnectionInterface(ConnectionInterfacesList[ComponentsListBox.SelectedIndex], false);
                            ViewInfoInListBox<Connection_interfaces>(ComponentsListBox, ConnectionInterfacesList);
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
                    ComponentTypeComboBox.SelectedItem = null;
                }
                else
                    MessageBox.Show("Элемент для изменения не выбран!");

                ComponentNameTextBox.Clear();
                ComponentTypeComboBox.SelectedItem = null;
            }
            else MessageBox.Show("Поле для ввода не заполнено.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SystemFunctions.LockButtonInSecondTab(this);
        }

        //нужен для того, чтобы выбрать данные из выпадающего списка
        //для раздела с доп сведениями о комплектующих
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

        //Нужны для того, чтобы отобразить данные в списке
        //для раздела с доп сведениями о комплектующих
        private void ViewInfoInListBox<T>(ListBox _listBox, List<T> _list) where T: ProductWithOnlyName
        {
            _listBox.Items.Clear();
            foreach (var i in _list)
            {
                _listBox.Items.Add(i.Name);
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
        private void ViewPowerConnectorsToListBox()
        {
            ComponentsListBox.Items.Clear();
            foreach (var i in PowerConnectorsList)
            {
                ComponentsListBox.Items.Add(i.Connectors);
            }
        }


        //Нужен для загрузки сведений  в таблицы
        private void ViewInfoInDataGrid<T>(DataGridView _datagrid, List<T> _list) where T : Product
        {
            try
            {
                _datagrid.Rows.Clear();
                foreach (T i in _list)
                {
                    _datagrid.Rows.Add(i.ID, i.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

        private void AddCPURadio_CheckedChanged(object sender, EventArgs e)
        {
            if(AddCPURadio.Checked)
            {
                SystemFunctions.ChangeCPUTextBoxesEnable(this, true);
                SystemFunctions.SetEditOrAddButtonMode(ActWithCPU, true);
                FindCPUIDButton.Enabled = true;
                SystemFunctions.ClearCPUInfoInTextBoxes(this);
            }
        }

        private void ChangeCPURadio_CheckedChanged(object sender, EventArgs e)
        {
            if(ChangeCPURadio.Checked)
            {
                SystemFunctions.ChangeCPUTextBoxesEnable(this, true);
                SystemFunctions.SetEditOrAddButtonMode(ActWithCPU, false);
                CPUIDTextBox.Enabled = false;
                FindCPUIDButton.Enabled = false;
                ViewCPUInfoToChange();
            }
        }

        private void FindCPUIDButton_Click(object sender, EventArgs e)
        {
            SearchInfoInList<CPU>(CPUList, CPUIDTextBox, ActWithCPU);  
        }
        
        //Нужен для поиска id перед добавлением данных о товаре
        private void SearchInfoInList<T>(List<T> _list, TextBox _textbox, Button _button)where T:Product
        {
            try
            {
                bool findItem = false;
                foreach (T i in _list)
                {
                    if (_textbox.Text == i.ID)
                        findItem = true;
                }
                if (findItem)
                {
                    MessageBox.Show("Такой серийный номер присутствует в базе");
                }
                else
                {
                    MessageBox.Show("Такой серийный номер отсутствует в базе");
                    _button.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void CPUIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (AddCPURadio.Checked)
            {
                if (CPUIDTextBox.Text == "")
                    FindCPUIDButton.Enabled = false;
                else
                {
                    FindCPUIDButton.Enabled = true;
                    FindInfoFromTextBox<CPU>(CPUIDTextBox, CPUList, ActWithCPU);
                }
            }    
        }

        //Необходим для проверки наличия процессора в базе по уникальному идентификатору
        private void FindInfoFromTextBox<T>(TextBox _searchTextBox, List<T> _list, Button _actionButton)where T: Product
        {
            var findInfo = _list.SingleOrDefault(i => i.ID == _searchTextBox.Text);//false;
            if (findInfo != null)
                _actionButton.Enabled = false;
            else
                _actionButton.Enabled = true;
        }

        private void ActWithCPU_Click(object sender, EventArgs e)
        {
            if (SystemFunctions.CheckNullForCPUTextBoxes(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (!SystemFunctions.CheckNumConvertForCPUTextBoxes(this))
            {                   
                MessageBox.Show("Не все значения можно привести к числовому виду");
                return;
            }

            if (AddCPURadio.Checked)
            {
                CPU newCPU = AddInfoAboutCPU();
                if (SQLRequests.Warning(true, newCPU.Name))
                {
                    SQLRequests.AddCPU(newCPU);
                    SQLRequests.EditCPUInMediator(newCPU, "Add");
                    CPUList.Add(newCPU);
                    MessageBox.Show("Добавление прошло успешно!");
                    ViewInfoInDataGrid<CPU>(CPU_DatagridView, CPUList);
                        
                }
            }
            else if (ChangeCPURadio.Checked)
            {
                CPU changedCPU = AddInfoAboutCPU();
                if (SQLRequests.Warning(false, changedCPU.Name))
                {
                    SQLRequests.ChangeCPU(changedCPU);
                    SQLRequests.EditCPUInMediator(changedCPU, "Edit");
                    int temp = CPUList.IndexOf(CPUList.Single(i => i.ID == changedCPU.ID));
                    CPUList[temp] = changedCPU;
                    MessageBox.Show("Изменение прошло успешно!");
                    ViewInfoInDataGrid<CPU>(CPU_DatagridView, CPUList);
                }
            }
            SystemFunctions.ClearCPUInfoInTextBoxes(this);
            CPUSeriesComboBoxText = "";
            CPUCodeNameComboBoxText = "";
            CPUSocketComboBoxText = "";
            CPUMemoryTypeComboBoxText = "";
            CPUChanelsComboBoxText = "";
            CPUMemoryFrequencyComboBoxText = "";
            this.ActiveControl = null;
        }

        //Необходим для загрузки данных из списков в выпадающие списки
        private void LoadInfoFromListToCombobox<T>(List<T> _list, ComboBox _combobox)where T: ProductWithOnlyName
        {
            _combobox.Items.Clear();
            foreach (var i in _list)
            {
                _combobox.Items.Add(i.Name);
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

        private void LoadFormFactorsInComboBox(ComboBox _comboBox, string _deviceType)
        {
            _comboBox.Items.Clear();
            List<Form_factors> form_Factors = (from b in FormFactorsList
                                               where b.Device_type == _deviceType
                                               select b).ToList();
            foreach (var i in form_Factors)
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

        //Аналогично вышеописанным методам только для списка видеокарт
        private void LoadPowerTypeInComboBox(ComboBox _combobox)
        {
            _combobox.Items.Clear();
            foreach (var i in PowerConnectorsList)
            {
                _combobox.Items.Add(i.Connectors);
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        //Нужен для загрузки данных из бд в поля для ввода
        private void ViewCPUInfoToChange()
        {
            if (CPU_DatagridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = CPU_DatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = CPU_DatagridView.Rows[selectedrowindex];
                //Отображение данных из списка CPU
                CPU currentCPU = new CPU();
                currentCPU = CPUList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
                CPUIDTextBox.Text = currentCPU.ID;
                CPUNameTextBox.Text = currentCPU.Name;
                CPUSeriesComboBox.SelectedItem = currentCPU.SeriesName;
                DeliveryTypeComboBox.SelectedItem = currentCPU.Delivery_type;
                CPUCodeNameComboBox.SelectedItem = currentCPU.CodeName;
                CPUSocketComboBox.SelectedItem = currentCPU.Socket;
                CPUCoresTextBox.Text = Convert.ToString(currentCPU.Сores_count);
                MultithreadingCheckBox.Checked = currentCPU.Multithreading;
                CPUBaseStateTextBox.Text = Convert.ToString(currentCPU.Base_state);
                CPUMaxStateTextBox.Text = Convert.ToString(currentCPU.Max_state);
                CPUMemoryTypeComboBox.SelectedItem = currentCPU.RAM_type;
                CPUChanelsComboBox.SelectedItem = currentCPU.RAM_chanel;
                CPURamFrequaencyComboBox.SelectedItem = currentCPU.RAM_frequency;
                CPUIntegratedGraphicCheckBox.Checked = currentCPU.Integrated_graphic;
                CPUTDPTextBox.Text = Convert.ToString(currentCPU.Consumption);
                CPUTechprocessTextBox.Text = Convert.ToString(currentCPU.Technical_process);
            }
        }

        private void ViewGPUInfoToChange()
        {
            if (GPU_DatagridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = GPU_DatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = GPU_DatagridView.Rows[selectedrowindex];
                //Отображение данных из списка CPU
                GPU currentGPU = new GPU();
                currentGPU = GPUList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
                GPUIDTextBox.Text = currentGPU.ID;
                GPUNameTextBox.Text = currentGPU.Name;
                GPUInterfacesComboBox.SelectedItem = currentGPU.ConnectionInterface;
                GPUManufactureTextBox.Text = currentGPU.Manufacturer;
                GPUSLISupportCheckBox.Checked = currentGPU.SLI_support;
                GPUCapacityTextBox.Text = Convert.ToString(currentGPU.Capacity);
                GPUMemoryTypeComboBox.SelectedItem = currentGPU.GPU_type;
                GPUBusWidthTextBox.Text = Convert.ToString(currentGPU.Bus_width);
                GPUOverclockingCheckBox.Checked = currentGPU.Overclocking;
                GPUEnergyConsumptTextBox.Text = Convert.ToString(currentGPU.Consumption);
                GPUDirectXVersionTextBox.Text = currentGPU.DirectX;
                GPUOutputInterfacesTextBox.Text = currentGPU.External_interfaces;
                GPUPowerTypeComboBox.SelectedItem = currentGPU.PowerType;
                GPUCoolersCountTextBox.Text = Convert.ToString(currentGPU.Coolers_count);
                GPUCoolingSysThicknessTextBox.Text = Convert.ToString(currentGPU.Cooling_system_thikness);
                GPULengthTextBox.Text = Convert.ToString(currentGPU.Length);
                GPUHeightTextBox.Text = Convert.ToString(currentGPU.Height);
            }
        }

        private void ViewMotherboardInfoToChange()
        {
            if (Motherboard_DatagridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = Motherboard_DatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = Motherboard_DatagridView.Rows[selectedrowindex];
                Motherboard motherboard = new Motherboard();
                motherboard = MotherBoardList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
                MotherboardIDTextBox.Text = motherboard.ID;
                MotherboardNameTextBox.Text = motherboard.Name;
                MotherboardConnectorsTextBox.Text = motherboard.Connectors;
                MotherboardSupportedCPUTextBox.Text = motherboard.Supported_CPU;
                MotherboardSLISupportCheckBox.Checked = motherboard.SLI_support;
                MotherboardIntegratedGraphicCheckBox.Checked = motherboard.Integrated_graphic;
                MotherboardRAMCapacityTextBox.Text = Convert.ToString(motherboard.Capacity);
                MotherboardCountOfRAMSlotsTextBox.Text = Convert.ToString(motherboard.Count_of_memory_slots);
                MotherboardExpansionsSlotsTextBox.Text = motherboard.Expansion_slots;
                MotherboardStorageInterfacesTextBox.Text = motherboard.Storage_interfaces;
                MotherboardLengthTextBox.Text = Convert.ToString(motherboard.Length);
                MotherboardWidthTextBox.Text = Convert.ToString(motherboard.Width);
                MotherboardSocketComboBox.SelectedItem = motherboard.Socket;
                MotherboardChipsetComboBox.SelectedItem = motherboard.Chipset;
                MotherBoardRAMChanelsComboBox.SelectedItem = motherboard.RAM_chanel;
                MotherboardFormFactorComboBox.SelectedItem = motherboard.FormFactor;
                MotherboardSupportedRAMComboBox.SelectedItem = motherboard.RAM_type;
                MotherboardRamFrequencyComboBox.SelectedItem = motherboard.RAM_frequency;
            }
        }

        //Добавить с материнками

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ChangeCPURadio.Checked)
            {
                ViewCPUInfoToChange();
                ActWithCPU.Enabled = true;
            } 
        }

        //Нужен для добавления или изменения сведений о процессоре
        private CPU AddInfoAboutCPU()
        {
            CPU changedCPU = new CPU();
            changedCPU.ID = CPUIDTextBox.Text;
            changedCPU.Name = CPUNameTextBox.Text;
            changedCPU.Series_ID = SeriesList.Single(i => i.Name == CPUSeriesComboBox.Text).ID;
            changedCPU.SeriesName = CPUSeriesComboBox.Text;
            changedCPU.Delivery_type = DeliveryTypeComboBox.Text;
            changedCPU.Codename_ID = CPUCodeNamesList.Single(i => i.Name == CPUCodeNameComboBox.Text).ID;
            changedCPU.CodeName = CPUCodeNameComboBox.Text;
            changedCPU.Socket_ID = SocketsList.Single(i => i.Name == CPUSocketComboBox.Text).ID;
            changedCPU.Socket = CPUSocketComboBox.Text;
            changedCPU.Сores_count = Convert.ToInt32(CPUCoresTextBox.Text);
            changedCPU.Multithreading = MultithreadingCheckBox.Checked;
            changedCPU.Base_state = Convert.ToInt32(CPUBaseStateTextBox.Text);
            changedCPU.Max_state = Convert.ToInt32(CPUMaxStateTextBox.Text);
            changedCPU.RAM_type_ID = (from b in MemoryTypesList
                                      where b.Device_type == "RAM" && b.Name == CPUMemoryTypeComboBox.Text
                                      select b.ID).SingleOrDefault();
            changedCPU.RAM_type = CPUMemoryTypeComboBox.Text;
            changedCPU.RAM_chanels_ID = RAMChanelsList.Single(i => i.Name == CPUChanelsComboBox.Text).ID;
            changedCPU.RAM_chanel = CPUChanelsComboBox.Text;
            changedCPU.RAM_frequency_ID = RAMFrequencyList.Single(i =>
                  i.Frequency == Convert.ToInt32(CPURamFrequaencyComboBox.Text)).ID;
            changedCPU.RAM_frequency = Convert.ToInt32(CPURamFrequaencyComboBox.Text);
            changedCPU.Integrated_graphic = CPUIntegratedGraphicCheckBox.Checked;
            changedCPU.Technical_process = Convert.ToInt32(CPUTechprocessTextBox.Text);
            changedCPU.Consumption = Convert.ToInt32(CPUTDPTextBox.Text);
            return changedCPU;
        }

        private GPU AddInfoAboutGPU()
        {
            GPU newGPU = new GPU();
            newGPU.ID = GPUIDTextBox.Text;
            newGPU.Name = GPUNameTextBox.Text;
            newGPU.Interface_ID = (from b in ConnectionInterfacesList
                                   where b.Name == GPUInterfacesComboBox.Text
                                   select b.ID).Single();
            newGPU.Manufacturer = GPUManufactureTextBox.Text;
            newGPU.SLI_support = GPUSLISupportCheckBox.Checked;
            newGPU.Capacity = Convert.ToInt32(GPUCapacityTextBox.Text);
            newGPU.GPU_type_ID = (from b in MemoryTypesList
                                  where b.Device_type == "GPU" && b.Name == GPUMemoryTypeComboBox.Text
                                  select b.ID).Single();
            newGPU.Bus_width = Convert.ToInt32(GPUBusWidthTextBox.Text);
            newGPU.Overclocking = GPUOverclockingCheckBox.Checked;
            newGPU.Consumption = Convert.ToInt32(GPUEnergyConsumptTextBox.Text);
            newGPU.DirectX = GPUDirectXVersionTextBox.Text;
            newGPU.External_interfaces = GPUOutputInterfacesTextBox.Text;
            newGPU.Power_type_ID = PowerConnectorsList.Single(i => i.Connectors == GPUPowerTypeComboBox.Text).ID;
            newGPU.Coolers_count = Convert.ToInt32(GPUCoolersCountTextBox.Text);
            newGPU.Cooling_system_thikness = Convert.ToInt32(GPUCoolingSysThicknessTextBox.Text);
            newGPU.Length = Convert.ToInt32(GPULengthTextBox.Text);
            newGPU.Height = Convert.ToInt32(GPUHeightTextBox.Text);
            return newGPU;
        }

        private Motherboard AddInfoAboutMotherboard()
        {
            Motherboard newMotherboard = new Motherboard();
            newMotherboard.ID = MotherboardIDTextBox.Text;
            newMotherboard.Name = MotherboardNameTextBox.Text;
            newMotherboard.Connectors = MotherboardConnectorsTextBox.Text;
            newMotherboard.Supported_CPU = MotherboardSupportedCPUTextBox.Text;
            newMotherboard.SLI_support = MotherboardSLISupportCheckBox.Checked;
            newMotherboard.Integrated_graphic = MotherboardIntegratedGraphicCheckBox.Checked;
            newMotherboard.Capacity = Convert.ToInt32(MotherboardRAMCapacityTextBox.Text);
            newMotherboard.Count_of_memory_slots = Convert.ToInt32(MotherboardCountOfRAMSlotsTextBox.Text);
            newMotherboard.Expansion_slots = MotherboardExpansionsSlotsTextBox.Text;
            newMotherboard.Storage_interfaces = MotherboardStorageInterfacesTextBox.Text;
            newMotherboard.Length = Convert.ToInt32(MotherboardLengthTextBox.Text);
            newMotherboard.Width = Convert.ToInt32(MotherboardWidthTextBox.Text);
            newMotherboard.Socket_ID = SocketsList.Single(i => i.Name == MotherboardSocketComboBox.Text).ID;
            newMotherboard.Chipset_ID = ChipsetsList.Single(i => i.Name == MotherboardChipsetComboBox.Text).ID;
            newMotherboard.RAM_chanels_ID = RAMChanelsList.Single(i => i.Name == MotherBoardRAMChanelsComboBox.Text).ID;
            newMotherboard.RAM_frequency = Convert.ToInt32(MotherboardRamFrequencyComboBox.Text);
            newMotherboard.Form_factor_ID = (from b in FormFactorsList
                                             where b.Device_type == "Motherboard" && 
                                             b.Name == MotherboardFormFactorComboBox.Text
                                             select b.ID).Single();
            newMotherboard.RAM_type_ID = (from b in MemoryTypesList
                                          where b.Device_type == "RAM" &&
                                          b.Name == MotherboardSupportedRAMComboBox.Text
                                          select b.ID).Single();
            return newMotherboard;
        }



        private void tabPage12_Enter(object sender, EventArgs e)
        {
            //загрузка данных из holding doc
            this.AutoScroll = false;
        }
        

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void ViewDocsInDataGrid()
        {
            HoldingDocsDatagridView.Rows.Clear();
            foreach (var i in HoldingDocuments)
            {
                i.GetDataFromDB();
                string name;
                using (ApplicationContext db = new ApplicationContext())
                {
                    name = db.Users.Single(b => b.ID == i.User_ID).Name;
                }
                    HoldingDocsDatagridView.Rows.Add(i.ID, i.Product_name, i.Time, i.State, i.Items_count_in_move, name, i.Location_name);
            }
        }

        private void SearchGPUIDButton_Click(object sender, EventArgs e)
        {
            SearchInfoInList<GPU>(GPUList, GPUIDTextBox, ActWithGPU);
        }

        private void AddGPURadio_CheckedChanged(object sender, EventArgs e)
        {
            if(AddGPURadio.Checked)
            {
                SystemFunctions.SetEditOrAddButtonMode(ActWithGPU, true);
                SystemFunctions.ChangeGPUTextBoxesEnable(this, true);
                FindGPUIDButton.Enabled = true;
                SystemFunctions.ClearGPUInfoTextBoxes(this);
            }
        }

        private void ChangeGPURadio_CheckedChanged(object sender, EventArgs e)
        {
            if(ChangeGPURadio.Checked)
            {
                SystemFunctions.ChangeGPUTextBoxesEnable(this, true);
                FindGPUIDButton.Enabled = false;
                GPUIDTextBox.Enabled = false;
                SystemFunctions.SetEditOrAddButtonMode(ActWithGPU, false);
                ViewGPUInfoToChange();
            }
        }

        private void ActWithGPU_Click(object sender, EventArgs e)
        {
            if (SystemFunctions.CheckNullForGPUTextBoxes(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (!SystemFunctions.CheckNumConvertGPUTextBoxes(this))
            {               
                MessageBox.Show("Не все поля можно привести к числовому типу");
                return;
            }               
            
            if (AddGPURadio.Checked)
            {
                GPU newGPU = AddInfoAboutGPU();
                if (SQLRequests.Warning(true, newGPU.Name))
                {
                    SQLRequests.AddGPU(newGPU);
                    SQLRequests.EditGPUInMediator(newGPU, "Add");
                    GPUList.Add(newGPU);
                    MessageBox.Show("Добавление прошло успешно!");
                    ViewInfoInDataGrid<GPU>(GPU_DatagridView, GPUList);
                        
                }
            }
            else if (ChangeGPURadio.Checked)
            {
                GPU changedGPU = AddInfoAboutGPU();
                if (SQLRequests.Warning(false, changedGPU.Name))
                {
                    SQLRequests.ChangeGPU(changedGPU);
                    SQLRequests.EditGPUInMediator(changedGPU, "Edit");
                    int temp = GPUList.IndexOf(GPUList.Single(i => i.ID == changedGPU.ID));
                    GPUList[temp] = changedGPU;
                    MessageBox.Show("Изменение прошло успешно!");
                    ViewInfoInDataGrid<GPU>(GPU_DatagridView, GPUList);
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                GPUList.Clear();
                int counter = 0;
                foreach (GPU c in db.GPU)
                {
                    GPUList.Add(new GPU(c.ID));
                    GPUList[counter].GetDataFromDB();
                    counter++;
                }
            }
            SystemFunctions.ClearGPUInfoTextBoxes(this);
            GPUpowerTypeComboBoxText = "";
            GPUTypeComboBoxText = "";
            GPUconnectionInterfaceComboBoxText = "";
            this.ActiveControl = null;
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        private void GPUIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (AddGPURadio.Checked)
            {
                if (GPUIDTextBox.Text == "")
                    FindGPUIDButton.Enabled = false;
                else
                {
                    FindGPUIDButton.Enabled = true;
                    FindInfoFromTextBox<GPU>(GPUIDTextBox, GPUList, ActWithGPU);
                }
            }
        }

        private void GPU_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ChangeGPURadio.Checked)
            {
                ViewGPUInfoToChange();
                ActWithGPU.Enabled = true;
            }
        }

        private void SearcchMotherboardButton_Click(object sender, EventArgs e)
        {
            SearchInfoInList<Motherboard>(MotherBoardList, MotherboardIDTextBox, ActWithMotherboard);
        }

        private void AddMotherboardRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(AddMotherboardRadio.Checked)
            {
                SystemFunctions.SetEditOrAddButtonMode(ActWithMotherboard, true);
                SystemFunctions.ChangeMotherboardTextBoxesEnable(this, true);
                FindMotherboardIDButton.Enabled = true;
                SystemFunctions.ClearMotherboardTextBoxes(this);
            } 
        }

        private void ChangeMotherboardRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(ChangeMotherboardRadio.Checked)
            {
                SystemFunctions.ChangeMotherboardTextBoxesEnable(this, true);
                FindMotherboardIDButton.Enabled = false;
                MotherboardIDTextBox.Enabled = false;
                SystemFunctions.SetEditOrAddButtonMode(ActWithMotherboard, false);
                ViewMotherboardInfoToChange();
            }
        }

        private void ActWithMotherboard_Click(object sender, EventArgs e)
        {
            if (SystemFunctions.CheckNullForCaseTextBoxes(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (!SystemFunctions.CheckNumConvertMotherboardTextBoxes(this))
            {
                MessageBox.Show("Не все поля можно привести к числовому типу");
                return;
            }

            if (AddMotherboardRadio.Checked)
            {
                Motherboard newMotherboard = AddInfoAboutMotherboard();
                if (SQLRequests.Warning(true, newMotherboard.Name))
                {
                    SQLRequests.AddMotherboard(newMotherboard);
                    SQLRequests.EditMotherboardMediator(newMotherboard, "Add");
                    MotherBoardList.Add(newMotherboard);
                    MessageBox.Show("Добавление прошло успешно!");
                    ViewInfoInDataGrid<Motherboard>(Motherboard_DatagridView, MotherBoardList);
                        
                }
            }
            else if (ChangeMotherboardRadio.Checked)
            {
                Motherboard changedMotherboard = AddInfoAboutMotherboard();
                if (SQLRequests.Warning(false, changedMotherboard.Name))
                {
                    SQLRequests.ChangeMotherboard(changedMotherboard);
                    SQLRequests.EditMotherboardMediator(changedMotherboard, "Edit");
                    int temp = MotherBoardList.IndexOf(MotherBoardList.Single(i => i.ID == changedMotherboard.ID));
                    MotherBoardList[temp] = changedMotherboard;
                    MessageBox.Show("Изменение прошло успешно!");
                    ViewInfoInDataGrid<Motherboard>(Motherboard_DatagridView, MotherBoardList);
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                MotherBoardList.Clear();
                int counter = 0;
                foreach (Motherboard c in db.Motherboard)
                {
                    MotherBoardList.Add(new Motherboard(c.ID));
                    MotherBoardList[counter].GetDataFromDB();
                    counter++;
                }
            }
            SystemFunctions.ClearMotherboardTextBoxes(this);
            MotherboardSocketComboBoxText = "";
            MotherboardChipsetComboBoxText = "";
            MotherboardFormFactorComboBoxText = "";
            MotherboardSupportedMemoryComboBoxText = "";
            MotherboardMemoryChanelsComboBoxText = "";
            MotherboardRAMFrequencyComboBoxText = "";

            this.ActiveControl = null;
        }

        private void Motherboard_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(ChangeMotherboardRadio.Checked)
                ViewMotherboardInfoToChange();
        }


        private void tabPage1_Enter(object sender, EventArgs e)
        {
            if(tabControl2.SelectedTab.Text != "Оперативная память")
                AutoScroll = true;
            //можно через потоки
            LoadInfoFromListToCombobox<CPU_series>(SeriesList, CPUSeriesComboBox);
            LoadInfoFromListToCombobox<CPU_codename>(CPUCodeNamesList, CPUCodeNameComboBox);
            LoadInfoFromListToCombobox<Sockets>(SocketsList, CPUSocketComboBox);
            LoadMemoryTypeInComboBox(CPUMemoryTypeComboBox, "RAM");
            LoadInfoFromListToCombobox<RAM_chanels>(RAMChanelsList, CPUChanelsComboBox);
            LoadRamFrequencyInComboBox(CPURamFrequaencyComboBox);

            LoadInfoFromListToCombobox<Connection_interfaces>(ConnectionInterfacesList, GPUInterfacesComboBox);
            LoadMemoryTypeInComboBox(GPUMemoryTypeComboBox, "GPU");
            LoadPowerTypeInComboBox(GPUPowerTypeComboBox);

            LoadInfoFromListToCombobox<Sockets>(SocketsList, MotherboardSocketComboBox);
            LoadInfoFromListToCombobox<Chipset>(ChipsetsList, MotherboardChipsetComboBox);
            LoadInfoFromListToCombobox<RAM_chanels>(RAMChanelsList, MotherBoardRAMChanelsComboBox);
            LoadFormFactorsInComboBox(MotherboardFormFactorComboBox, "Motherboard");
            LoadRamFrequencyInComboBox(MotherboardRamFrequencyComboBox);

            LoadFormFactorsInComboBox(CaseFormFactorComboBox, "Case");

            try
            {
                CPUSeriesComboBox.SelectedItem = CPUSeriesComboBoxText;
                CPUCodeNameComboBox.SelectedItem = CPUCodeNameComboBoxText;
                CPUSocketComboBox.SelectedItem = CPUSocketComboBoxText;
                CPUMemoryTypeComboBox.SelectedItem = CPUMemoryTypeComboBoxText;
                CPUChanelsComboBox.SelectedItem = CPUChanelsComboBoxText;
                if(CPUMemoryFrequencyComboBoxText != "")
                    CPURamFrequaencyComboBox.SelectedItem = Convert.ToInt32(CPUMemoryFrequencyComboBoxText);

                GPUPowerTypeComboBox.SelectedItem = GPUpowerTypeComboBoxText;
                GPUMemoryTypeComboBox.SelectedItem = GPUTypeComboBoxText;
                GPUInterfacesComboBox.SelectedItem = GPUconnectionInterfaceComboBoxText;

                MotherboardSocketComboBox.SelectedItem = MotherboardSocketComboBoxText;
                MotherboardChipsetComboBox.SelectedItem = MotherboardChipsetComboBoxText;
                MotherboardFormFactorComboBox.SelectedItem = MotherboardFormFactorComboBoxText;
                MotherboardSupportedRAMComboBox.SelectedItem = MotherboardSupportedMemoryComboBoxText;
                MotherBoardRAMChanelsComboBox.SelectedItem = MotherboardMemoryChanelsComboBoxText;
                if(MotherboardRAMFrequencyComboBoxText != "")
                    MotherboardRamFrequencyComboBox.SelectedItem = Convert.ToInt32(MotherboardRAMFrequencyComboBoxText);

                CaseFormFactorComboBox.SelectedItem = CaseFormFactorComboBoxText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            AutoScroll = false;
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        private void Case_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ChangeCaseRadio.Checked)
                ViewCaseInfoToChange();
        }

        private void ViewCaseInfoToChange()
        {
            if (Case_DatagridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = Case_DatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = Case_DatagridView.Rows[selectedrowindex];
                Case pcCase = new Case();
                pcCase = CaseList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
                CaseIDTextBox.Text = pcCase.ID;
                CaseNameTextBox.Text = pcCase.Name;
                CasePSUTextBox.Text = pcCase.Power_supply_unit;
                CasesGamingCheckBox.Checked = pcCase.Gaming;
                CasePSUPositionTextBox.Text = pcCase.PSU_position;
                CaseFormFactorComboBox.SelectedItem = (from b in FormFactorsList
                                                       where b.Device_type == "Case" && b.ID == pcCase.Form_factor_ID
                                                       select b.Name).Single();
                CaseMaterialTextBox.Text = pcCase.Material;
                CaseSupportedMotherboardsTextBox.Text = pcCase.Compatible_motherboard;
                CaseCoolingTypeTextBox.Text = pcCase.Cooling_type;
                CaseCoolersCountTextBox.Text = Convert.ToString(pcCase.Coolers_count);
                CaseCoolerSlotsCountTextBox.Text = Convert.ToString(pcCase.Coolers_slots);
                CaseHeightTextBox.Text = Convert.ToString(pcCase.Height);
                CaseWidthTextBox.Text = Convert.ToString(pcCase.Width);
                CaseDepthTextBox.Text = Convert.ToString(pcCase.Depth);
                CaseStorageLocationCountTextBox.Text = Convert.ToString(pcCase.Storage_sections_count);
                CaseWaterCoolingCheckBox.Checked = pcCase.Water_cooling_support;
                CaseExpansionSlotsCount.Text = Convert.ToString(pcCase.Expansion_slots_count);
                CaseCoolerInSetCheckBox.Checked = pcCase.Cooler_in_set;
                CaseMaxLengthOfGPUTextBox.Text = Convert.ToString(pcCase.Max_GPU_length);
                CaseSoundIsolationCheckBox.Checked = pcCase.Sound_isolation;
                CaseMaxCPUCoolerHeightTextBox.Text = Convert.ToString(pcCase.Max_CPU_cooler_height);
                CaseDustFiltersCheckBox.Checked = pcCase.Dust_filter;
                CaseMaxPSULengthTextBox.Text = Convert.ToString(pcCase.Max_PSU_length);
                CaseWeightTextBox.Text = Convert.ToString(pcCase.Weight);
            }
        }

        private void AddCaseRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(AddCaseRadio.Checked)
            {
                SystemFunctions.ClearCaseTextBoxes(this);
                SystemFunctions.SetEditOrAddButtonMode(ActWithCase, true);
                SystemFunctions.ChangeCaseTextBoxesEnable(this, true);
                FindCaseIDButton.Enabled = true;
            }
        }

        private void ChangeCaseRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(ChangeCaseRadio.Checked)
            {
                SystemFunctions.ChangeCaseTextBoxesEnable(this, true);

                FindCaseIDButton.Enabled = false;
                CaseIDTextBox.Enabled = false;
                SystemFunctions.SetEditOrAddButtonMode(ActWithCase, false);
                ViewCaseInfoToChange();
            }
        }

        private void FindCaseIDButton_Click(object sender, EventArgs e)
        {
            SearchInfoInList<Case>(CaseList, CaseIDTextBox, ActWithCase);
        }

        private void ActWithCase_Click(object sender, EventArgs e)
        {
            if (SystemFunctions.CheckNullForCaseTextBoxes(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            else
            {
                if (!SystemFunctions.CheckNumConvertForCaseTextBoxes(this))
                {
                    MessageBox.Show("Не все поля можно привести к числовому типу");
                    return;
                }
            }
            if (AddCaseRadio.Checked)
            {
                Case newCase = AddInfoAboutCase();
                if (SQLRequests.Warning(true, newCase.Name))
                {
                    SQLRequests.AddCase(newCase);
                    SQLRequests.EditCaseMediator(newCase, "Add");
                    CaseList.Add(newCase);
                    MessageBox.Show("Добавление прошло успешно!");
                    ViewInfoInDataGrid<Case>(Case_DatagridView, CaseList);
                }
            }
            else if (ChangeCaseRadio.Checked)
            {
                Case changedCase = AddInfoAboutCase();
                if (SQLRequests.Warning(false, changedCase.Name))
                {
                    SQLRequests.ChangeCase(changedCase);
                    SQLRequests.EditCaseMediator(changedCase, "Edit");
                    int temp = CaseList.IndexOf(CaseList.Single(i => i.ID == changedCase.ID));
                    CaseList[temp] = changedCase;
                    MessageBox.Show("Изменение прошло успешно!");
                    ViewInfoInDataGrid<Case>(Case_DatagridView, CaseList);
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                CaseList.Clear();
                int counter = 0;
                foreach (Case c in db.Case)
                {
                    CaseList.Add(new Case(c.ID));
                    CaseList[counter].GetDataFromDB();
                    counter++;
                }
            }
            CaseFormFactorComboBoxText = "";
            SystemFunctions.ClearCaseTextBoxes(this);
            this.ActiveControl = null;
        }

        private Case AddInfoAboutCase()
        {
            Case newCase = new Case();

            newCase.ID = CaseIDTextBox.Text;
            newCase.Name = CaseNameTextBox.Text;
            newCase.Power_supply_unit = CasePSUTextBox.Text;
            newCase.Gaming = CasesGamingCheckBox.Checked;
            newCase.PSU_position = CasePSUPositionTextBox.Text;
            newCase.Form_factor_ID = (from b in FormFactorsList
                                      where b.Device_type == "Case" && b.Name == CaseFormFactorComboBox.Text
                                      select b.ID).Single();

            newCase.Material = CaseMaterialTextBox.Text;
            newCase.Compatible_motherboard = CaseSupportedMotherboardsTextBox.Text;
            newCase.Cooling_type = CaseCoolingTypeTextBox.Text;
            newCase.Coolers_count = Convert.ToInt32(CaseCoolersCountTextBox.Text);
            newCase.Coolers_slots = Convert.ToInt32(CaseCoolerSlotsCountTextBox.Text);
            newCase.Height = Convert.ToInt32(CaseHeightTextBox.Text);
            newCase.Width = Convert.ToInt32(CaseWidthTextBox.Text);
            newCase.Depth = Convert.ToInt32(CaseDepthTextBox.Text);
            newCase.Storage_sections_count = Convert.ToInt32(CaseStorageLocationCountTextBox.Text);
            newCase.Water_cooling_support = CaseWaterCoolingCheckBox.Checked;
            newCase.Expansion_slots_count = Convert.ToInt32(CaseExpansionSlotsCount.Text);
            newCase.Cooler_in_set = CaseCoolerInSetCheckBox.Checked;
            newCase.Max_GPU_length = Convert.ToInt32(CaseMaxLengthOfGPUTextBox.Text);
            newCase.Sound_isolation = CaseSoundIsolationCheckBox.Checked;
            newCase.Max_CPU_cooler_height = Convert.ToInt32(CaseMaxCPUCoolerHeightTextBox.Text);
            newCase.Dust_filter = CaseDustFiltersCheckBox.Checked;
            newCase.Max_PSU_length = Convert.ToInt32(CaseMaxPSULengthTextBox.Text);
            newCase.Weight = float.Parse(CaseWeightTextBox.Text);
            return newCase;
        }


        private void CPUSeriesComboBox_Leave(object sender, EventArgs e)
        {
            CPUSeriesComboBoxText = CPUSeriesComboBox.Text;
        }

        private void CPUCodeNameComboBox_Leave(object sender, EventArgs e)
        {
            CPUCodeNameComboBoxText = CPUCodeNameComboBox.Text;
        }

        private void CPUSocketComboBox_Leave(object sender, EventArgs e)
        {
            CPUSocketComboBoxText = CPUSocketComboBox.Text;
        }

        private void CPUMemoryTypeComboBox_Leave(object sender, EventArgs e)
        {
            CPUMemoryTypeComboBoxText = CPUMemoryTypeComboBox.Text;
        }

        private void CPUChanelsComboBox_Leave(object sender, EventArgs e)
        {
            CPUChanelsComboBoxText = CPUChanelsComboBox.Text;
        }

        private void CPURamFrequaencyComboBox_Leave(object sender, EventArgs e)
        {
            CPUMemoryFrequencyComboBoxText = CPURamFrequaencyComboBox.Text;
        }

        private void GPUInterfacesComboBox_Leave(object sender, EventArgs e)
        {
            GPUconnectionInterfaceComboBoxText = GPUInterfacesComboBox.Text;
        }

        private void GPUMemoryTypeComboBox_Leave(object sender, EventArgs e)
        {
            GPUTypeComboBoxText = GPUMemoryTypeComboBox.Text;
        }

        private void GPUPowerTypeComboBox_Leave(object sender, EventArgs e)
        {
            GPUpowerTypeComboBoxText = GPUPowerTypeComboBox.Text;
        }

        private void MotherboardSocketComboBox_Leave(object sender, EventArgs e)
        {
            MotherboardSocketComboBoxText = MotherboardSocketComboBox.Text;
        }

        private void MotherboardChipsetComboBox_Leave(object sender, EventArgs e)
        {
            MotherboardChipsetComboBoxText = MotherboardChipsetComboBox.Text;
        }

        private void MotherboardFormFactorComboBox_Leave(object sender, EventArgs e)
        {
            MotherboardFormFactorComboBoxText = MotherboardFormFactorComboBox.Text;
        }

        private void MotherboardSupportedRAMComboBox_Leave(object sender, EventArgs e)
        {
            MotherboardSupportedMemoryComboBoxText = MotherboardSupportedRAMComboBox.Text;       
        }

        private void MotherBoardRAMChanelsComboBox_Leave(object sender, EventArgs e)
        {
            MotherboardMemoryChanelsComboBoxText = MotherBoardRAMChanelsComboBox.Text;
        }

        private void MotherboardRamFrequencyComboBox_Leave(object sender, EventArgs e)
        {
            MotherboardRAMFrequencyComboBoxText = MotherboardRamFrequencyComboBox.Text;
        }

        private void CaseFormFactorComboBox_Leave(object sender, EventArgs e)
        {
            CaseFormFactorComboBoxText = CaseFormFactorComboBox.Text;
        }
    }
}