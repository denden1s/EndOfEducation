using Computer_house.DataBase.Entities;
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
        private List<Holding_document> HoldingDocuments = new List<Holding_document>();
        //сведения о процессоре


        public ComponentsOptionsForm()
        {
            InitializeComponent();
        }
        public ComponentsOptionsForm(Users _user,List<Warehouse_info> _wareHouse, List<CPU> _cpus)
        {
            InitializeComponent();
            //WarehouseInfo = _wareHouse;
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
            FindCPUIDButton.Enabled = false;
            FindGPUIDButton.Enabled = false;
            SetButtonsDefaultOptions(ActToComponent, ActWithCPU, ActWithGPU);

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
            ViewCPUInfoInDataGrid();
            LoadSeriesInfoInComboBox();
            LoadCodeNameInfoInComboBox();
            LoadSocketInfoInComboBox(CPUSocketComboBox);
            LoadMemoryTypeInComboBox(CPUMemoryTypeComboBox, "RAM");
            LoadMemoryChanelsInComboBox(CPUChanelsComboBox);
            LoadRamFrequencyInComboBox(CPURamFrequaencyComboBox);
            SetEnableStatusOfCPUTextBoxes(false);
            ChangeGPUTextBoxesEnable(false);
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
            SetButtonsDefaultOptions(ActToComponent);
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
                SetEditOrAddButtonMode(ActToComponent, false);
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
                SetEditOrAddButtonMode(ActToComponent, true);

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
            
        private void LockButtonInSecondTab()
        {
            if ((ComponentNameTextBox.Text == "")||((AddNewComponent.Checked == false)&&(EditComponent.Checked == false)))
                ActToComponent.Enabled = false;
            else
                ActToComponent.Enabled = true;
        }

        //Нужны для того, чтобы отобразить данные в списке
        //для раздела с доп сведениями о комплектующих
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
            CPU_DatagridView.Rows.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (CPU i in CPUList)
                {
                    CPU_DatagridView.Rows.Add(i.ID, i.Name);
                }
            }
        }

        private void AddCPURadio_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableStatusOfCPUTextBoxes(true);
            SetEditOrAddButtonMode(ActWithCPU, true);
            FindCPUIDButton.Enabled = true;
            ClearCPUInfoInTextBoxes();
        }

        private void ChangeCPURadio_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableStatusOfCPUTextBoxes(true);
            SetEditOrAddButtonMode(ActWithCPU, false);
            CPUIDTextBox.Enabled = false;
            FindCPUIDButton.Enabled = false;
            ViewCPUInfoToChange();
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
            if (AddCPURadio.Checked)
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
        private bool CheckNullForCPUTextBoxes()
        {
            if(CPUIDTextBox.TextLength == 0)
                return true;
            if(CPUNameTextBox.TextLength == 0)
                return true;
            if(CPUSeriesComboBox.Text == "")
                return true;
            if (DeliveryTypeComboBox.Text == "")
                return true;
            if (CPUCodeNameComboBox.Text == "")
                return true;
            if(CPUSocketComboBox.Text == "")
                return true;
            if (CPUCoresTextBox.TextLength == 0)
                return true;
            if (CPUBaseStateTextBox.TextLength == 0)
                return true;
            if (CPUMaxStateTextBox.TextLength == 0)
                return true;
            if (CPUMemoryTypeComboBox.Text == "")
                return true;
            if (CPUChanelsComboBox.Text == "")
                return true;
            if (CPURamFrequaencyComboBox.Text == "")
                return true;
            if (CPUTDPTextBox.TextLength == 0)
                return true;
            if (CPUTechprocessTextBox.TextLength == 0)
                return true;
            return false;
        }
        //Нужен для проверки на наличие числовых значений в необходимых полях
        private bool CheckIntParseForCPUTextBoxes()
        {
            int res;
            bool isInt = Int32.TryParse(CPUCoresTextBox.Text, out res);
            //true - если распарсит
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUBaseStateTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUMaxStateTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUTDPTextBox.Text, out res);
            if (!isInt)
                return false;
            isInt = Int32.TryParse(CPUTechprocessTextBox.Text, out res);
            if (!isInt)
                return false;
            return true;
        }

        private void ActWithCPU_Click(object sender, EventArgs e)
        {
            bool exception = false;
            if (CheckNullForCPUTextBoxes())
            {
                exception = true;
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                if (!CheckIntParseForCPUTextBoxes())
                {
                    exception = true;
                    MessageBox.Show("Не все значения можно привести к числовому виду");
                }
            }
            if(!exception)
            {
                if (AddCPURadio.Checked)
                {
                    //добавление работает
                    CPU newCPU = AddInfoAboutCPU();
                    //Добавление процессора
                    if(SQLRequests.Warning(true, newCPU.Name))
                    {
                        SQLRequests.AddCPU(newCPU);
                        SQLRequests.EditCPUInMediator(newCPU, "Add");
                        //добавление в таблицу mediator
                        //Добавить в warehouseInfo
                        CPUList.Add(newCPU);
                        ViewCPUInfoInDataGrid();
                        ClearCPUInfoInTextBoxes();
                    }
                }
                else if(ChangeCPURadio.Checked)
                {
                    //изменение
                    CPU changedCPU = AddInfoAboutCPU();
                    if (SQLRequests.Warning(false, changedCPU.Name))
                    {
                        SQLRequests.ChangeCPU(changedCPU);
                        SQLRequests.EditCPUInMediator(changedCPU, "Edit");
                        ViewCPUInfoInDataGrid();
                    }
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

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            LoadSeriesInfoInComboBox();
            LoadCodeNameInfoInComboBox();
            LoadSocketInfoInComboBox(CPUSocketComboBox);
            LoadMemoryTypeInComboBox(CPUMemoryTypeComboBox, "RAM");
            LoadMemoryChanelsInComboBox(CPUChanelsComboBox);
            LoadRamFrequencyInComboBox(CPURamFrequaencyComboBox);
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ChangeCPURadio.Checked)
            {
                ViewCPUInfoToChange();
                ActWithCPU.Enabled = true;
            } 
        }
        private void ClearCPUInfoInTextBoxes()
        {
            CPUIDTextBox.Clear();
            CPUNameTextBox.Clear();
            CPUSeriesComboBox.SelectedItem = null;
            DeliveryTypeComboBox.SelectedItem = null;
            CPUCodeNameComboBox.SelectedItem = null;
            CPUSocketComboBox.SelectedItem = null;
            CPUCoresTextBox.Clear();
            MultithreadingCheckBox.Checked = false;
            CPUBaseStateTextBox.Clear();
            CPUMaxStateTextBox.Clear();
            CPUMemoryTypeComboBox.SelectedItem = null;
            CPUChanelsComboBox.SelectedItem = null;
            CPURamFrequaencyComboBox.SelectedItem = null;
            CPUIntegratedGraphicCheckBox.Checked = false;
            CPUTDPTextBox.Clear();
            CPUTechprocessTextBox.Clear();
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
            return changedCPU;
        }

        private void SetEnableStatusOfCPUTextBoxes(bool _status)
        {
            CPUIDTextBox.Enabled = _status;
            CPUNameTextBox.Enabled = _status;
            CPUSeriesComboBox.Enabled = _status;
            DeliveryTypeComboBox.Enabled = _status;
            CPUCodeNameComboBox.Enabled = _status;
            CPUSocketComboBox.Enabled = _status;
            CPUCoresTextBox.Enabled = _status;
            MultithreadingCheckBox.Enabled = _status;
            CPUBaseStateTextBox.Enabled = _status;
            CPUMaxStateTextBox.Enabled = _status;
            CPUMemoryTypeComboBox.Enabled = _status;
            CPUChanelsComboBox.Enabled = _status;
            CPURamFrequaencyComboBox.Enabled = _status;
            CPUIntegratedGraphicCheckBox.Enabled = _status;
            CPUTDPTextBox.Enabled = _status;
            CPUTechprocessTextBox.Enabled = _status;
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
            bool findGPU = false;
            foreach (GPU cp in GPUList)
            {
                if (CPUIDTextBox.Text == cp.ID)
                    findGPU = true;
            }
            if (findGPU)
            {
                MessageBox.Show("Такой серийный номер присутствует в базе");
            }
            else
            {
                MessageBox.Show("Такой серийный номер отсутствует в базе");
                ActWithGPU.Enabled = true;
            }
        }

        //Нужен для установки стартового значения для кнопок
        private void SetButtonsDefaultOptions(params Button[] _buttons)
        {
            foreach (var i in _buttons)
            {
                i.Enabled = false;
                i.Text = "";
                i.BackColor = Color.Transparent;
            }
        }

        //Нужен для изменения кнопки в зависимости от режима добавления или изменения
        private void SetEditOrAddButtonMode(Button _button, bool _workMode)
        {
            //true - добавление
            //false - изменение
            if(_workMode)
            {
                _button.Text = "Добавить";
                _button.Enabled = true;
                _button.BackColor = Color.Green;
            }
            else
            {
                _button.Enabled = true;
                _button.Text = "Изменить";
                _button.BackColor = Color.BlueViolet;
            }
        }

        private void AddGPURadio_CheckedChanged(object sender, EventArgs e)
        {
            SetEditOrAddButtonMode(ActWithGPU, true);

            ChangeGPUTextBoxesEnable(true);
            FindGPUIDButton.Enabled = true;
            ClearGPUInfoTextBoxex();

        }

        private void ClearGPUInfoTextBoxex()
        {
            GPUIDTextBox.Clear();
            GPUNameTextBox.Clear();
            GPUInterfacesComboBox.SelectedItem = null;
            GPUManufactureTextBox.Clear();
            GPUSLISupportCheckBox.Checked = false;
            GPUCapacityTextBox.Clear();
            GPUMemoryTypeComboBox.SelectedItem = null;
            GPUBusWidthTextBox.Clear();
            GPUOverclockingCheckBox.Checked = false;
            GPUEnergyConsumptTextBox.Clear();
            GPUDirectXVersionTextBox.Clear();
            GPUOutputInterfacesTextBox.Clear();
            GPUPowerTypeComboBox.SelectedItem = null;
            GPUCoolersCountTextBox.Clear();
            GPUCoolingSysThicknessTextBox.Clear();
            GPUWidthTextBox.Clear();
            GPUHeightTextBox.Clear();
        }

        private void ChangeGPUTextBoxesEnable(bool _status)
        {
            GPUIDTextBox.Enabled = _status;
            GPUNameTextBox.Enabled = _status;
            GPUInterfacesComboBox.Enabled = _status;
            GPUManufactureTextBox.Enabled = _status;
            GPUSLISupportCheckBox.Enabled = _status;
            GPUCapacityTextBox.Enabled = _status;
            GPUMemoryTypeComboBox.Enabled = _status;
            GPUBusWidthTextBox.Enabled = _status;
            GPUOverclockingCheckBox.Enabled = _status;
            GPUEnergyConsumptTextBox.Enabled = _status;
            GPUDirectXVersionTextBox.Enabled = _status;
            GPUOutputInterfacesTextBox.Enabled = _status;
            GPUPowerTypeComboBox.Enabled = _status;
            GPUCoolersCountTextBox.Enabled = _status;
            GPUCoolingSysThicknessTextBox.Enabled = _status;
            GPUWidthTextBox.Enabled = _status;
            GPUHeightTextBox.Enabled = _status;
        }

        private void ChangeGPURadio_CheckedChanged(object sender, EventArgs e)
        {
            ChangeGPUTextBoxesEnable(true);
            FindGPUIDButton.Enabled = false;
            GPUIDTextBox.Enabled = false;
            SetEditOrAddButtonMode(ActWithGPU, false);
        }

        private bool CheckForNullGPUTextBoxes()
        {
            bool isNull = false;
            if (GPUIDTextBox.TextLength == 0)
                return true;
            if(GPUNameTextBox.TextLength == 0)
                return true;
            if (GPUInterfacesComboBox.Text == "")
                return true;
            if (GPUManufactureTextBox.TextLength == 0)
                return true;
            if (GPUCapacityTextBox.TextLength == 0)
                return true;
            if (GPUMemoryTypeComboBox.Text == "")
                return true;
            if (GPUBusWidthTextBox.TextLength == 0)
                return true;
            if (GPUEnergyConsumptTextBox.TextLength == 0)
                return true;
            if (GPUDirectXVersionTextBox.TextLength == 0)
                return true;
            if (GPUOutputInterfacesTextBox.TextLength == 0)
                return true;
            if (GPUPowerTypeComboBox.Text == "")
                return true;
            if (GPUCoolersCountTextBox.TextLength == 0)
                return true;
            if (GPUCoolingSysThicknessTextBox.TextLength == 0)
                return true;
            if (GPUWidthTextBox.TextLength == 0)
                return true;
            if (GPUHeightTextBox.TextLength == 0)
                return true;

            return isNull;
        }

        private void ActWithGPU_Click(object sender, EventArgs e)
        {
            bool exception = false;
            if (CheckForNullGPUTextBoxes())
            {
                MessageBox.Show("Не все поля заполнены");
                exception = true;
            }
            else
            {
                //проверка на перевод к числовому типу
            }
            if(!exception)
            {
                if (AddGPURadio.Checked)
                {
                    //добавление
                }
                else if (ChangeGPURadio.Checked)
                {
                    //изменение
                }
            }
            
        }
    }
}