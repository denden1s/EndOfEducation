using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
  public partial class ComponentsOptionsForm : Form
  {
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
    private List<CPU> CPUList = new List<CPU>();
    private List<GPU> GPUList = new List<GPU>();
    private List<Motherboard> MotherBoardList = new List<Motherboard>();
    private List<Case> CaseList = new List<Case>();
    private List<RAM> RAMList = new List<RAM>();
    private List<Cooling_system> CoolingSystemList = new List<Cooling_system>();
    private List<PSU> PSUList = new List<PSU>();
    private List<Storage_devices> StorageDevicesList = new List<Storage_devices>();
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
    private string RAMTypeComboBoxText = "";
    private string RAMFrequencyComboBoxText = "";
    private string CoolingSystemPowerTypeText = "";
    private string PSUPowerTypeText = "";
    private string SDFormFactorText = "";
    private string SDConnectionInterfaceText = "";

    private bool othersTabsLostFocus = false;

    public ComponentsOptionsForm()
    {
      InitializeComponent();
    }
    public ComponentsOptionsForm(Users _user, List<CPU> _cpus, List<GPU> _gpus, List<Motherboard> _motherboards, 
      List<Case> _cases, List<RAM> _rams, List<Cooling_system> _coolingSys, List<PSU> _psus, List<Storage_devices> _sd)
    {
      InitializeComponent();
      GPUList = _gpus;
      CPUList = _cpus;
      MotherBoardList = _motherboards;
      user = _user;
      CaseList = _cases;
      RAMList = _rams;
      CoolingSystemList = _coolingSys;
      PSUList = _psus;
      StorageDevicesList = _sd;
    }

    private void AddComponentsOptionsForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      authorizedForm = new AuthorizedForm(user);
      this.Hide();
      authorizedForm.Show();
    }

    private async void AddComponentsOptionsForm_Load(object sender, EventArgs e)
    {
      //Настройки перед загрузкой формы
      SystemFunctions.SetVisibleLables(this, false);
      SystemFunctions.ChangeButtonEnable(false, FindCPUIDButton, FindGPUIDButton,
        FindMotherboardIDButton, FindCaseIDButton, FindRAMIDButton, FindCoolingSystemIDButton,
        FindPSUIDButton, FindSDIDButton);

      SystemFunctions.SetButtonsDefaultOptions(ActToComponent, ActWithCPU, ActWithGPU, ActWithMotherboard,
          ActWithCase, ActWithRAM, ActWithCoolingSystem,ActWithPSU, ActWithSD);

      Task[] tasks =
      {
        Task.Run(() => LoadHoldingDocsFromDB()),Task.Run(() => LoadCPUCodeNameFromDB()),
        Task.Run(() => LoadSocketInfoFromDB()),Task.Run(() => LoadChipsetInfoFromDB()),
        Task.Run(() => LoadChanelInfoFromDB()),Task.Run(() => LoadFrequensyInfoFromDB()),
        Task.Run(() => LoadFormFactorsFromDB()), Task.Run(() => LoadMemoryTypesFromDB()),
        Task.Run(() => LoadConnectionInterfacesFromDB()), Task.Run(() => LoadPowerConnectorsFromDB()),
        Task.Run(() => LoadCPUSeriesFromDB())
      };
      await Task.WhenAll(tasks);

      ViewInfoInDataGrid<CPU>(CPU_DatagridView, CPUList);
      ViewInfoInDataGrid<GPU>(GPU_DatagridView, GPUList);
      ViewInfoInDataGrid<Motherboard>(Motherboard_DatagridView, MotherBoardList);
      ViewInfoInDataGrid<Case>(Case_DatagridView, CaseList);
      ViewInfoInDataGrid<RAM>(RAM_DatagridView, RAMList);
      ViewInfoInDataGrid<Cooling_system>(CoolingSystem_DatagridView, CoolingSystemList);
      ViewInfoInDataGrid<PSU>(PSU_DatagridView, PSUList);
      ViewInfoInDataGrid(SD_DatagridView, StorageDevicesList);

      SystemFunctions.ChangeCPUTextBoxesEnable(this, false);
      SystemFunctions.ChangeMotherboardTextBoxesEnable(this, false);
      SystemFunctions.ChangeCaseTextBoxesEnable(this, false);
      SystemFunctions.ChangeGPUTextBoxesEnable(this, false);
      SystemFunctions.ChangeRAMTextBoxesEnable(this, false);
      SystemFunctions.ChangeCoolingSystemTextBoxesEnable(this, false);
      SystemFunctions.ChangePSUTextBoxesEnable(this, false);
      SystemFunctions.ChangeSDTextBoxesEnable(this, false);

      await Task.Run(() => ViewDocsInDataGrid());

      await Task.Run(() => ViewInfoFromListsToFormElements(this));
      ViewSecondPartDataFromListsToFormEl(this);
      Task.Run(() => UpdateInfo());
    }

    //Методы для загрузки данных из БД
    private void LoadCPUSeriesFromDB()
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
          SeriesList = db.CPU_series.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadCPUCodeNameFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          CPUCodeNamesList = db.CPU_codename.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadSocketInfoFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          SocketsList = db.Sockets.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadChipsetInfoFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          ChipsetsList = db.Chipset.ToList(); 
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadChanelInfoFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          RAMChanelsList = db.RAM_chanels.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadFrequensyInfoFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          RAMFrequencyList = db.RAM_frequency.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadFormFactorsFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          FormFactorsList = db.Form_factors.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadMemoryTypesFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          MemoryTypesList = db.Memory_types.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadConnectionInterfacesFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          ConnectionInterfacesList = db.Connection_interfaces.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadPowerConnectorsFromDB()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          PowerConnectorsList = db.Power_connectors.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadHoldingDocsFromDB()
    {
      HoldingDocuments.Clear();
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          HoldingDocuments = db.Holding_document.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
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
      CoolingSystemPowerTypeText = CoolingSystemPowerTypeComboBox.Text;
      CaseFormFactorComboBoxText = CaseFormFactorComboBox.Text;
      RAMFrequencyComboBoxText = RAMFrequencyComboBox.Text;
      RAMTypeComboBoxText = RAMTypeComboBox.Text;
      PSUPowerTypeText = PSUMotherboardPowerTypeComboBox.Text;
      SDFormFactorText = SDFormFactorComboBox.Text;
      SDConnectionInterfaceText = SDConnectionInterfaceComboBox.Text;
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
          ViewInfoInListBox(ComponentsListBox, SeriesList);
          break;
        case 1:
          ViewInfoInListBox(ComponentsListBox, CPUCodeNamesList);
          break;
        case 2:
          ViewInfoInListBox(ComponentsListBox, SocketsList);
          break;
        case 3:
          ViewInfoInListBox(ComponentsListBox, ChipsetsList);
          break;
        case 4:
          ViewInfoInListBox(ComponentsListBox, RAMChanelsList);
          break;
        case 5:
          ViewFrequencyInfoToListBox();
          break;
        case 6:
          ViewInfoInListBox(ComponentsListBox, FormFactorsList);
          SystemFunctions.SetVisibleLables(this, true);
          ComponentTypeComboBox.Items.Clear();
          ComponentTypeComboBox.Items.AddRange(new string[] { "Motherboard", "Case", "SD" });
          break;
        case 7:
          ViewInfoInListBox(ComponentsListBox, MemoryTypesList);
          SystemFunctions.SetVisibleLables(this, true);
          ComponentTypeComboBox.Items.Clear();
          ComponentTypeComboBox.Items.AddRange(new string[] { "RAM", "GPU" });
          break;
        case 8:
          ViewInfoInListBox(ComponentsListBox, ConnectionInterfacesList);
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
      if (EditComponent.Checked)
      {
        if (TypesOfComponentComboBox.Text == "")
          EditComponent.Checked = false;
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
      if (AddNewComponent.Checked)
      {
        if (TypesOfComponentComboBox.Text == "")
          AddNewComponent.Checked = false;
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
                ViewInfoInListBox(ComponentsListBox, SeriesList);
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
                ViewInfoInListBox(ComponentsListBox, CPUCodeNamesList);
              }
              break;
            case 2:
              Sockets newSocket = new Sockets(ComponentNameTextBox.Text);
              if (ComponentsListBox.Items.Contains(newSocket.Name))
                exception = true;
              else
              {
                SQLRequests.AddSocket(newSocket, true);
                ViewInfoInListBox(ComponentsListBox, SocketsList);
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
                ViewInfoInListBox(ComponentsListBox, ChipsetsList);
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
                ViewInfoInListBox(ComponentsListBox, RAMChanelsList);
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
                ViewInfoInListBox(ComponentsListBox, FormFactorsList);
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
                ViewInfoInListBox(ComponentsListBox, MemoryTypesList);
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
                ViewInfoInListBox(ComponentsListBox, ConnectionInterfacesList);
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
              ViewInfoInListBox(ComponentsListBox, SeriesList);
              break;
            case 1:
              CPUCodeNamesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              SQLRequests.ChangeCodeName(CPUCodeNamesList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, CPUCodeNamesList);
              break;
            case 2:
              SocketsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              SQLRequests.ChangeSocketInfo(SocketsList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, SocketsList);
              break;
            case 3:
              ChipsetsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              SQLRequests.ChangeChipsetInfo(ChipsetsList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, ChipsetsList);
              break;
            case 4:
              RAMChanelsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              SQLRequests.ChangeChanelInfo(RAMChanelsList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, RAMChanelsList);
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
                MessageBox.Show("Строка не является числом");
              break;
            case 6:
              FormFactorsList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              FormFactorsList[ComponentsListBox.SelectedIndex].Device_type = ComponentTypeComboBox.Text;
              SQLRequests.ChangeFormFactors(FormFactorsList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, FormFactorsList);
              break;
            case 7:
              MemoryTypesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              MemoryTypesList[ComponentsListBox.SelectedIndex].Device_type = ComponentTypeComboBox.Text;
              SQLRequests.ChangeMemoryType(MemoryTypesList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, MemoryTypesList);
              break;
            case 8:
              ConnectionInterfacesList[ComponentsListBox.SelectedIndex].Name = ComponentNameTextBox.Text;
              SQLRequests.ChangeConnectionInterface(ConnectionInterfacesList[ComponentsListBox.SelectedIndex], false);
              ViewInfoInListBox(ComponentsListBox, ConnectionInterfacesList);
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
      if (EditComponent.Checked)
      {
        ComponentNameTextBox.Text = Convert.ToString(ComponentsListBox.Items[ComponentsListBox.SelectedIndex]);
        ComponentTypeComboBox.Items.Clear();
        if (TypesOfComponentComboBox.SelectedIndex == 6)
        {
          ComponentTypeComboBox.Items.AddRange(new string[] { "Motherboard", "Case", "SD" });
          switch (FormFactorsList[ComponentsListBox.SelectedIndex].Device_type)
          {
            case "Motherboard":
              ComponentTypeComboBox.SelectedIndex = 0;
              break;
            case "Case":
              ComponentTypeComboBox.SelectedIndex = 1;
              break;
            case "SD":
              ComponentTypeComboBox.SelectedIndex = 2;
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
    private void ViewInfoInListBox<T>(ListBox _listBox, List<T> _list) where T : ProductWithOnlyName
    {
      _listBox.Items.Clear();
      foreach (var i in _list)
          _listBox.Items.Add(i.Name);
    }
    private void ViewFrequencyInfoToListBox()
    {
      ComponentsListBox.Items.Clear();
      foreach (var i in RAMFrequencyList)
        ComponentsListBox.Items.Add(Convert.ToString(i.Frequency));
    }
    private void ViewPowerConnectorsToListBox()
    {
      ComponentsListBox.Items.Clear();
      foreach (var i in PowerConnectorsList)
        ComponentsListBox.Items.Add(i.Connectors);
    }


    //Нужен для загрузки сведений  в таблицы
    private void ViewInfoInDataGrid<T>(DataGridView _datagrid, List<T> _list) where T : Product
    {
      try
      {
        _datagrid.Rows.Clear();
        foreach (T i in _list)
          _datagrid.Rows.Add(i.ID, i.Name);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void AddCPURadio_CheckedChanged(object sender, EventArgs e)
    {
      if (AddCPURadio.Checked)
      {
        SystemFunctions.ChangeCPUTextBoxesEnable(this, true);
        SystemFunctions.SetEditOrAddButtonMode(ActWithCPU, true);
        FindCPUIDButton.Enabled = true;
        SystemFunctions.ClearCPUTextBoxes(this);
      }
    }

    private void ChangeCPURadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangeCPURadio.Checked)
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
      SearchInfoInList(CPUList, CPUIDTextBox, ActWithCPU);
    }

    //Нужен для поиска id перед добавлением данных о товаре
    private void SearchInfoInList<T>(List<T> _list, TextBox _textbox, Button _button) where T : Product
    {
      try
      {
        bool findItem = false;
        foreach (T i in _list)
          if (_textbox.Text == i.ID)
              findItem = true;

        if (findItem)
          MessageBox.Show("Такой серийный номер присутствует в базе");
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
          FindInfoFromTextBox(CPUIDTextBox, CPUList, ActWithCPU);
        }
      }
    }

    //Необходим для проверки наличия процессора в базе по уникальному идентификатору
    private void FindInfoFromTextBox<T>(TextBox _searchTextBox, List<T> _list, Button _actionButton) where T : Product
    {
      var findInfo = _list.SingleOrDefault(i => i.ID.ToLower() == _searchTextBox.Text.ToLower());
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
          newCPU.GetDataFromDB();
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
      SystemFunctions.ClearCPUTextBoxes(this);
      this.ActiveControl = null;
    }

    //Необходим для загрузки данных из списков в выпадающие списки
    private void LoadInfoFromListToCombobox<T>(List<T> _list, ComboBox _combobox) where T : ProductWithOnlyName
    {
      _combobox.Items.Clear();
      foreach (var i in _list)
        _combobox.Items.Add(i.Name);
    }

    private void LoadMemoryTypeInComboBox(ComboBox _comboBox, string _deviceType)
    {
      _comboBox.Items.Clear();
      List<Memory_types> tempMemoryTypes = (from b in MemoryTypesList
                                          where b.Device_type == _deviceType
                                          select b).ToList();
      foreach (var i in tempMemoryTypes)
        _comboBox.Items.Add(i.Name);
    }

    private void LoadFormFactorsInComboBox(ComboBox _comboBox, string _deviceType)
    {
      _comboBox.Items.Clear();
      List<Form_factors> form_Factors = (from b in FormFactorsList
                                        where b.Device_type == _deviceType
                                        select b).ToList();
      foreach (var i in form_Factors)
        _comboBox.Items.Add(i.Name);
    }
    private void LoadRamFrequencyInComboBox(ComboBox _comboBox)
    {
      _comboBox.Items.Clear();
      foreach (var i in RAMFrequencyList)
        _comboBox.Items.Add(i.Frequency);
    }

    //Аналогично вышеописанным методам только для списка видеокарт
    private void LoadPowerTypeInComboBox(ComboBox _combobox)
    {
      _combobox.Items.Clear();
      foreach (var i in PowerConnectorsList)
        _combobox.Items.Add(i.Connectors);
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

    private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (ChangeCPURadio.Checked)
      {
        ViewCPUInfoToChange();
        ActWithCPU.Enabled = true;
      }
    }

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

    private void ViewDocsInDataGrid()
    {
      HoldingDocsDatagridView.Rows.Clear();
      foreach (var i in HoldingDocuments)
      {
        i.GetDataFromDB();
        string name;
        using (ApplicationContext db = new ApplicationContext())
          name = db.Users.Single(b => b.ID == i.User_ID).Name;

        HoldingDocsDatagridView.Rows.Add(i.ID, i.Product_name, i.Time, i.State, i.Items_count_in_move, name, i.Location_name);
      }
    }

    private void SearchGPUIDButton_Click(object sender, EventArgs e)
    {
      SearchInfoInList<GPU>(GPUList, GPUIDTextBox, ActWithGPU);
    }

    private void AddGPURadio_CheckedChanged(object sender, EventArgs e)
    {
      if (AddGPURadio.Checked)
      {
        SystemFunctions.SetEditOrAddButtonMode(ActWithGPU, true);
        SystemFunctions.ChangeGPUTextBoxesEnable(this, true);
        FindGPUIDButton.Enabled = true;
        SystemFunctions.ClearGPUTextBoxes(this);
      }
    }

    private void ChangeGPURadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangeGPURadio.Checked)
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
          newGPU.GetDataFromDB();
          GPUList.Add(newGPU);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid(GPU_DatagridView, GPUList);
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
          ViewInfoInDataGrid(GPU_DatagridView, GPUList);
        }
      }
      SystemFunctions.ClearGPUTextBoxes(this);
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
          FindInfoFromTextBox(GPUIDTextBox, GPUList, ActWithGPU);
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
      SearchInfoInList(MotherBoardList, MotherboardIDTextBox, ActWithMotherboard);
    }

    private void AddMotherboardRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (AddMotherboardRadio.Checked)
      {
        SystemFunctions.SetEditOrAddButtonMode(ActWithMotherboard, true);
        SystemFunctions.ChangeMotherboardTextBoxesEnable(this, true);
        FindMotherboardIDButton.Enabled = true;
        SystemFunctions.ClearMotherboardTextBoxes(this);
      }
    }

    private void ChangeMotherboardRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangeMotherboardRadio.Checked)
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
          newMotherboard.GetDataFromDB();
          MotherBoardList.Add(newMotherboard);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid(Motherboard_DatagridView, MotherBoardList);
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
          ViewInfoInDataGrid(Motherboard_DatagridView, MotherBoardList);
        }
      }
      SystemFunctions.ClearMotherboardTextBoxes(this);
      this.ActiveControl = null;
    }

    private void Motherboard_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (ChangeMotherboardRadio.Checked)
        ViewMotherboardInfoToChange();
    }

    private void ViewInfoFromListsToFormElements(ComponentsOptionsForm _form)
    {
      LoadInfoFromListToCombobox(_form.ConnectionInterfacesList, _form.GPUInterfacesComboBox);
      LoadMemoryTypeInComboBox(_form.GPUMemoryTypeComboBox, "GPU");
      LoadPowerTypeInComboBox(_form.GPUPowerTypeComboBox);
      LoadInfoFromListToCombobox(_form.ConnectionInterfacesList, _form.SDConnectionInterfaceComboBox);
      LoadInfoFromListToCombobox(_form.SocketsList, _form.MotherboardSocketComboBox);
      LoadInfoFromListToCombobox(_form.ChipsetsList, _form.MotherboardChipsetComboBox);
      LoadInfoFromListToCombobox(_form.RAMChanelsList, _form.MotherBoardRAMChanelsComboBox);
      LoadFormFactorsInComboBox(_form.MotherboardFormFactorComboBox, "Motherboard");
      LoadFormFactorsInComboBox(_form.SDFormFactorComboBox, "SD");
      LoadRamFrequencyInComboBox(_form.MotherboardRamFrequencyComboBox);
      LoadMemoryTypeInComboBox(_form.MotherboardSupportedRAMComboBox, "RAM");

      LoadFormFactorsInComboBox(_form.CaseFormFactorComboBox, "Case");
    }
    private void ViewSecondPartDataFromListsToFormEl(ComponentsOptionsForm _form)
    {
      LoadInfoFromListToCombobox(_form.SeriesList, _form.CPUSeriesComboBox);
      LoadInfoFromListToCombobox(_form.CPUCodeNamesList, _form.CPUCodeNameComboBox);
      LoadInfoFromListToCombobox(_form.SocketsList, _form.CPUSocketComboBox);
      LoadMemoryTypeInComboBox(_form.CPUMemoryTypeComboBox, "RAM");
      LoadInfoFromListToCombobox(_form.RAMChanelsList, _form.CPUChanelsComboBox);
      LoadRamFrequencyInComboBox(_form.CPURamFrequaencyComboBox);
      LoadMemoryTypeInComboBox(_form.RAMTypeComboBox, "RAM");
      LoadRamFrequencyInComboBox(_form.RAMFrequencyComboBox);
      LoadPowerTypeInComboBox(_form.CoolingSystemPowerTypeComboBox);
      LoadPowerTypeInComboBox(_form.PSUMotherboardPowerTypeComboBox);
    }

    private void tabPage1_Enter(object sender, EventArgs e)
    {
      if (othersTabsLostFocus)
      {
        if (tabControl2.SelectedTab.Text != "Оперативная память" && tabControl2.SelectedTab.Text != "Накопители" &&
            tabControl2.SelectedTab.Text != "Охлаждение" && tabControl2.SelectedTab.Text != "Блок питания")
          AutoScroll = true;

        ViewInfoFromListsToFormElements(this);
        ViewSecondPartDataFromListsToFormEl(this);
        ReturnComboBoxInfo();
      }
    }
    
    //Нужен для того чтобы информация в ComboBox-ах не сбивалась
    private void ReturnComboBoxInfo()
    {
      CPUSeriesComboBox.SelectedItem = CPUSeriesComboBoxText;
      CPUCodeNameComboBox.SelectedItem = CPUCodeNameComboBoxText;
      CPUSocketComboBox.SelectedItem = CPUSocketComboBoxText;
      CPUMemoryTypeComboBox.SelectedItem = CPUMemoryTypeComboBoxText;
      CPUChanelsComboBox.SelectedItem = CPUChanelsComboBoxText;
      if (CPUMemoryFrequencyComboBoxText != "")
          CPURamFrequaencyComboBox.SelectedItem = Convert.ToInt32(CPUMemoryFrequencyComboBoxText);

      GPUPowerTypeComboBox.SelectedItem = GPUpowerTypeComboBoxText;
      GPUMemoryTypeComboBox.SelectedItem = GPUTypeComboBoxText;
      GPUInterfacesComboBox.SelectedItem = GPUconnectionInterfaceComboBoxText;

      MotherboardSocketComboBox.SelectedItem = MotherboardSocketComboBoxText;
      MotherboardChipsetComboBox.SelectedItem = MotherboardChipsetComboBoxText;
      MotherboardFormFactorComboBox.SelectedItem = MotherboardFormFactorComboBoxText;
      MotherboardSupportedRAMComboBox.SelectedItem = MotherboardSupportedMemoryComboBoxText;
      MotherBoardRAMChanelsComboBox.SelectedItem = MotherboardMemoryChanelsComboBoxText;
      if (MotherboardRAMFrequencyComboBoxText != "")
          MotherboardRamFrequencyComboBox.SelectedItem = Convert.ToInt32(MotherboardRAMFrequencyComboBoxText);

      CaseFormFactorComboBox.SelectedItem = CaseFormFactorComboBoxText;
      CoolingSystemPowerTypeComboBox.SelectedItem = CoolingSystemPowerTypeText;

      PSUMotherboardPowerTypeComboBox.SelectedItem = PSUPowerTypeText;
      SDFormFactorComboBox.SelectedItem = SDFormFactorText;
      SDConnectionInterfaceComboBox.SelectedItem = SDConnectionInterfaceText;

      RAMTypeComboBox.SelectedItem = RAMTypeComboBoxText;
      if (RAMFrequencyComboBoxText != "")
        RAMFrequencyComboBox.SelectedItem = Convert.ToInt32(RAMFrequencyComboBoxText);
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
      if (AddCaseRadio.Checked)
      {
        SystemFunctions.ClearCaseTextBoxes(this);
        SystemFunctions.SetEditOrAddButtonMode(ActWithCase, true);
        SystemFunctions.ChangeCaseTextBoxesEnable(this, true);
        FindCaseIDButton.Enabled = true;
      }
    }

    private void ChangeCaseRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangeCaseRadio.Checked)
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
      SearchInfoInList(CaseList, CaseIDTextBox, ActWithCase);
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
          newCase.GetDataFromDB();
          CaseList.Add(newCase);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid(Case_DatagridView, CaseList);
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
          ViewInfoInDataGrid(Case_DatagridView, CaseList);
        }
      }
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

    private void tabPage2_Leave(object sender, EventArgs e)
    {
      othersTabsLostFocus = true;
    }

    private void tabPage12_Leave(object sender, EventArgs e)
    {
        othersTabsLostFocus = true;
    }

    private void AddRAMRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (AddRAMRadio.Checked)
      {
        SystemFunctions.ClearRAMTextBoxes(this);
        SystemFunctions.SetEditOrAddButtonMode(ActWithRAM, true);
        SystemFunctions.ChangeRAMTextBoxesEnable(this, true);
        FindRAMIDButton.Enabled = true;
      }
    }

    private void ChangeRAMRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangeRAMRadio.Checked)
      {
        SystemFunctions.ChangeRAMTextBoxesEnable(this, true);
        FindRAMIDButton.Enabled = false;
        RAMIDTextBox.Enabled = false;
        SystemFunctions.SetEditOrAddButtonMode(ActWithRAM, false);
        ViewRAMInfoToChange();
      }
    }

    private void ViewRAMInfoToChange()
    {
      if (RAM_DatagridView.SelectedCells.Count > 0)
      {
        int selectedrowindex = RAM_DatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = RAM_DatagridView.Rows[selectedrowindex];
        RAM ram = new RAM();
        ram = RAMList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
        RAMIDTextBox.Text = ram.ID;
        RAMNameTextBox.Text = ram.Name;
        RAMKitTextBox.Text = Convert.ToString(ram.Kit);
        RAMTypeComboBox.SelectedItem = (from b in MemoryTypesList
                                        where b.Device_type == "RAM" && b.ID == ram.RAM_type_ID
                                        select b.Name).Single();
        RAMFrequencyComboBox.SelectedItem = RAMFrequencyList.Single(i => i.ID == ram.RAM_frequency_ID).Frequency;
        RAMVoltageTextBox.Text = Convert.ToString(ram.Voltage);
        RAMCapacityTextBox.Text = Convert.ToString(ram.Capacity);
        RAMTimingsTextBox.Text = ram.Timings;
        RAMLowProfileModuleCheckBox.Checked = ram.Low_profile_module;
        RAMxmpSupportCheckBox.Checked = ram.XMP_profile;
        RAMCoolingCheckBox.Checked = ram.Cooling;
      }
    }

    private void RAM_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (ChangeRAMRadio.Checked)
        ViewRAMInfoToChange();
    }

    private void FindRAMIDButton_Click(object sender, EventArgs e)
    {
      SearchInfoInList(RAMList, RAMIDTextBox, ActWithRAM);
    }

    private void ActWithRAM_Click(object sender, EventArgs e)
    {
      if (SystemFunctions.CheckNullForRAMTextBoxes(this))
      {
        MessageBox.Show("Не все поля заполнены");
        return;
      }
      else
      {
        if (!SystemFunctions.CheckNumConvertForRAMTextBoxes(this))
        {
          MessageBox.Show("Не все поля можно привести к числовому типу");
          return;
        }
      }
      if (AddRAMRadio.Checked)
      {
        RAM newRAM = AddInfoAboutRAM();
        if (SQLRequests.Warning(true, newRAM.Name))
        {
          SQLRequests.AddRAM(newRAM);
          SQLRequests.EditRAMMediator(newRAM, "Add");
          newRAM.GetDataFromDB();
          RAMList.Add(newRAM);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid(RAM_DatagridView, RAMList);
        }
      }
      else if (ChangeRAMRadio.Checked)
      {
        RAM changedRAM = AddInfoAboutRAM();
        if (SQLRequests.Warning(false, changedRAM.Name))
        {
          SQLRequests.ChangeRAM(changedRAM);
          SQLRequests.EditRAMMediator(changedRAM, "Edit");
          int temp = RAMList.IndexOf(RAMList.Single(i => i.ID == changedRAM.ID));
          RAMList[temp] = changedRAM;
          MessageBox.Show("Изменение прошло успешно!");
          ViewInfoInDataGrid(RAM_DatagridView, RAMList);
        }
      }
      SystemFunctions.ClearRAMTextBoxes(this);
      this.ActiveControl = null;
    }
    private RAM AddInfoAboutRAM()
    {
      RAM ram = new RAM();
      ram.ID = RAMIDTextBox.Text;
      ram.Name = RAMNameTextBox.Text;
      ram.Kit = Convert.ToInt32(RAMKitTextBox.Text);
      ram.RAM_type_ID = (from b in MemoryTypesList
                        where b.Device_type == "RAM" && b.Name == RAMTypeComboBox.Text
                        select b.ID).Single();
      ram.RAM_frequency_ID = RAMFrequencyList.Single(i => 
        i.Frequency == Convert.ToInt32(RAMFrequencyComboBox.Text)).ID;
      ram.Voltage = float.Parse(RAMVoltageTextBox.Text);
      ram.Capacity = Convert.ToInt32(RAMCapacityTextBox.Text);
      ram.Timings = RAMTimingsTextBox.Text;
      ram.Low_profile_module = RAMLowProfileModuleCheckBox.Checked;
      ram.XMP_profile = RAMxmpSupportCheckBox.Checked;
      ram.Cooling = RAMCoolingCheckBox.Checked;
      return ram;
    }

    private void MotherboardIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if (AddMotherboardRadio.Checked)
      {
        if (MotherboardIDTextBox.Text == "")
          FindMotherboardIDButton.Enabled = false;
        else
        {
          FindMotherboardIDButton.Enabled = true;
          FindInfoFromTextBox(MotherboardIDTextBox, MotherBoardList, ActWithMotherboard);
        }
      }
    }

    private void CaseIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if (AddCaseRadio.Checked)
      {
        if (CaseIDTextBox.Text == "")
          FindCaseIDButton.Enabled = false;
        else
        {
          FindCaseIDButton.Enabled = true;
          FindInfoFromTextBox(CaseIDTextBox, CaseList, ActWithCase);
        }
      }
    }

    private void RAMIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if (AddRAMRadio.Checked)
      {
        if (RAMIDTextBox.Text == "")
          FindRAMIDButton.Enabled = false;
        else
        {
          FindRAMIDButton.Enabled = true;
          FindInfoFromTextBox(RAMIDTextBox, RAMList, ActWithRAM);
        }
      }
    }

    private void AddCoolingSystemRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (AddCoolingSystemRadio.Checked)
      {
        SystemFunctions.ClearCoolingSystemTextBoxes(this);
        SystemFunctions.SetEditOrAddButtonMode(ActWithCoolingSystem, true);
        SystemFunctions.ChangeCoolingSystemTextBoxesEnable(this, true);
        FindCoolingSystemIDButton.Enabled = true;
      }
    }

    private void CoolingSystemIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if (AddCoolingSystemRadio.Checked)
      {
        if (CoolingSystemIDTextBox.Text == "")
          FindCoolingSystemIDButton.Enabled = false;
        else
        {
          FindCoolingSystemIDButton.Enabled = true;
          FindInfoFromTextBox(CoolingSystemIDTextBox, CoolingSystemList, ActWithCoolingSystem);
        }
      }
    }

    private void ChangeCoolingSystemRadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangeCoolingSystemRadio.Checked)
      {
        SystemFunctions.ChangeCoolingSystemTextBoxesEnable(this, true);
        FindCoolingSystemIDButton.Enabled = false;
        CoolingSystemIDTextBox.Enabled = false;
        SystemFunctions.SetEditOrAddButtonMode(ActWithCoolingSystem, false);
        ViewCoolingSystemInfoToChange();
      }
    }

    private void FindCoolingSystemIDButton_Click(object sender, EventArgs e)
    {
      SearchInfoInList(CoolingSystemList, CoolingSystemIDTextBox, ActWithCoolingSystem);
    }

    private void CoolingSystem_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (ChangeCoolingSystemRadio.Checked)
        ViewCoolingSystemInfoToChange();
    }
    private void ViewCoolingSystemInfoToChange()
    {
      if (CoolingSystem_DatagridView.SelectedCells.Count > 0)
      {
        int selectedrowindex = CoolingSystem_DatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = CoolingSystem_DatagridView.Rows[selectedrowindex];

        Cooling_system currentCoolingSystem = new Cooling_system();
        currentCoolingSystem = CoolingSystemList.Single(i => i.ID == (string)currentRow.Cells[0].Value);

        CoolingSystemIDTextBox.Text = currentCoolingSystem.ID;
        CoolingSystemNameTextBox.Text = currentCoolingSystem.Name;
        CoolingSystemSocketsTextBox.Text = currentCoolingSystem.Supported_sockets;
        CoolingSystemCountOfHeatPipesTextBox.Text = Convert.ToString(currentCoolingSystem.Count_of_heat_pipes);
        CoolingSystemEvaporationChamberCheckBox.Checked = currentCoolingSystem.Evaporation_chamber;
        CoolingSystemTypeOfBearing.Text = currentCoolingSystem.Type_of_bearing;
        CoolingSystemNoiseLevelTextBox.Text = Convert.ToString(currentCoolingSystem.Noise_level);
        CoolingSystemRSCCheckBox.Checked = currentCoolingSystem.Rotation_speed_control;
        CoolingSystemPowerTypeComboBox.SelectedItem = PowerConnectorsList.Single(i =>
          i.ID == currentCoolingSystem.Power_type_ID).Connectors;
        CoolingSystemMaxSpeedTextBox.Text = Convert.ToString(currentCoolingSystem.Max_state);
        CoolingSystemConsumptionTextBox.Text = Convert.ToString(currentCoolingSystem.Consumption);
        CoolingSystemDiametrTextBox.Text = Convert.ToString(currentCoolingSystem.Diameter);
      }
    }

    private void ActWithCoolingSystem_Click(object sender, EventArgs e)
    {
      if (SystemFunctions.CheckNullForCoolingSystemTextBoxes(this))
      {
        MessageBox.Show("Не все поля заполнены");
        return;
      }
      else
      {
        if (!SystemFunctions.CheckNumConvertForCoolingSystemTextBoxes(this))
        {
          MessageBox.Show("Не все поля можно привести к числовому типу");
          return;
        }
      }
      if (AddCoolingSystemRadio.Checked)
      {
        Cooling_system newCoolingSystem = AddInfoAboutCoolingSystem();
        if (SQLRequests.Warning(true, newCoolingSystem.Name))
        {
          SQLRequests.AddCoolingSystem(newCoolingSystem);
          SQLRequests.EditCoolingSystemMediator(newCoolingSystem, "Add");
          newCoolingSystem.GetDataFromDB();
          CoolingSystemList.Add(newCoolingSystem);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid<Cooling_system>(CoolingSystem_DatagridView, CoolingSystemList);
        }
      }
      else if (ChangeCoolingSystemRadio.Checked)
      {
        Cooling_system changedCoolingSystem = AddInfoAboutCoolingSystem();
        if (SQLRequests.Warning(false, changedCoolingSystem.Name))
        {
          SQLRequests.ChangeCoolingSystem(changedCoolingSystem);
          SQLRequests.EditCoolingSystemMediator(changedCoolingSystem, "Edit");
          int temp = CoolingSystemList.IndexOf(CoolingSystemList.Single(i => i.ID == changedCoolingSystem.ID));
          CoolingSystemList[temp] = changedCoolingSystem;
          MessageBox.Show("Изменение прошло успешно!");
          ViewInfoInDataGrid<Cooling_system>(CoolingSystem_DatagridView, CoolingSystemList);
        }
      }
      SystemFunctions.ClearCoolingSystemTextBoxes(this);
      this.ActiveControl = null;
    }

    private Cooling_system AddInfoAboutCoolingSystem()
    {
      Cooling_system coolingSys = new Cooling_system();
      coolingSys.ID = CoolingSystemIDTextBox.Text;
      coolingSys.Name = CoolingSystemNameTextBox.Text;
      coolingSys.Supported_sockets = CoolingSystemSocketsTextBox.Text;
      coolingSys.Count_of_heat_pipes = Convert.ToInt32(CoolingSystemCountOfHeatPipesTextBox.Text);
      coolingSys.Evaporation_chamber = CoolingSystemEvaporationChamberCheckBox.Checked;
      coolingSys.Type_of_bearing = CoolingSystemTypeOfBearing.Text;
      coolingSys.Noise_level = float.Parse(CoolingSystemNoiseLevelTextBox.Text);
      coolingSys.Rotation_speed_control = CoolingSystemRSCCheckBox.Checked;
      coolingSys.Power_type_ID = PowerConnectorsList.Single(i => i.Connectors == CoolingSystemPowerTypeComboBox.Text).ID;
      coolingSys.Max_state = Convert.ToInt32(CoolingSystemMaxSpeedTextBox.Text);
      coolingSys.Consumption = Convert.ToInt32(CoolingSystemConsumptionTextBox.Text);
      coolingSys.Diameter = Convert.ToInt32(CoolingSystemDiametrTextBox.Text);
      return coolingSys;
    }

    private void RAMTypeComboBox_Leave(object sender, EventArgs e)
    {
      RAMTypeComboBoxText = RAMTypeComboBox.Text;
    }

    private void RAMFrequencyComboBox_Leave(object sender, EventArgs e)
    {
      RAMFrequencyComboBoxText = RAMFrequencyComboBox.Text;
    }

    private void CoolingSystemPowerTypeComboBox_Leave(object sender, EventArgs e)
    {
      CoolingSystemPowerTypeText = CoolingSystemPowerTypeComboBox.Text;
    }

    private void tabPage8_Enter(object sender, EventArgs e)
    {
      this.AutoScroll = false;
    }

    private void PSUIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if (AddPSURadio.Checked)
      {
        if (PSUIDTextBox.Text == "")
          FindPSUIDButton.Enabled = false;
        else
        {
          FindPSUIDButton.Enabled = true;
          FindInfoFromTextBox(PSUIDTextBox, PSUList, ActWithPSU);
        }
      }
    }

    private void FindPSUIDButton_Click(object sender, EventArgs e)
    {
      SearchInfoInList(PSUList, PSUIDTextBox, ActWithPSU);
    }

    private void AddPSURadio_CheckedChanged(object sender, EventArgs e)
    {
      if (AddPSURadio.Checked)
      {
        SystemFunctions.ClearPSUTextBoxes(this);
        SystemFunctions.SetEditOrAddButtonMode(ActWithPSU, true);
        SystemFunctions.ChangePSUTextBoxesEnable(this, true);
        FindPSUIDButton.Enabled = true;
      }
    }

    private void ChangePSURadio_CheckedChanged(object sender, EventArgs e)
    {
      if (ChangePSURadio.Checked)
      {
        SystemFunctions.ChangePSUTextBoxesEnable(this, true);
        FindPSUIDButton.Enabled = false;
        PSUIDTextBox.Enabled = false;
        SystemFunctions.SetEditOrAddButtonMode(ActWithPSU, false);
        ViewPSUInfoToChange();
      }
    }
        
    private void ViewPSUInfoToChange()
    {
      if (PSU_DatagridView.SelectedCells.Count > 0)
      {
        int selectedrowindex = PSU_DatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = PSU_DatagridView.Rows[selectedrowindex];

        PSU currentPSU = new PSU();
        currentPSU = PSUList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
        PSUIDTextBox.Text = currentPSU.ID;
        PSUNameTextBox.Text = currentPSU.Name;
        PSUConsumptionTextBox.Text = Convert.ToString(currentPSU.Consumption);
        PSUsataCountTextBox.Text = Convert.ToString(currentPSU.Sata_power_count);
        PSUStandartTextBox.Text = currentPSU.PSU_standart;
        PSUTwelveLineTextBox.Text = Convert.ToString(currentPSU.Line_plus_twelve_V_count);
        PSUTwelveLineMaxAmperageTextBox.Text = Convert.ToString(currentPSU.Max_amperage_on_line_plus_twelve);
        PSUEfficiencyTextBox.Text =  Convert.ToString(currentPSU.Efficiency);
        PSUModularityCheckBox.Checked = currentPSU.Modularity;
        PSUMotherboardPowerTypeComboBox.SelectedItem = PowerConnectorsList.Single(i =>
                                                      i.ID == currentPSU.Power_motherboard_type_ID).Connectors;
        PSUPowerUSBCheckBox.Checked = currentPSU.Power_USB;
        PSUcpuPowerTypeTextBox.Text = currentPSU.Power_CPU;
        PSUidePowerTypeTextBox.Text = currentPSU.Power_IDE;
        PSUpciePowerTypeTextBox.Text = currentPSU.Power_PCIe;
        PSULengthTextBox.Text = Convert.ToString(currentPSU.Length);
      }
    }

    private void PSU_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (ChangePSURadio.Checked)
        ViewPSUInfoToChange();
    }

    private void tabPage9_Enter(object sender, EventArgs e)
    {
      AutoScroll = false;
    }

    private void ActWithPSU_Click(object sender, EventArgs e)
    {
      if (SystemFunctions.CheckNullForPSUTextBoxes(this))
      {
        MessageBox.Show("Не все поля заполнены");
        return;
      }
      else
      {
        if (!SystemFunctions.CheckNumConvertForPSUTextBoxes(this))
        {
          MessageBox.Show("Не все поля можно привести к числовому типу");
          return;
        }
      }
      if (AddPSURadio.Checked)
      {
        PSU newPSU = AddInfoAboutPSU();
        if (SQLRequests.Warning(true, newPSU.Name))
        {
          SQLRequests.AddPSU(newPSU);
          SQLRequests.EditPSUMediator(newPSU, "Add");
          newPSU.GetDataFromDB();
          PSUList.Add(newPSU);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid<PSU>(PSU_DatagridView, PSUList);
        }
      }
      else if (ChangePSURadio.Checked)
      {
        PSU changedPSU = AddInfoAboutPSU();
        if (SQLRequests.Warning(false, changedPSU.Name))
        {
          SQLRequests.ChangePSU(changedPSU);
          SQLRequests.EditPSUMediator(changedPSU, "Edit");
          int temp = PSUList.IndexOf(PSUList.Single(i => i.ID == changedPSU.ID));
          PSUList[temp] = changedPSU;
          MessageBox.Show("Изменение прошло успешно!");
          ViewInfoInDataGrid<PSU>(PSU_DatagridView, PSUList);
        }
      }
      SystemFunctions.ClearPSUTextBoxes(this);
      this.ActiveControl = null;
    }

    private PSU AddInfoAboutPSU()
    {
      PSU changedPSU = new PSU();
      changedPSU.ID = PSUIDTextBox.Text;
      changedPSU.Name = PSUNameTextBox.Text;
      changedPSU.PSU_standart = PSUStandartTextBox.Text;
      changedPSU.Line_plus_twelve_V_count = Convert.ToInt32(PSUTwelveLineTextBox.Text);
      changedPSU.Max_amperage_on_line_plus_twelve = Convert.ToInt32(PSUTwelveLineMaxAmperageTextBox.Text);
      changedPSU.Efficiency = Convert.ToInt32(PSUEfficiencyTextBox.Text);
      changedPSU.Modularity = PSUModularityCheckBox.Checked;

      changedPSU.Power_motherboard_type_ID = PowerConnectorsList.Single(i => 
        i.Connectors == PSUMotherboardPowerTypeComboBox.Text).ID;

      changedPSU.Power_USB = PSUPowerUSBCheckBox.Checked;
      changedPSU.Power_CPU = PSUcpuPowerTypeTextBox.Text;
      changedPSU.Power_IDE = PSUidePowerTypeTextBox.Text;
      changedPSU.Power_PCIe = PSUpciePowerTypeTextBox.Text;
      changedPSU.Sata_power_count = Convert.ToInt32(PSUsataCountTextBox.Text);
      changedPSU.Consumption = Convert.ToInt32(PSUConsumptionTextBox.Text);
      changedPSU.Length = Convert.ToInt32(PSULengthTextBox.Text);
      return changedPSU;
    }

    //Нужен для обновления списка документов движения
    private async void UpdateInfo()
    {
      while(true)
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          NeedToUpdate needToUpdate = db.NeedToUpdate.Single(i => i.ID == 1);
          if (needToUpdate.UpdateStatus)
          {
            await Task.Run(() => LoadHoldingDocsFromDB());
            await Task.Run(() => ViewDocsInDataGrid());
            needToUpdate.UpdateStatus = false;
            db.NeedToUpdate.Update(needToUpdate);
            db.SaveChanges();
          }
        }               
      }
    }

    private void AddSDRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(AddSDRadio.Checked)
      {
        SystemFunctions.ClearSDTextBoxes(this);
        SystemFunctions.SetEditOrAddButtonMode(ActWithSD, true);
        SystemFunctions.ChangeSDTextBoxesEnable(this, true);
        FindSDIDButton.Enabled = true;
      }
    }

    private void ChangeSDRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(ChangeSDRadio.Checked)
      {
        SystemFunctions.ChangeSDTextBoxesEnable(this, true);
        FindSDIDButton.Enabled = false;
        SDIDTextBox.Enabled = false;
        SystemFunctions.SetEditOrAddButtonMode(ActWithSD, false);
        ViewSDInfoToChange();
      }
    }

    private void FindSDIDButton_Click(object sender, EventArgs e)
    {
      SearchInfoInList(StorageDevicesList, SDIDTextBox, ActWithSD);
    }

    private void SD_DatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if(ChangeSDRadio.Checked)
        ViewSDInfoToChange();
    }

    private void tabPage10_Enter(object sender, EventArgs e)
    {
      AutoScroll = false;
    }

    private void ActWithSD_Click(object sender, EventArgs e)
    {
      if(SystemFunctions.CheckNullForSDTextBoxes(this))
      {
        MessageBox.Show("Не все поля заполнены");
        return;
      }
      else
      {
        if(!SystemFunctions.CheckNumConvertForSDTextBoxes(this))
        {
          MessageBox.Show("Не все поля можно привести к числовому типу");
          return;
        }
      }
      if(AddSDRadio.Checked)
      {
        Storage_devices newSD = AddInfoAboutSD();
        if(SQLRequests.Warning(true, newSD.Name))
        {
          SQLRequests.AddSD(newSD);
          SQLRequests.EditSDMediator(newSD, "Add");
          newSD.GetDataFromDB();
          StorageDevicesList.Add(newSD);
          MessageBox.Show("Добавление прошло успешно!");
          ViewInfoInDataGrid(SD_DatagridView, StorageDevicesList);
        }
      }
      else if(ChangeSDRadio.Checked)
      {
        Storage_devices changedSD = AddInfoAboutSD();
        if(SQLRequests.Warning(false, changedSD.Name))
        {
          SQLRequests.ChangeSD(changedSD);
          SQLRequests.EditSDMediator(changedSD, "Edit");
          int temp = StorageDevicesList.IndexOf(StorageDevicesList.Single(i => i.ID == changedSD.ID));
          StorageDevicesList[temp] = changedSD;
          MessageBox.Show("Изменение прошло успешно!");
          ViewInfoInDataGrid(SD_DatagridView, StorageDevicesList);
        }
      }

      SystemFunctions.ClearSDTextBoxes(this);
      this.ActiveControl = null;
    }

    private Storage_devices AddInfoAboutSD()
    {
      Storage_devices changedSD = new Storage_devices();
      changedSD.ID = SDIDTextBox.Text;
      changedSD.Name = SDNameTextBox.Text;
      changedSD.Form_factor_ID = FormFactorsList.Single(i => i.Name == SDFormFactorComboBox.Text).ID;
      changedSD.Interface_ID = ConnectionInterfacesList.Single(i => i.Name == SDConnectionInterfaceComboBox.Text).ID;
      changedSD.Buffer = Convert.ToInt32(SDBufferTextBox.Text);
      changedSD.Capacity = Convert.ToInt32(SDCapacityTextBox.Text);
      changedSD.Hardware_encryption = SDEncryptionCheckBox.Checked;
      changedSD.Sequential_read_speed = Convert.ToInt32(SDSeqReadSpeedTextBox.Text);
      changedSD.Sequeintial_write_speed = Convert.ToInt32(SDSeqWriteSpeedTextBox.Text);
      changedSD.Random_read_speed = Convert.ToInt32(SDRandReadSpeedTextBox.Text);
      changedSD.Random_write_speed = Convert.ToInt32(SDRandWriteSpeedTextBox.Text);
      changedSD.Consumption = Convert.ToInt32(SDConsumptionTextBox.Text);
      changedSD.Thickness = float.Parse(SDThicknessTextBox.Text);
      return changedSD;
    }

    private void ViewSDInfoToChange()
    {
      if(SD_DatagridView.SelectedCells.Count > 0)
      {
        int selectedrowindex = SD_DatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = SD_DatagridView.Rows[selectedrowindex];

        Storage_devices currentSD = new Storage_devices();
        currentSD = StorageDevicesList.Single(i => i.ID == (string)currentRow.Cells[0].Value);
        SDIDTextBox.Text = currentSD.ID;
        SDNameTextBox.Text = currentSD.Name;
        SDFormFactorComboBox.SelectedItem = FormFactorsList.Single(i => i.ID == currentSD.Form_factor_ID).Name;
        SDConnectionInterfaceComboBox.SelectedItem = ConnectionInterfacesList.Single(i => i.ID == currentSD.Interface_ID).Name;
        SDBufferTextBox.Text = Convert.ToString(currentSD.Buffer);
        SDCapacityTextBox.Text = Convert.ToString(currentSD.Capacity);
        SDEncryptionCheckBox.Checked = currentSD.Hardware_encryption;
        SDSeqReadSpeedTextBox.Text = Convert.ToString(currentSD.Sequential_read_speed);
        SDSeqWriteSpeedTextBox.Text = Convert.ToString(currentSD.Sequeintial_write_speed);
        SDRandReadSpeedTextBox.Text = Convert.ToString(currentSD.Random_read_speed);
        SDRandWriteSpeedTextBox.Text = Convert.ToString(currentSD.Random_write_speed);
        SDConsumptionTextBox.Text = Convert.ToString(currentSD.Consumption);
        SDThicknessTextBox.Text = Convert.ToString(currentSD.Thickness);        
      }
    }

    private void PSUMotherboardPowerTypeComboBox_Leave(object sender, EventArgs e)
    {
      PSUPowerTypeText = PSUMotherboardPowerTypeComboBox.Text;
    }

    private void SDConnectionInterfaceComboBox_Leave(object sender, EventArgs e)
    {
      SDConnectionInterfaceText = SDConnectionInterfaceComboBox.Text;
    }

    private void SDFormFactorComboBox_Leave(object sender, EventArgs e)
    {
      SDFormFactorText = SDFormFactorComboBox.Text;
    }

    private void SDIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if(AddSDRadio.Checked)
      {
        if(SDIDTextBox.Text == "")
          FindSDIDButton.Enabled = false;
        else
        {
          FindSDIDButton.Enabled = true;
          FindInfoFromTextBox(SDIDTextBox, StorageDevicesList, ActWithSD);
        }
      }
    }
  }
}