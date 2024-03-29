﻿using Computer_house.DataBase;
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
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
  public partial class AuthorizedForm : Form
  {
    private List<Warehouse_info> WarehouseInformationList = new List<Warehouse_info>();
    private List<Case> Cases = new List<Case>();
    private List<Cooling_system> CoolingSystems = new List<Cooling_system>();
    private List<CPU> Cpus = new List<CPU>();
    private List<GPU> Gpus = new List<GPU>();
    //private List<HDD> Hdds;
    private List<Motherboard> Motherboards = new List<Motherboard>();
    private List<PSU> Psus = new List<PSU>();
    private List<RAM> Rams = new List<RAM>();
    // private List<SSD> Ssds;
    private List<Locations_in_warehouse> LocationInWarehouseList = new List<Locations_in_warehouse>();
    private List<Products_location> ProductLocationsList = new List<Products_location>();
    private List<Mediator> Mediators = new List<Mediator>();
    private List<Price_list> PriceList = new List<Price_list>();
    private List<Storage_devices> StorageDevices = new List<Storage_devices>();

    private List<Mediator> PCs = new List<Mediator>();

    private string cpuNameInListBox = "";
    private string gpuNameInListBox = "";
    private string motherboardNameInListBox = "";
    private string ramNameInListBox = "";
    private string coolingSystemNameInListBox = "";
    private string psuNameInListBox = "";
    private string storageNameInListBox = "";
    private string caseNameInListBox = "";

    //поля для определения ограничений конфигуратора
    private string socket = "";
    private string ramType = "";
    private int maxRamFrequency = 0;
    private int maxRamCapacity = 0;
    private bool integratedGSupport = false;
    private string gpuInterface = "";
    private string sdInterface = "";
    private string motherboardFormFactor = "";
    private int maxGpuLength = 0;
    private int maxCpuHeight = 0;
    private int maxPsuLength = 0;
    private int cpuConsumptionForCooling = 0;
    private int psuConsumption = 0;
    private int maxConsumption = 0;

    private bool checkUpdate = true;

    private List<Product> shoppingBasket = new List<Product>();

    //Нужна для печати сведений о покупке
    private string finalPrintMessage = "";

    //списки фильтрованных комплектующих
    private List<Case> filteredCases = new List<Case>();
    private List<Cooling_system> filteredCoolingSystems = new List<Cooling_system>();
    private List<CPU> filteredCpus = new List<CPU>();
    private List<GPU> filteredGpus = new List<GPU>();
    private List<Motherboard> filteredMotherboards = new List<Motherboard>();
    private List<PSU> filteredPsus = new List<PSU>();
    private List<RAM> filteredRams = new List<RAM>();
    private List<Storage_devices> filteredStorageDevices = new List<Storage_devices>();

    private Users user;

    //организация блокирования функции перетаскивания формы
    const int SC_CLOSE = 0xF010;
    const int MF_BYCOMMAND = 0;
    [DllImport("User32.dll")]
    static extern int SendMessage(IntPtr hWnd,
    int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("User32.dll")]
    static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32.dll")]
    static extern bool RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      IntPtr hMenu = GetSystemMenu(Handle, false);
      RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
    }

    public AuthorizedForm()
    {
      InitializeComponent();
    }

    public AuthorizedForm(Users _user)
    {
      user = _user;
      InitializeComponent();
    }

    private async void AuthorizedForm_Load(object sender, EventArgs e)
    {
      Width = Convert.ToInt32(DesktopScreen.Width / DesktopScreen.GetScalingFactor());
      Height = Convert.ToInt32(DesktopScreen.Height / DesktopScreen.GetScalingFactor());
      UnsetUpdate();

        LockOrEnableComboBox(false, CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
          RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);

      await Task.Run(() => LoadPriceListFromDB());
      await Task.Run(() => LoadInfoAboutCPUFromDB());
      await Task.Run(() => LoadInfoAboutCasesFromDB());
      await Task.Run(() => LoadInfoAboutGPUFromDB());
      await Task.Run(() => LoadInfoAboutMotherboardsFromDB());
      await Task.Run(() => LoadLocationInWarehouseFromDB());
      await Task.Run(() => LoadProductLocationFromDB());
      await Task.Run(() => LoadInfoAboutMediatorFromDB());
      await Task.Run(() => LoadInfoAboutRAMFromDB());
      await Task.Run(() => LoadInfoAboutCoolingFromDB());
      await Task.Run(() => LoadInfoAboutPSUFromDB());
      await Task.Run(() => LoadInfoAboutSDFromDB());
      await Task.Run(() => LoadPCConfigs());

      ViewInfoInComboBox<CPU>(Cpus, CPU_ComboBox);
      ViewInfoInComboBox<GPU>(Gpus, GPU_ComboBox);
      ViewInfoInComboBox<Motherboard>(Motherboards, Motherboard_ComboBox);
      ViewInfoInComboBox<RAM>(Rams, RAM_ComboBox);
      ViewInfoInComboBox<Cooling_system>(CoolingSystems, CoolingSystem_ComboBox);
      ViewInfoInComboBox<PSU>(Psus, PSU_ComboBox);
      ViewInfoInComboBox<Case>(Cases, Case_ComboBox);
      ViewInfoInComboBox<Storage_devices>(StorageDevices, StorageDevice_ComboBox);
      ViewPCsInDataGrid();
      LoadInfoFromDBAndView();

      LockOrEnableComboBox(true, CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);

      Task.Run(() => UpdateInTime());
      
    }

    //Нужен в случае первоначального запуска программы
    private void UnsetUpdate()
    {
      using(ApplicationContext db = new ApplicationContext())
      {
        NeedToUpdate update = db.NeedToUpdate.Single(i => i.ID == 1);
        update.UpdateStatusForShop = false;
        db.NeedToUpdate.Update(update);
        db.SaveChanges();
      }
    }
    private void LoadPCConfigs()
    {
      PCs.Clear();
      using(ApplicationContext db = new ApplicationContext())
      {
        PCs = db.Mediator.Where(i => i.Components_type == "PC").ToList();
      }
    }
    private void ViewPCsInDataGrid()
    {
      PCConfigsDataGridView.Rows.Clear();
      try
      {
        string cpu, gpu, mb, cas, cs, psu, sd, ram;
        foreach(Mediator m in PCs)
        {
          using(ApplicationContext db = new ApplicationContext())
          {
            cpu = db.CPU.Single(i => i.ID == m.CPU_ID).Name;
            gpu = db.GPU.Single(i => i.ID == m.GPU_ID).Name;
            mb = db.Motherboard.Single(i => i.ID == m.Motherboard_ID).Name;
            cas = db.Case.Single(i => i.ID == m.Case_ID).Name;
            cs = db.Cooling_system.Single(i => i.ID == m.Cooling_system_ID).Name;
            psu = db.PSU.Single(i => i.ID == m.PSU_ID).Name;
            sd = db.Storage_devices.Single(i => i.ID == m.SD_ID).Name;
            ram = db.RAM.Single(i => i.ID == m.RAM_ID).Name;
            PCConfigsDataGridView.Rows.Add(m.ID, cpu, gpu, mb, ram, cs, psu, sd, cas);
          }
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private async void Update()
    {
      object s = new object();
      EventArgs e = new EventArgs();
      ClearConfig_Click(s, e);
      SystemFunctions.Clear(CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);
      SystemFunctions.Clear(SelectedConfigIntemsListBox, SelectedItemsListBox);
      SystemFunctions.Clear(SelectedComponentInfoTextBox, AllProductInfo);
      PriceLabel.Text = "0";
      AddProduct.Value = AddProduct.Minimum;
      LockOrEnableComboBox(false, CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);

      await Task.Run(() => LoadPriceListFromDB());
      await Task.Run(() => LoadInfoAboutCPUFromDB());
      await Task.Run(() => LoadInfoAboutCasesFromDB());
      await Task.Run(() => LoadInfoAboutGPUFromDB());
      await Task.Run(() => LoadInfoAboutMotherboardsFromDB());
      await Task.Run(() => LoadLocationInWarehouseFromDB());
      await Task.Run(() => LoadProductLocationFromDB());
      await Task.Run(() => LoadInfoAboutMediatorFromDB());
      await Task.Run(() => LoadInfoAboutRAMFromDB());
      await Task.Run(() => LoadInfoAboutCoolingFromDB());
      await Task.Run(() => LoadInfoAboutPSUFromDB());
      await Task.Run(() => LoadInfoAboutSDFromDB());
      await Task.Run(() => LoadPCConfigs());

      ViewInfoInComboBox<CPU>(Cpus, CPU_ComboBox);
      ViewInfoInComboBox<GPU>(Gpus, GPU_ComboBox);
      ViewInfoInComboBox<Motherboard>(Motherboards, Motherboard_ComboBox);
      ViewInfoInComboBox<RAM>(Rams, RAM_ComboBox);
      ViewInfoInComboBox<Cooling_system>(CoolingSystems, CoolingSystem_ComboBox);
      ViewInfoInComboBox<PSU>(Psus, PSU_ComboBox);
      ViewInfoInComboBox<Case>(Cases, Case_ComboBox);
      ViewInfoInComboBox<Storage_devices>(StorageDevices, StorageDevice_ComboBox);
      ViewPCsInDataGrid();

      LoadInfoFromDBAndView();

      LockOrEnableComboBox(true, CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);
    }

    private async void UpdateInTime()
    {
      while(checkUpdate)
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          NeedToUpdate needToUpdate = db.NeedToUpdate.Single(i => i.ID == 1);
          if(needToUpdate.UpdateStatusForShop)
          {
            string question = "Произошли изменения на складе, в связи с этим необходимо обновить информацию," +
            "если сделать это сейчас, то все поля будут очищены. Обновить информацию?";
            DialogResult questionResult = MessageBox.Show(question,
                                          "Обновление информации",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Information,
                                          MessageBoxDefaultButton.Button2,
                                          MessageBoxOptions.DefaultDesktopOnly);
            if(questionResult == DialogResult.Yes)
            {
              Update();
            }
            needToUpdate.UpdateStatusForShop = false;
            db.NeedToUpdate.Update(needToUpdate);
            db.SaveChanges();
          }
        }
      }
    }

    private void LockOrEnableComboBox(bool status, params ComboBox[] comboBoxes)
    {
      foreach (ComboBox i in comboBoxes)
        i.Enabled = status;
    }

    private void ViewInfoInComboBox<T>(List<T> items, ComboBox comboBox) where T : Product
    {
      comboBox.Items.Clear();
      foreach (T i in items)
        comboBox.Items.Add(i.Name);
    }

    private void AuthorizedForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }
    private void LoadInfoAboutMediatorFromDB()
    {
      try
      {
        Mediators.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (Mediator c in db.Mediator)
            Mediators.Add(c);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadPriceListFromDB()
    {
      try
      {
        PriceList.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (Price_list c in db.Price_list)
            PriceList.Add(new Price_list(c.Product_ID, c.Purchasable_price, c.Markup_percent));
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadAdditionalInfo<T>(List<T> products) where T : Product
    {
      foreach (Product i in products)
        i.GetDataFromDB();
    }

    private void LoadInfoAboutSDFromDB()
    {
      try
      {
        StorageDevices.Clear();
        using(ApplicationContext db = new ApplicationContext())
          foreach(Storage_devices c in db.Storage_devices)
            StorageDevices.Add(new Storage_devices(c.ID));

        LoadAdditionalInfo(StorageDevices);
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadInfoAboutCasesFromDB()
    {
      try
      {
        Cases.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (Case c in db.Case)
            Cases.Add(new Case(c.ID));

        LoadAdditionalInfo(Cases);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutCoolingFromDB()
    {
      try
      {
        CoolingSystems.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (Cooling_system c in db.Cooling_system)
            CoolingSystems.Add(new Cooling_system(c.ID));

        LoadAdditionalInfo(CoolingSystems);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutCPUFromDB()
    {
      try
      {
        Cpus.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (CPU c in db.CPU)
            Cpus.Add(new CPU(c.ID));

        LoadAdditionalInfo(Cpus);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutGPUFromDB()
    {
      try
      {
        Gpus.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (GPU c in db.GPU)
            Gpus.Add(new GPU(c.ID));

        LoadAdditionalInfo(Gpus);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadInfoAboutMotherboardsFromDB()
    {
      try
      {
        Motherboards.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (Motherboard c in db.Motherboard)
            Motherboards.Add(new Motherboard(c.ID));

        LoadAdditionalInfo(Motherboards);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadInfoAboutPSUFromDB()
    {
      try
      {
        Psus.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (PSU c in db.PSU)
            Psus.Add(new PSU(c.ID));

        LoadAdditionalInfo(Psus);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadInfoAboutRAMFromDB()
    {
      try
      {
        Rams.Clear();
        using (ApplicationContext db = new ApplicationContext())
          foreach (RAM c in db.RAM)
            Rams.Add(new RAM(c.ID));

        LoadAdditionalInfo(Rams);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }    

    private void LoadProductLocationFromDB()
    {
      try
      {
        ProductLocationsList.Clear();
        using (ApplicationContext db = new ApplicationContext())
        {
          foreach (Products_location i in db.Products_location)
            ProductLocationsList.Add(new Products_location(i.Product_ID, i.Location_ID, i.Items_count));
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadLocationInWarehouseFromDB()
    {
      try
      {
        LocationInWarehouseList.Clear();
        using (ApplicationContext db = new ApplicationContext())
        {
          foreach (Locations_in_warehouse i in db.Locations_in_warehouse)
          {
            LocationInWarehouseList.Add(new Locations_in_warehouse(i.ID,i.Location_label,i.Current_item_count,
              i.Max_item_count));
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private async void LoadInfoFromDBAndView()
    {
      await Task.Run(() => LoadAllInfoFromDB());
      ViewInfoInDataGrid();
    }

    private void LoadAllInfoFromDB()
    {
      try
      {
        WarehouseInformationList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          foreach(Warehouse_info c in db.Warehouse_info)
            WarehouseInformationList.Add(new Warehouse_info(c.Product_ID, c.Current_items_count, c.Items_in_shop));
        
        List<Warehouse_info> tempList = (from b in WarehouseInformationList
                                        orderby b.Items_in_shop descending, b.Current_items_count descending
                                         select b).ToList();

        WarehouseInformationList = tempList;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public void ViewInfoInDataGrid()
    {
      AllInfoDatagridView.Rows.Clear();
      try
      {
        foreach (Warehouse_info wi in WarehouseInformationList)
          AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.ProductType, wi.Current_items_count, wi.Items_in_shop);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void настроитьIPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SystemFunctions.SetNewDataBaseAdress();
    }

    private void выйтиИзУчётнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Authentification_logs log = new Authentification_logs(user.ID, false);
      SQLRequests.CreateAuthentificationLog(log);
      AuthentificationForm authentificationForm = new AuthentificationForm();
      authentificationForm.Show();
      this.Hide();
    }

    private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string fbPath = Application.StartupPath;
      string fname = "Справка.pdf";
      string filename = fbPath + @"\" + fname;
      Help.ShowHelp(this, filename, HelpNavigator.Find, "");
    }

    //нужен для поиска данных по имени в списке переданном в параметрах
    private List<Warehouse_info> SearchByName(List<Warehouse_info> WarehouseInfo)
    {
      List<Warehouse_info> SearchResultList = new List<Warehouse_info>();
      //Поиск по имени
      SearchResultList = (from b in WarehouseInfo
                          where b.ProductName.ToLower().Contains(SearchInfo.Text.ToLower())
                          select b).ToList();
      return SearchResultList;
    }

    private void SearchInfoInWarehouse()
    {
      List<Warehouse_info> SearchResultList = SearchByName(WarehouseInformationList);
      List<Mediator> tempRequest = new List<Mediator>();
      List<Mediator> tempRequestCPU = new List<Mediator>();
      List<Mediator> tempRequestGPU = new List<Mediator>();
      List<Mediator> tempRequestMotherboard = new List<Mediator>();
      List<Mediator> tempRequestCase = new List<Mediator>();
      List<Mediator> tempRequestRAM = new List<Mediator>();
      List<Mediator> tempRequestCoolingSys = new List<Mediator>();
      List<Mediator> tempRequestPSU = new List<Mediator>();
      List<Mediator> tempRequestSD = new List<Mediator>();

      tempRequestCPU.AddRange(GetSearchInfo("CPU"));
      tempRequestGPU.AddRange(GetSearchInfo("GPU"));
      tempRequestMotherboard.AddRange(GetSearchInfo("Motherboard"));
      tempRequestCase.AddRange(GetSearchInfo("Case"));
      tempRequestRAM.AddRange(GetSearchInfo("RAM"));
      tempRequestCoolingSys.AddRange(GetSearchInfo("Cooling system"));
      tempRequestPSU.AddRange(GetSearchInfo("PSU"));
      tempRequestSD.AddRange(GetSearchInfo("SD"));
      tempRequest.AddRange((from b in tempRequestCPU
                            where b.CPU_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestGPU
                            where b.GPU_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestMotherboard
                            where b.Motherboard_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestCase
                            where b.Case_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestRAM
                            where b.RAM_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestCoolingSys
                            where b.Cooling_system_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestPSU
                            where b.PSU_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      tempRequest.AddRange((from b in tempRequestSD
                            where b.SD_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                            select b).ToList());
      ViewInfoAfterSearch(tempRequest, SearchResultList);
    }

    //Нужен для отображения информации после поиска
    private void ViewInfoAfterSearch(List<Mediator> RequestList, List<Warehouse_info> SearchResultList)
    {
      if(RequestList != null)
      {
        foreach(Mediator i in RequestList)
        {
          if(!SearchResultList.Contains(WarehouseInformationList.Single(a => a.Product_ID == i.ID)))
            SearchResultList.Add(WarehouseInformationList.Single(a => a.Product_ID == i.ID));
        }
        //Добавление и вывод при успешном поиске
        if(SearchResultList.Count != 0)
        {
          foreach(Warehouse_info wi in SearchResultList)
            AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
        }
        //В противном случае очистить таблицу
        else
          AllInfoDatagridView.Rows.Clear();
      }
      else
        AllInfoDatagridView.Rows.Clear();
    }

    private void SearchInfo_TextChanged(object sender, EventArgs e)
    {
      AllInfoDatagridView.Rows.Clear();
      AllProductInfo.Clear();
      if (SearchInfo.TextLength > 0)
      {
        try
        {
          SearchInfoInWarehouse();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
      else
        ViewInfoInDataGrid();
    }

    private List<Mediator> GetSearchInfo(string _deviceType)
    {
      return Mediators.Where(i => i.Components_type == _deviceType).ToList();
    }

    //Нужен для добавления комплектующих в список выбранных
    private void AllInfoDatagridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (AllInfoDatagridView.SelectedCells.Count > 0)
        {
          int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
          DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];

          Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
            i.Product_ID == (int)currentRow.Cells[0].Value);
          if (warehouseInfo.Items_in_shop != 0)
          {
            SelectedItemsListBox.Items.Add(warehouseInfo.ProductName);
            shoppingBasket.Add(new Product { Name = "Shop", ID = warehouseInfo.ProductName });
            int index = WarehouseInformationList.IndexOf(warehouseInfo);
            warehouseInfo.Items_in_shop--;
            WarehouseInformationList[index] = warehouseInfo;
            currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) - 1);
            Price_list price = PriceList.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
            PriceLabel.Text = Convert.ToString(Math.Round(decimal.Parse(PriceLabel.Text) + currentPrice, 2));
          }
          else if(warehouseInfo.Current_items_count != 0)
          {
            //Доделать
            SelectedItemsListBox.Items.Add(warehouseInfo.ProductName);
            shoppingBasket.Add(new Product { Name = "Warehouse", ID = warehouseInfo.ProductName });
            int index = WarehouseInformationList.IndexOf(warehouseInfo);
            warehouseInfo.Current_items_count--;
            //WarehouseInformationList[index] = warehouseInfo;
            currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) - 1);
            Price_list price = PriceList.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
            PriceLabel.Text = Convert.ToString(Math.Round(decimal.Parse(PriceLabel.Text) + currentPrice, 2));
          }
          else
            MessageBox.Show($"Нет возможности выбрать {warehouseInfo.ProductName}!");

          if(SearchInfo.Text.Length > 0)
          {
            SearchInfo_TextChanged(sender, e);
          }
          else
          {
            ViewInfoInDataGrid();
          }
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    //Нужен для отображения детальных сведений о товаре и установка их макс. кол-ва на запрос 
    private void AllInfoDatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (AllInfoDatagridView.SelectedCells.Count > 0)
        {
            AddProduct.Maximum = 100;
            int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
            DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];
            AddProduct.Value = (int)currentRow.Cells[3].Value;
            //AddProduct.Maximum = (int)currentRow.Cells[3].Value;
            Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
              i.Product_ID == (int)currentRow.Cells[0].Value);

            ViewInfoAboutComponent(AllProductInfo, warehouseInfo);
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    //Нужен для удаления товара из списка выбранных
    private void SelectedItemsListBox_DoubleClick(object sender, EventArgs e)
    {
      if(SelectedItemsListBox.SelectedIndex != -1)
      {
        Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
          i.ProductName == (string)SelectedItemsListBox.Items[SelectedItemsListBox.SelectedIndex]);
         
        DataGridViewRow currentRow = AllInfoDatagridView.Rows.Cast<DataGridViewRow>().Where(i => 
          (string)i.Cells[1].Value == warehouseInfo.ProductName).First();
        Product currentProduct = (Product)(from b in shoppingBasket
                              where b.ID == warehouseInfo.ProductName && b.Name == "Warehouse"
                              select b).FirstOrDefault();
        if(currentProduct == null)
          currentProduct = (Product)(from b in shoppingBasket
                                  where b.ID == warehouseInfo.ProductName
                                  select b).FirstOrDefault();
        SelectedItemsListBox.Items.Remove(SelectedItemsListBox.SelectedItem);
        int index = WarehouseInformationList.IndexOf(warehouseInfo);
        if(currentProduct.Name == "Shop")
        {
          warehouseInfo.Items_in_shop++;
          currentRow.Cells[4].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[4].Value) + 1);
          WarehouseInformationList[index] = warehouseInfo;
        }
        else
        {
          warehouseInfo.Current_items_count++;
          currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) + 1);
        }
        
        ViewInfoInDataGrid();

        shoppingBasket.RemoveAt(shoppingBasket.IndexOf(currentProduct));
         Price_list price = PriceList.Single(i => i.Product_ID == warehouseInfo.Product_ID);
        decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
        PriceLabel.Text = Convert.ToString(Math.Round(decimal.Parse(PriceLabel.Text) - currentPrice, 2));
      }
    }

    //Нужен для отправки запроса на получение товаров со склада
    private void RequestComponents_Click(object sender, EventArgs e)
    {
      if((AddProduct.Value != 0)&&(AllInfoDatagridView.SelectedCells.Count > 0))
      {
        int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];

        Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
          i.Product_ID == (int)currentRow.Cells[0].Value);
                
        ShopRequests newRequest = new ShopRequests(warehouseInfo.Product_ID, 
          Convert.ToInt32(AddProduct.Value),user.ID);
        using (ApplicationContext db = new ApplicationContext())
        {
          db.ShopRequests.Add(newRequest);
          List<NeedToUpdate> update = db.NeedToUpdate.ToList();
          update[0].UpdateStatusForWarehouse = true;
          db.NeedToUpdate.Update(update[0]);
          db.SaveChanges();
          MessageBox.Show("Товар успешно запрошен");
        }
      }
      else
        MessageBox.Show("Ошибка! Либо количество товара указано неверно, либо товар не был выбран.");
    }

    //Нужен для оформления покупки
    private void SellComponents_Click(object sender, EventArgs e)
    {
      bool takeFromWarehouse = false;//для отсутствия повторного запроса на самовызов
      if((SelectedItemsListBox.Items.Count > 0) &&(InTimeRadio.Checked || InManyTimeRadio.Checked))
      {
        string question = "Вы действительно хотите оформить покупку";
        DialogResult questionResult = MessageBox.Show(question,
                                    "Оформление покупки",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Information,
                                      MessageBoxDefaultButton.Button2,
                                      MessageBoxOptions.DefaultDesktopOnly);
        if (questionResult == DialogResult.Yes)
        {
          string deviceType = "";
          string ShopUserName = "";
          string WarehouseUserName = "";
          using(ApplicationContext db = new ApplicationContext())
          {
            ShopUserName = db.Users.Single(i => i.ID == user.ID).Name;
            WarehouseUserName = db.Users.Single(i => i.Authorization_status == true).Name;
          }
          string namesInPrint = $"\t \t \t \t Работник магазина: {ShopUserName}\n";
          
          if(WarehouseUserName.Length != 0)
            namesInPrint += $"\t \t \t \t Работник склада: {WarehouseUserName}\n";
          Warehouse_info warehouse = new Warehouse_info();
          for (int i = 0; i < shoppingBasket.Count; i++)
          {
            using (ApplicationContext db = new ApplicationContext())
            {
              warehouse = WarehouseInformationList.Single(k =>
                k.ProductName == Convert.ToString(shoppingBasket[i].ID));
              deviceType = db.Mediator.Single(k => k.ID == warehouse.Product_ID).Components_type;
            }
            if(shoppingBasket[i].Name == "Shop")
            {
              Price_list price = PriceList.Single(q => q.Product_ID == warehouse.Product_ID);
              decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
              string paymentMethod = InTimeRadio.Checked == true ? "единовременно" : "в рассрочку";
              SQLRequests.CreateHoldingDocument(warehouse, user, deviceType, currentPrice, paymentMethod);
            }
            else
            {
              takeFromWarehouse = true;
              ShopRequests newRequest = new ShopRequests(warehouse.Product_ID, 1, user.ID);
              using(ApplicationContext db = new ApplicationContext())
              {
                db.ShopRequests.Add(newRequest);
                List<NeedToUpdate> update = db.NeedToUpdate.ToList();
                update[0].UpdateStatusForWarehouse = true;
                db.NeedToUpdate.Update(update[0]);
                db.SaveChanges();
              }
              
                string printMessage = "";
                finalPrintMessage += namesInPrint;
                switch(warehouse.ProductType)
                {
                  case "CPU":
                    CPU currentCPU = Cpus.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentCPU.ID};\n" +
                      $"Наименование: {currentCPU.Name};\n";
                    break;
                  case "Cooling system":
                    Cooling_system currentCoolSys = CoolingSystems.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentCoolSys.ID};\n" +
                      $"Наименование: {currentCoolSys.Name};\n";
                    break;
                  case "GPU":
                    GPU currentGPU = Gpus.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentGPU.ID};\n" +
                      $"Наименование: {currentGPU.Name};\n";
                    break;
                  case "Motherboard":
                    Motherboard currentMotherboard = Motherboards.Single(Q => Q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentMotherboard.ID};\n" +
                      $"Наименование: {currentMotherboard.Name};\n";
                    break;
                  case "PSU":
                    PSU currentPSU = Psus.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentPSU.ID};\n" +
                      $"Наименование: {currentPSU.Name};\n";
                    break;
                  case "RAM":
                    RAM currentRAM = Rams.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentRAM.ID};\n" +
                      $"Наименование: {currentRAM.Name}; \n";
                    break;
                  case "SD":
                    Storage_devices currentStorage = StorageDevices.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentStorage.ID};\n" +
                      $"Наименование: {currentStorage.Name};\n";
                    break;
                  case "Case":
                    Case currentCase = Cases.Single(q => q.Product_ID == warehouse.Product_ID);
                    printMessage = $"ID товара: {currentCase.ID};\n" +
                      $"Наименование: {currentCase.Name};\n";
                    break;
                  default:
                    break;
                }
                finalPrintMessage += printMessage + "\n";
            }
          }
          if(takeFromWarehouse)
          {
            string questionAboutWarehouse = "Будет осуществлён самовывоз товара?";
            DialogResult questionResultWarehouse = MessageBox.Show(questionAboutWarehouse,
                                        "Подготовка запроса",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Information,
                                          MessageBoxDefaultButton.Button2,
                                          MessageBoxOptions.DefaultDesktopOnly);
            if(questionResultWarehouse == DialogResult.Yes)
            {
              PrintDocument printDocument = new PrintDocument();
              // обработчик события печати
              printDocument.PrintPage += PrintPageHandler;
              // диалог настройки печати
              PrintDialog printDialog = new PrintDialog();
              // установка объекта печати для его настройки
              printDialog.Document = printDocument;
              printDialog.Document.Print();
            }
            finalPrintMessage = "";
          }
          shoppingBasket.Clear();
          SelectedItemsListBox.Items.Clear();
          SQLRequests.UpdateWarehouseData();
          AllProductInfo.Clear();
          PriceLabel.Text = "0";
          MessageBox.Show("Покупка оформлена успешно.");
        }
        else
          MessageBox.Show("Действие отменено");
      }
      else
        MessageBox.Show("Не выбраны комплектующие для продажи или способ оплаты");
    }

    private void PCConfigurationPage_Enter(object sender, EventArgs e)
    {
      //очистка второго раздела
    }

    private void SellingOtherComponentsPage_Enter(object sender, EventArgs e)
    {
      //очистка первого раздела
    }

    private void CreateConfiguration_Click(object sender, EventArgs e)
    {
      bool isNullComboBoxes = SystemFunctions.CheckNullForComboBox(CPU_ComboBox, GPU_ComboBox,
        Motherboard_ComboBox, PSU_ComboBox, RAM_ComboBox, Case_ComboBox, 
        CoolingSystem_ComboBox, StorageDevice_ComboBox);
      if (!isNullComboBoxes)
      {
        bool pcInDb = false;
        List<Mediator> Computers = new List<Mediator>();
        Mediator currentPC = new Mediator(CPU_ComboBox.Text, GPU_ComboBox.Text, Case_ComboBox.Text,
          RAM_ComboBox.Text, CoolingSystem_ComboBox.Text, PSU_ComboBox.Text, StorageDevice_ComboBox.Text, 
          Motherboard_ComboBox.Text);
        using (ApplicationContext db = new ApplicationContext())
        {
          Computers = db.Mediator.Where(i => i.Components_type == "PC").ToList();
          foreach(Mediator m in Computers)
          {
            if(m.Case_ID == currentPC.Case_ID && m.Cooling_system_ID == currentPC.Cooling_system_ID &&
               m.CPU_ID == currentPC.CPU_ID && m.GPU_ID == currentPC.GPU_ID && m.Motherboard_ID == currentPC.Motherboard_ID &&
               m.PSU_ID == currentPC.PSU_ID && m.RAM_ID == currentPC.RAM_ID && m.SD_ID == currentPC.SD_ID)
              pcInDb = true;
          }
          if(pcInDb)
          {
            MessageBox.Show("Такая конфигурация присутствует в базе данных");
          }
          else
          {
            db.Mediator.Add(currentPC);
            db.SaveChanges();
            LoadPCConfigs();
            ViewPCsInDataGrid();
            MessageBox.Show("Добавление прошло успешно");
            ClearConfig_Click(sender, e);
          }
        }
      }
      else MessageBox.Show("Не все поля заполнены!");
    }

    //Нужен для отображения детальных сведений о комплектующих
    private void ViewInfoAboutComponent(RichTextBox textBox, Warehouse_info currentItem)
    {
      switch (currentItem.ProductType)
      {
        case "CPU":
          CPU currentCPU = Cpus.Single(i => i.Product_ID == currentItem.Product_ID);
          string integratedGPU = currentCPU.Integrated_graphic? "поддерживается" : "не поддерживается";
          int multi = currentCPU.Multithreading? 2 : 1;
          textBox.Text = $"ID товара: {currentCPU.ID};\n" +
            $"Наименование: {currentCPU.Name};\n" +
            $"Модельный ряд: {currentCPU.SeriesName};\n" +
            $"Тип поставки: {currentCPU.Delivery_type};\n" +
            $"Кодовое название кристалла: {currentCPU.CodeName};\n" +
            $"Сокет: {currentCPU.Socket};\n" +
            $"Кол-во ядер: {currentCPU.Сores_count}, потоков: {currentCPU.Сores_count * multi};\n" +
            $"Частоты (min/max): " +
            $"{(float)currentCPU.Base_state / 1000}/{(float)currentCPU.Max_state / 1000} Ghz;\n" +
            $"Тип памяти / кол-во каналов: {currentCPU.RAM_type} / {currentCPU.RAM_chanel}\n" +
            $"Макс частота ОЗУ: {currentCPU.RAM_frequency} Mhz;\n" +
            $"Встроенная графика: {integratedGPU};\n" +
            $"Энергопотребление: {currentCPU.Consumption} Вт\n" +
            $"Техпроцесс: {currentCPU.Technical_process} нм\n";
          break;
        case "Cooling system":
          Cooling_system currentCoolSys = CoolingSystems.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentCoolSys.ID};\n" +
            $"Наименование: {currentCoolSys.Name};\n" +
            $"Поддерживаемые сокеты: {currentCoolSys.Supported_sockets};\n" +
            $"Количество тепловых трубок: {currentCoolSys.Count_of_heat_pipes};\n" +
            $"Тип подшипника: {currentCoolSys.Type_of_bearing};\n" +
            $"Уровень шума: {currentCoolSys.Noise_level} дБ;\n" +
            $"Тип питания: {currentCoolSys.PowerType};\n" +
            $"Максимальная скорость вращения кулера {currentCoolSys.Max_state} об/мин;\n" +
            $"Рассеиваемая мощность /  диаметр: {currentCoolSys.Consumption} / {currentCoolSys.Diameter} мм;\n";
          break;
        case "GPU":
          GPU currentGPU = Gpus.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentGPU.ID};\n" +
            $"Наименование: {currentGPU.Name};\n" +
            $"Интерфейс подключения: {currentGPU.ConnectionInterface};\n" +
            $"Производитель: {currentGPU.Manufacturer};\n" +
            $"Объём памяти: {Convert.ToString(currentGPU.Capacity)} Гб;\n" +
            $"Тип видеопамяти: {currentGPU.GPU_type};\n" +
            $"Ширина шины: {Convert.ToString(currentGPU.Bus_width)} бит; \n" +
            $"Разогнанная версия: ";
          textBox.Text += currentGPU.Overclocking? "да; \n" : "нет; \n";
          textBox.Text += $"Энергопотребление: {Convert.ToString(currentGPU.Consumption)} Вт;\n" +
            $"Версия DirectX: {currentGPU.DirectX};\n" +
            $"Внешние интерфейсы: {currentGPU.External_interfaces};\n" +
            $"Тип питания: {currentGPU.PowerType};\n" +
            $"Кол-во вентиляторов: {Convert.ToString(currentGPU.Coolers_count)};\n" +
            $"Толщина системы охлаждения: {Convert.ToString(currentGPU.Cooling_system_thikness)} слотов;\n" +
            $"Длина / высота видеокарты: {Convert.ToString(currentGPU.Length)} / " +
            $"{Convert.ToString(currentGPU.Height)} мм;\n";
          break;
        case "Motherboard":
          Motherboard currentMotherboard = Motherboards.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentMotherboard.ID};\n" +
            $"Наименование: {currentMotherboard.Name};\n" +
            $"Поддерживаемые процессоры: {currentMotherboard.Supported_CPU};\n" +
            $"Сокет / чипсет: {currentMotherboard.Socket} / {currentMotherboard.Chipset};\n" +
            $"Форм-фактор: {currentMotherboard.FormFactor};\n" +
            $"Тип / объём ОЗУ: {currentMotherboard.RAM_type} /" +
            $" {Convert.ToString(currentMotherboard.Capacity)} Гб;\n" +
            $"Кол-во слотов / каналов памяти: {Convert.ToString(currentMotherboard.Count_of_memory_slots)} /" +
            $" {Convert.ToString(currentMotherboard.RAM_chanel)}; \n" +
            $"Максимальная частота памяти: {Convert.ToString(currentMotherboard.RAM_frequency)} МГц; \n" +
            $"Слоты расширения: {currentMotherboard.Expansion_slots} \n" +
            $"Интерфейсы накопителей: {currentMotherboard.Storage_interfaces} \n" +
            $"Поддержка SLI / IGPU: ";
          textBox.Text += currentMotherboard.SLI_support? "Да" : "Нет";
          textBox.Text += " / ";
          textBox.Text += currentMotherboard.Integrated_graphic? "Да" : "Нет";
          textBox.Text += "\n";
          textBox.Text += $"Разъёмы: {currentMotherboard.Connectors}\n" +
            $"Длина / ширина платы: {Convert.ToString(currentMotherboard.Length)} / " +
            $"{Convert.ToString(currentMotherboard.Width)} мм\n";
          break;
        case "PSU":
          PSU currentPSU = Psus.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentPSU.ID};\n" +
            $"Наименование: {currentPSU.Name};\n" +
            $"Стандарт блока питания: {currentPSU.PSU_standart};\n" +
            $"Кол-во разъёмов SATA / отдельных линий +12V: {currentPSU.Sata_power_count} / " +
            $"{currentPSU.Line_plus_twelve_V_count} шт;\n" +
            $"Максимальный ток по линии +12V / КПД: {currentPSU.Max_amperage_on_line_plus_twelve} А / " +
            $"{currentPSU.Efficiency} %;\n" +
            $"Мощность: {currentPSU.Consumption} Вт;\n" +
            $"Тип питания материнской платы: {currentPSU.PowerMotherboardType};\n" +
            $"Питание CPU / IDE / PCIe: {currentPSU.Power_CPU} / {currentPSU.Power_IDE} /" +
            $" {currentPSU.Power_PCIe};\n" +
            $"Длина БП: {currentPSU.Length} мм;\n" +
            $"Доп. опции: ";
          var psuTuple = new List<(bool state, string elem)>
          {
            (currentPSU.Power_USB, "поддержка USB power"),
            (currentPSU.Modularity, "модульность")
          };
          foreach (var i in psuTuple)
            if (i.state)
              textBox.Text += i.elem + "; ";

          textBox.Text += "\n";
          break;
        case "RAM":
          RAM currentRAM = Rams.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentRAM.ID};\n" +
            $"Наименование: {currentRAM.Name}; \n" +
            $"Количество в наборе: {Convert.ToString(currentRAM.Kit)} шт.;\n" +
            $"Тип / частота памяти: {currentRAM.RAM_type} / " +
            $"{Convert.ToString(currentRAM.RAM_frequency)} МГц;\n" +
            $"Напряжение питания: {Convert.ToString(currentRAM.Voltage)} В;\n" +
            $"Объём памяти: {Convert.ToString(currentRAM.Capacity * currentRAM.Kit)} Гб;\n" +
            $"Тайминги: {currentRAM.Timings};\n" +
            $"Доп функции: ";
          var ramTuple = new List<(bool state, string elem)>
          {
            (currentRAM.XMP_profile, "поддержка XMP"),
            (currentRAM.Cooling, "охлаждение"),
            (currentRAM.Low_profile_module, "низкопрофильный модуль")
          };
          foreach (var i in ramTuple)
            if (i.state)
              textBox.Text += i.elem + "; ";

          textBox.Text += "\n";
          break;
        case "SD":
          Storage_devices currentStorage = StorageDevices.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentStorage.ID};\n" +
            $"Наименование: {currentStorage.Name};\n" +
            $"Объём / буфер: {currentStorage.Capacity} Гб / {currentStorage.Buffer} Мб;\n" +
            $"Интерфейс подключения: {currentStorage.ConnectionInterface};\n" +
            $"Форм-фактор: {currentStorage.FormFactor};\n" +
            $"Скорость послед. чтения / записи: {currentStorage.Sequential_read_speed} / " +
            $"{currentStorage.Sequeintial_write_speed} Мб/c;\n" +
            $"Скорость случ. чтения / записи: {currentStorage.Random_read_speed} / {currentStorage.Random_write_speed} Мб/c;\n" +
            $"Энергопотребление / толщина: {currentStorage.Consumption} Вт / {Math.Round(currentStorage.Thickness, 2)} мм\n" +
            $"Аппаратное шифрование: ";
          textBox.Text += currentStorage.Hardware_encryption ? "Да" : "Нет";
          textBox.Text += "\n";
          break;
        case "Case":
          Case currentCase = Cases.Single(i => i.Product_ID == currentItem.Product_ID);
          textBox.Text = $"ID товара: {currentCase.ID};\n" +
            $"Наименование: {currentCase.Name};\n" +
            $"Блок питания/расположение: {currentCase.Power_supply_unit} / {currentCase.PSU_position};\n" +
            $"Тип/материал корпуса: {currentCase.Form_factor_ID} / {currentCase.Material};\n" +
            $"Совместимые мат. платы: {currentCase.Compatible_motherboard};\n" +
            $"Вид охлаждения: {currentCase.Cooling_type};\n" +
            $"Доп функции: ";
          var caseTuple = new List<(bool state, string elem)>
          {
            (currentCase.Gaming, "игровой"),
            (currentCase.Water_cooling_support, "жидкостное охлаждение"),
            (currentCase.Cooler_in_set, "вентилятор в комплекте"),
            (currentCase.Sound_isolation, "шумоизоляция"),
            (currentCase.Dust_filter, "пылевые фильтры")
          };
          foreach (var i in caseTuple)
            if (i.state)
              textBox.Text += i.elem + "; ";

          textBox.Text += "\n" +
            $"Установлено кулеров/кол-во слотов для их установки: {currentCase.Coolers_count} /" +
            $" {currentCase.Coolers_slots};\n" +
            $"Высота/ширина/глубина: {Convert.ToString(currentCase.Height)} / " +
            $"{Convert.ToString(currentCase.Width)} / {Convert.ToString(currentCase.Depth)} мм;\n" +
            $"Отсеки под накопители/слоты расширения: " +
            $"{Convert.ToString(currentCase.Storage_sections_count)} / " +
            $"{Convert.ToString(currentCase.Expansion_slots_count)} шт.;\n" +
            $"Макс. длина GPU/высота кулера CPU/длина БП: {Convert.ToString(currentCase.Max_GPU_length)} / " +
            $"{Convert.ToString(currentCase.Max_CPU_cooler_height)} / " +
            $"{Convert.ToString(currentCase.Max_PSU_length)} мм;\n" +
            $"Вес: {Convert.ToString(currentCase.Weight)} Кг.\n";
          break;
        default:
          break;
      }
      Price_list price = PriceList.Single(i => i.Product_ID == currentItem.Product_ID);
      textBox.Text += $"Стоимость элемента: " +
        $"{Math.Round(price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100, 2)} руб.";
    }

    private void CPU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(CPU_ComboBox.SelectedIndex != -1 && CPU_ComboBox.Items.Count > 0)
      {
        AddConfigItemInListBox(CPU_ComboBox, ref cpuNameInListBox);
        int productID = WarehouseInformationList.Single(i => i.ProductName == cpuNameInListBox).Product_ID;
        string cpuID = Mediators.Single(i => i.ID == productID).CPU_ID;
        CPU currentCPU = Cpus.Single(i => i.ID == cpuID);
        cpuConsumptionForCooling = currentCPU.Consumption;
        socket = currentCPU.Socket;


        if(Motherboard_ComboBox.SelectedIndex == -1 && RAM_ComboBox.SelectedIndex == -1)
        {
          Motherboard_ComboBox.Items.Clear();
          filteredMotherboards = Motherboards.Where(i => i.Socket == socket).ToList();
          foreach(Motherboard m in filteredMotherboards)
          {
            Motherboard_ComboBox.Items.Add(m.Name);
          }
          maxRamFrequency = currentCPU.RAM_frequency;
          filteredRams = (from b in Rams
                          where b.RAM_frequency <= maxRamFrequency &&
                          b.RAM_type == currentCPU.RAM_type
                          select b).ToList();

          RAM_ComboBox.Items.Clear();
          foreach(RAM r in filteredRams)
            RAM_ComboBox.Items.Add(r.Name);
        }
        //правка сокета материнской платы
        if(Motherboard_ComboBox.SelectedIndex != -1 && RAM_ComboBox.SelectedIndex == -1)
        {
          int warehouseMotherboardID = WarehouseInformationList.Single(i => i.ProductName == motherboardNameInListBox).Product_ID;
          string motherboardID = Mediators.Single(i => i.ID == warehouseMotherboardID).Motherboard_ID;
          Motherboard motherboard = Motherboards.Single(i => i.ID == motherboardID);
          if(motherboard.Socket != socket)
          {
            SelectedConfigIntemsListBox.Items.Remove(motherboardNameInListBox);
            motherboardNameInListBox = "";
            Motherboard_ComboBox.SelectedItem = null;
            Motherboard_ComboBox.BackColor = Color.White;

            Motherboard_ComboBox.Items.Clear();
            filteredMotherboards = Motherboards.Where(i => i.Socket == socket).ToList();
            foreach(Motherboard m in filteredMotherboards)
            {
              Motherboard_ComboBox.Items.Add(m.Name);
            }
          }
          else
          {
            CheckIntegratedGraphicSupport(motherboardID, currentCPU);
            //определение макс частоты памяти
            maxRamFrequency = Math.Min(Cpus.Single(i => i.ID == cpuID).RAM_frequency,
              Motherboards.Single(i => i.ID == motherboardID).RAM_frequency);
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == currentCPU.RAM_type
                            select b).ToList();
            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);
          }
        }
        //если оперативная память уже выбрана
        if(Motherboard_ComboBox.SelectedIndex == -1 && RAM_ComboBox.SelectedIndex != -1)
        {
          int warehouseRamID = WarehouseInformationList.Single(i => i.ProductName == ramNameInListBox).Product_ID;
          string ramID = Mediators.Single(i => i.ID == warehouseRamID).RAM_ID;
          RAM ram = Rams.Single(i => i.ID == ramID);
          if(ram.RAM_type != currentCPU.RAM_type || ram.RAM_frequency > currentCPU.RAM_frequency)
          {
            SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
            ramNameInListBox = "";
            RAM_ComboBox.SelectedItem = null;
            RAM_ComboBox.BackColor = Color.White;
          
            maxRamFrequency = Cpus.Single(i => i.ID == cpuID).RAM_frequency;
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == currentCPU.RAM_type
                            select b).ToList();

            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);
            SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
            ramNameInListBox = "";
            RAM_ComboBox.SelectedItem = null;
            RAM_ComboBox.BackColor = Color.White;

            
          }
          Motherboard_ComboBox.Items.Clear();
          filteredMotherboards = Motherboards.Where(i => i.Socket == socket).ToList();
          foreach(Motherboard m in filteredMotherboards)
          {
            Motherboard_ComboBox.Items.Add(m.Name);
          }
        }

        if(Motherboard_ComboBox.SelectedIndex != -1 && RAM_ComboBox.SelectedIndex != -1)
        {
          int warehouseMotherboardID = WarehouseInformationList.Single(i => i.ProductName == motherboardNameInListBox).Product_ID;
          string motherboardID = Mediators.Single(i => i.ID == warehouseMotherboardID).Motherboard_ID;
          Motherboard motherboard = Motherboards.Single(i => i.ID == motherboardID);
          RAM currentRam = Rams.Single(i => i.Name == ramNameInListBox);
          if(motherboard.Socket != socket)
          {
            SelectedConfigIntemsListBox.Items.Remove(motherboardNameInListBox);
            motherboardNameInListBox = "";
            Motherboard_ComboBox.SelectedItem = null;
            Motherboard_ComboBox.BackColor = Color.White;

            Motherboard_ComboBox.Items.Clear();
            filteredMotherboards = Motherboards.Where(i => i.Socket == socket).ToList();
            foreach(Motherboard m in filteredMotherboards)
            {
              Motherboard_ComboBox.Items.Add(m.Name);
            }

            maxRamFrequency = Cpus.Single(i => i.ID == cpuID).RAM_frequency;
            
              filteredRams = (from b in Rams
                              where b.RAM_frequency <= maxRamFrequency &&
                              b.RAM_type == currentCPU.RAM_type
                              select b).ToList();
            
            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);
            if(currentRam.RAM_frequency > maxRamFrequency)
            {
              SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
              ramNameInListBox = "";
              RAM_ComboBox.SelectedItem = null;
              RAM_ComboBox.BackColor = Color.White;
            }
            else
              RAM_ComboBox.SelectedItem = ramNameInListBox;
          }
          else
          {
            maxRamFrequency = Math.Min(currentCPU.RAM_frequency, motherboard.RAM_frequency);
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == currentCPU.RAM_type
                            select b).ToList();

            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);

            if(currentRam.RAM_frequency > maxRamFrequency)
            {
              SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
              ramNameInListBox = "";
              RAM_ComboBox.SelectedItem = null;
              RAM_ComboBox.BackColor = Color.White;
            }
            else
              RAM_ComboBox.SelectedItem = ramNameInListBox;
          }
        }
      
        if(Motherboard_ComboBox.SelectedIndex == -1 && CoolingSystem_ComboBox.SelectedIndex != -1)
        {
          Motherboard_ComboBox.Items.Clear();
          filteredMotherboards = Motherboards.Where(i => i.Socket == socket).ToList();
          foreach(Motherboard m in filteredMotherboards)
          {
            Motherboard_ComboBox.Items.Add(m.Name);
          }
        }
        
      }
      CalculatePCConsumption();
      SetPSUByConsumption();
      CalculatePCPrice();
     
    }

    private void CalculatePCConsumption()
    {
      List<Warehouse_info> items = new List<Warehouse_info>();
      int index = 0;
      maxConsumption = 0;
      foreach(var i in SelectedConfigIntemsListBox.Items)
      {
        items.Add(WarehouseInformationList.Single(q => q.ProductName == i.ToString()));
        using(ApplicationContext db = new ApplicationContext())
        {
          if(db.CPU.Where(q => q.Name == items[index].ProductName).Count() > 0 || 
            db.GPU.Where(q => q.Name == items[index].ProductName).Count() > 0)
          {
            Energy_consumption consumption = db.Energy_consumption.Single(q => q.Product_ID == items[index].Product_ID);
            maxConsumption += consumption.Consumption;
          }
        }
          index++;
      }
      //SetPSUByConsumption();
    }

    private void SetPSUByConsumption()
    {
      if(PSU_ComboBox.SelectedIndex == -1)
      {
        PSU_ComboBox.Items.Clear();
        filteredPsus.Clear();
        foreach(PSU p in Psus)
        {
          if(p.Consumption >= maxConsumption)
          {
            filteredPsus.Add(p);
            PSU_ComboBox.Items.Add(p.Name);
          }
        }
      }
      else
      {
        PSU currentPSU = Psus.Single(i => i.Name == PSU_ComboBox.Text);
        string name = currentPSU.Consumption >= maxConsumption ? currentPSU.Name : "";
        PSU_ComboBox.Items.Clear();
        List<PSU> tempPSU;
        tempPSU = Psus.Where(i => i.Consumption >= maxConsumption).ToList();
        filteredPsus = tempPSU;
        foreach(PSU cs in filteredPsus)
        {
          PSU_ComboBox.Items.Add(cs.Name);
        }
        PSU_ComboBox.SelectedItem = name;
      }
    }


    private void CheckIntegratedGraphicSupport(string motherboardID, CPU selectedCPU)
    {
      if(!Motherboards.Single(i => i.ID == motherboardID).Integrated_graphic)
      {
        if(selectedCPU.Integrated_graphic)
          MessageBox.Show("Процессор имеет встроенную графику, а мп её не поддерживает!");
      }
      else
              if(!selectedCPU.Integrated_graphic)
        MessageBox.Show("Материнская плата поддерживает роботу со встроенным графическим ядром, а процессор его не имеет");
    }
    private void GPU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(GPU_ComboBox.SelectedIndex != -1 && GPU_ComboBox.Items.Count > 0)
      {
        AddConfigItemInListBox(GPU_ComboBox, ref gpuNameInListBox);
        int warehouseGpuID = WarehouseInformationList.Single(i => i.ProductName == gpuNameInListBox).Product_ID;
        string gpuID = Mediators.Single(i => i.ID == warehouseGpuID).GPU_ID;
        GPU currentGPU = Gpus.Single(i => i.ID == gpuID);
        if(Motherboard_ComboBox.SelectedIndex == -1)
        {
          if(CPU_ComboBox.SelectedIndex == -1 && CoolingSystem_ComboBox.SelectedIndex == -1 &&
            RAM_ComboBox.SelectedIndex == -1)//и корпус
          {
            filteredMotherboards = Motherboards.Where(b => b.Expansion_slots.Contains
              (currentGPU.ConnectionInterface)).ToList();
            Motherboard_ComboBox.Items.Clear();
            foreach(Motherboard m in filteredMotherboards)
            {
              Motherboard_ComboBox.Items.Add(m.Name);
            }
          }
          else
          {
            List<Motherboard> secondFilterMotherboards = filteredMotherboards;
            filteredMotherboards = secondFilterMotherboards.Where(b => b.Expansion_slots.Contains
              (currentGPU.ConnectionInterface)).ToList();
            Motherboard_ComboBox.Items.Clear();
            foreach(Motherboard m in filteredMotherboards)
            {
              Motherboard_ComboBox.Items.Add(m.Name);
            }
          }
        }
        else
        {
          //если материнка выбрана то ничего делать не нужно
        }
        if(Case_ComboBox.SelectedIndex == -1)
        {
          Case_ComboBox.Items.Clear();
          filteredCases.Clear();
          foreach(Case cs in Cases)
          {
            if(cs.Max_GPU_length >= currentGPU.Length)
            {
              Case_ComboBox.Items.Add(cs.Name);
              filteredCases.Add(cs);
            }
          }
        }
        else
        {
          Case currentCase = Cases.Single(i => i.Name == Case_ComboBox.Text);
          string name = currentCase.Max_GPU_length >= currentGPU.Length ? currentCase.Name : "";
          Case_ComboBox.Items.Clear();
          List<Case> tempCases = filteredCases.Where(i => i.Max_GPU_length >= currentGPU.Length).ToList();
          filteredCases = tempCases;
          foreach(Case cs in filteredCases)
          {
            Case_ComboBox.Items.Add(cs.Name);
          }
          Case_ComboBox.SelectedItem = name;
        }
      }
      CalculatePCPrice();
      CalculatePCConsumption();
      SetPSUByConsumption();
    }

    //Нужен для добавления компонентов в конфигураторе ПК
    private void AddConfigItemInListBox(ComboBox comboBox, ref string prevName)
    {
      if (comboBox.SelectedIndex != -1)
      {
        if (prevName != "" && SelectedConfigIntemsListBox.Items.Count > 0)
        {
          int index = -1;
          index = SelectedConfigIntemsListBox.Items.IndexOf(prevName);
          if(index != -1)
            SelectedConfigIntemsListBox.Items[index] = comboBox.Text;
          else
            SelectedConfigIntemsListBox.Items.Add(comboBox.Text);
        }
        else
          SelectedConfigIntemsListBox.Items.Add(comboBox.Text);

        Warehouse_info warehouse = WarehouseInformationList.Single(i => i.ProductName == comboBox.Text);
        comboBox.BackColor = warehouse.Items_in_shop + warehouse.Current_items_count == 0 ? Color.Red : Color.Green;
        ViewInfoAboutComponent(SelectedComponentInfoTextBox, warehouse);
        SelectedConfigIntemsListBox.SelectedIndex = -1;
        prevName = comboBox.Text;
      }
    }

    private void EnterPurchaseWindow_Click(object sender, EventArgs e)
    {
      if(SelectedConfigIntemsListBox.Items.Count > 0)
      {
        string needToBuy = "В данный момент купить следующие товары не предоставляется возможным: ";
        //переносить в правое окно для оформления 
        //предусмотреть что не все выбранные элементы могут быть в магазине или на складе
        ComboBox[] comboboxes = {CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox, PSU_ComboBox,
          RAM_ComboBox, Case_ComboBox, CoolingSystem_ComboBox, StorageDevice_ComboBox};
        foreach(ComboBox cb in comboboxes)
        {
          if(cb.BackColor == Color.Red)
          {
            needToBuy += cb.Text + "; ";
          }
          else
          {
            Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
             i.ProductName == cb.Text);
            if(warehouseInfo.Items_in_shop != 0)
            {
              SelectedItemsListBox.Items.Add(warehouseInfo.ProductName);
              shoppingBasket.Add(new Product { Name = "Shop", ID = warehouseInfo.ProductName });
              int index = WarehouseInformationList.IndexOf(warehouseInfo);
              warehouseInfo.Items_in_shop--;
              WarehouseInformationList[index] = warehouseInfo;
              Price_list price = PriceList.Single(i => i.Product_ID == warehouseInfo.Product_ID);
              decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
              PriceLabel.Text = Convert.ToString(Math.Round(decimal.Parse(PriceLabel.Text) + currentPrice, 2));
              ViewInfoInDataGrid();
            }
            else if(warehouseInfo.Current_items_count != 0)
            {
              //Доделать
              SelectedItemsListBox.Items.Add(warehouseInfo.ProductName);
              shoppingBasket.Add(new Product { Name = "Warehouse", ID = warehouseInfo.ProductName });
              int index = WarehouseInformationList.IndexOf(warehouseInfo);
              warehouseInfo.Current_items_count--;
              //WarehouseInformationList[index] = warehouseInfo;
              Price_list price = PriceList.Single(i => i.Product_ID == warehouseInfo.Product_ID);
              decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
              PriceLabel.Text = Convert.ToString(Math.Round(decimal.Parse(PriceLabel.Text) + currentPrice, 2));
              ViewInfoInDataGrid();
            }
          }
        }
        //сделать диалог, если нужен запрос, то сделать запрос
        string question = needToBuy + "\nВы хотите сформировать запрос на склад?";
        DialogResult questionResult = MessageBox.Show(question,
                                    "Оформление покупки",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Information,
                                      MessageBoxDefaultButton.Button2,
                                      MessageBoxOptions.DefaultDesktopOnly);
        if(questionResult == DialogResult.Yes)
        {
          foreach(ComboBox cb in comboboxes)
          {
            Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
             i.ProductName == cb.Text);
            ShopRequests newRequest = new ShopRequests(warehouseInfo.Product_ID, 1, user.ID);
            using(ApplicationContext db = new ApplicationContext())
            {
              db.ShopRequests.Add(newRequest);
              List<NeedToUpdate> update = db.NeedToUpdate.ToList();
              update[0].UpdateStatusForWarehouse = true;
              db.NeedToUpdate.Update(update[0]);
              db.SaveChanges();
            }
          }
          MessageBox.Show("Товары успешно запрошены");
        }

        tabControl1.SelectedIndex = 1;

        //заполнить набор shopBasket

        ClearConfig_Click(sender, e);
      }
      else
      {
        MessageBox.Show("Нужно выбрать товары для покупки");
      }
    }

    private void ChangeCPUComboBoxBySocket()
    {
      CPU_ComboBox.Items.Clear();
      foreach(CPU c in Cpus)
        if(c.Socket == socket)
          CPU_ComboBox.Items.Add(c.Name);
    }

    private void Motherboard_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(Motherboard_ComboBox.SelectedIndex != -1)
      {
        AddConfigItemInListBox(Motherboard_ComboBox, ref motherboardNameInListBox);
        int productID = WarehouseInformationList.Single(i => i.ProductName == motherboardNameInListBox).Product_ID;
        string motherboardID = Mediators.Single(i => i.ID == productID).Motherboard_ID;
        Motherboard currentMotherboard = Motherboards.Single(i => i.ID == motherboardID);

        socket = currentMotherboard.Socket;
        if(CPU_ComboBox.SelectedIndex == -1 && CoolingSystem_ComboBox.SelectedIndex == -1 && 
          RAM_ComboBox.SelectedIndex == -1)
        {
          ChangeCPUComboBoxBySocket();

          CoolingSystem_ComboBox.Items.Clear();
          foreach(Cooling_system cs in CoolingSystems)
          {
            if(FindSupportedSocketByCoolingSystem(cs))
              CoolingSystem_ComboBox.Items.Add(cs.Name);
          }
          maxRamFrequency = currentMotherboard.RAM_frequency;
          filteredRams = (from b in Rams
                          where b.RAM_frequency <= maxRamFrequency &&
                          b.RAM_type == currentMotherboard.RAM_type
                          select b).ToList();

          RAM_ComboBox.Items.Clear();
          foreach(RAM r in filteredRams)
            RAM_ComboBox.Items.Add(r.Name);
        }

        if(GPU_ComboBox.SelectedIndex == -1)
        {
          if(filteredGpus.Count == 0)
          {
            filteredGpus = Gpus.Where(g => currentMotherboard.Expansion_slots.Contains(g.ConnectionInterface)).ToList();
            GPU_ComboBox.Items.Clear();
            foreach(GPU g in filteredGpus)
            {
              GPU_ComboBox.Items.Add(g.Name);
            }
          }
          else
          {
            List<GPU> tempGpus = filteredGpus;
            filteredGpus = tempGpus.Where(g => currentMotherboard.Expansion_slots.Contains(g.ConnectionInterface)).ToList();
            GPU_ComboBox.Items.Clear();
            foreach(GPU g in filteredGpus)
            {
              GPU_ComboBox.Items.Add(g.Name);
            }
          }
        }
        else
        {
          GPU currentGPU = Gpus.Single(i => i.Name == gpuNameInListBox);
          if(currentMotherboard.Expansion_slots.Contains(currentGPU.ConnectionInterface))
          {
            if(filteredGpus.Count == 0)
            {
              filteredGpus = Gpus.Where(g => currentMotherboard.Expansion_slots.Contains(g.ConnectionInterface)).ToList();
              //в идеале конечно делать проверку на равенство
              GPU_ComboBox.Items.Clear();
              foreach(GPU g in filteredGpus)
              {
                GPU_ComboBox.Items.Add(g.Name);
              }
              GPU_ComboBox.SelectedItem = gpuNameInListBox;
            }
            else
            {
              List<GPU> tempGpus = filteredGpus;
              filteredGpus = tempGpus.Where(g => currentMotherboard.Expansion_slots.Contains(g.ConnectionInterface)).ToList();
              GPU_ComboBox.Items.Clear();
              foreach(GPU g in filteredGpus)
              {
                GPU_ComboBox.Items.Add(g.Name);
              }
              GPU_ComboBox.SelectedItem = gpuNameInListBox;
            }
          }
          else
          {
            SelectedConfigIntemsListBox.Items.Remove(gpuNameInListBox);
            gpuNameInListBox = "";
            GPU_ComboBox.SelectedItem = null;
            GPU_ComboBox.BackColor = Color.White;
          }

        }

        if(StorageDevice_ComboBox.SelectedIndex == -1)
        {
          filteredStorageDevices = StorageDevices.Where(sd =>
          currentMotherboard.Storage_interfaces.Contains(sd.ConnectionInterface)).ToList();
          StorageDevice_ComboBox.Items.Clear();
          foreach(Storage_devices sd in filteredStorageDevices)
          {
            StorageDevice_ComboBox.Items.Add(sd.Name);
          }
        }

        //Если пустой только бокс с процессорами
        if(CPU_ComboBox.SelectedIndex == -1 && CoolingSystem_ComboBox.SelectedIndex != -1)
        {
          ChangeCPUComboBoxBySocket();

          int warehouseCsID = WarehouseInformationList.Single(i => i.ProductName == coolingSystemNameInListBox).Product_ID;
          string csID = Mediators.Single(i => i.ID == warehouseCsID).Cooling_system_ID;
          bool socketIsSup = false;

          Cooling_system selectedCS = CoolingSystems.Single(i => i.ID == csID);

          socketIsSup = FindSupportedSocketByCoolingSystem(selectedCS);

          if(!socketIsSup)
          {
            CoolingSystem_ComboBox.Items.Clear();
            foreach(Cooling_system cs in CoolingSystems)
            {
              if(FindSupportedSocketByCoolingSystem(cs))
                CoolingSystem_ComboBox.Items.Add(cs.Name);
            }
            SelectedConfigIntemsListBox.Items.Remove(coolingSystemNameInListBox);
            coolingSystemNameInListBox = "";
            CoolingSystem_ComboBox.SelectedItem = null;
            CoolingSystem_ComboBox.BackColor = Color.White;
          }
        }
        //Если пустой только бокс с системами охлаждения
        if(CPU_ComboBox.SelectedIndex != -1 && CoolingSystem_ComboBox.SelectedIndex == -1)
        {
          int warehouseCpuID = WarehouseInformationList.Single(i => i.ProductName == cpuNameInListBox).Product_ID;
          string cpuID = Mediators.Single(i => i.ID == warehouseCpuID).CPU_ID;

          CPU selectedCPU = Cpus.Single(i => i.ID == cpuID);
          if(selectedCPU.Socket != socket)
          {
            ChangeCPUComboBoxBySocket();
            SelectedConfigIntemsListBox.Items.Remove(cpuNameInListBox);
            cpuNameInListBox = "";
            CPU_ComboBox.SelectedItem = null;
            CPU_ComboBox.BackColor = Color.White;
          }
          //else 
          //  CheckIntegratedGraphicSupport(motherboardID, selectedCPU);
        }


        if(CPU_ComboBox.SelectedIndex != -1 && RAM_ComboBox.SelectedIndex != -1)
        {
          int warehouseCpuID = WarehouseInformationList.Single(i => i.ProductName == cpuNameInListBox).Product_ID;
          string cpuID = Mediators.Single(i => i.ID == warehouseCpuID).CPU_ID;

          CPU selectedCPU = Cpus.Single(i => i.ID == cpuID);
          RAM currentRam = Rams.Single(i => i.Name == ramNameInListBox);
          if(selectedCPU.Socket != socket)
          {
            ChangeCPUComboBoxBySocket();
            SelectedConfigIntemsListBox.Items.Remove(cpuNameInListBox);
            cpuNameInListBox = "";
            CPU_ComboBox.SelectedItem = null;
            CPU_ComboBox.BackColor = Color.White;

            maxRamFrequency = currentMotherboard.RAM_frequency;
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == currentMotherboard.RAM_type
                            select b).ToList();
            

            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);            
          }
          else
          {
            CheckIntegratedGraphicSupport(motherboardID, selectedCPU);
            maxRamFrequency = Math.Min(Cpus.Single(i => i.ID == cpuID).RAM_frequency,
              Motherboards.Single(i => i.ID == motherboardID).RAM_frequency);
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == selectedCPU.RAM_type
                            select b).ToList();
            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);
          }

          if(currentRam.RAM_frequency > maxRamFrequency)
          {
            SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
            ramNameInListBox = "";
            RAM_ComboBox.SelectedItem = null;
            RAM_ComboBox.BackColor = Color.White;
          }
          else
            RAM_ComboBox.SelectedItem = ramNameInListBox;
        }


        if(CPU_ComboBox.SelectedIndex == -1 && RAM_ComboBox.SelectedIndex == -1)
        {
          ChangeCPUComboBoxBySocket();
          maxRamFrequency = currentMotherboard.RAM_frequency;
          filteredRams = (from b in Rams
                          where b.RAM_frequency <= maxRamFrequency &&
                          b.RAM_type == currentMotherboard.RAM_type
                          select b).ToList();

          RAM_ComboBox.Items.Clear();
          foreach(RAM r in filteredRams)
            RAM_ComboBox.Items.Add(r.Name);
        }


        if(CPU_ComboBox.SelectedIndex != -1 && RAM_ComboBox.SelectedIndex == -1)
        {
          int warehouseCpuID = WarehouseInformationList.Single(i => i.ProductName == cpuNameInListBox).Product_ID;
          string cpuID = Mediators.Single(i => i.ID == warehouseCpuID).CPU_ID;
          CPU selectedCPU = Cpus.Single(i => i.ID == cpuID);
          if(selectedCPU.Socket != socket)
          {
            ChangeCPUComboBoxBySocket();
            SelectedConfigIntemsListBox.Items.Remove(cpuNameInListBox);
            cpuNameInListBox = "";
            CPU_ComboBox.SelectedItem = null;
            CPU_ComboBox.BackColor = Color.White;

            maxRamFrequency = Math.Min(selectedCPU.RAM_frequency, currentMotherboard.RAM_frequency);
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == selectedCPU.RAM_type
                            select b).ToList();

            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);
          }
          else
            CheckIntegratedGraphicSupport(motherboardID, selectedCPU);
        }

        if(CPU_ComboBox.SelectedIndex == -1 && RAM_ComboBox.SelectedIndex != -1)
        {
          int warehouseRamID = WarehouseInformationList.Single(i => i.ProductName == ramNameInListBox).Product_ID;
          string ramID = Mediators.Single(i => i.ID == warehouseRamID).RAM_ID;
          RAM ram = Rams.Single(i => i.ID == ramID);
          if(ram.RAM_type != currentMotherboard.RAM_type || ram.RAM_frequency > currentMotherboard.RAM_frequency)
          {
            SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
            ramNameInListBox = "";
            RAM_ComboBox.SelectedItem = null;
            RAM_ComboBox.BackColor = Color.White;

            maxRamFrequency = Motherboards.Single(i => i.ID == motherboardID).RAM_frequency;
            filteredRams = (from b in Rams
                            where b.RAM_frequency <= maxRamFrequency &&
                            b.RAM_type == currentMotherboard.RAM_type
                            select b).ToList();

            RAM_ComboBox.Items.Clear();
            foreach(RAM r in filteredRams)
              RAM_ComboBox.Items.Add(r.Name);
            SelectedConfigIntemsListBox.Items.Remove(ramNameInListBox);
            ramNameInListBox = "";
            RAM_ComboBox.SelectedItem = null;
            RAM_ComboBox.BackColor = Color.White;
          }
        }

        //корректировка сокета системы охлаждения
        if(CoolingSystem_ComboBox.SelectedIndex != -1)
        {
          int warehouseCsID = WarehouseInformationList.Single(i => i.ProductName == coolingSystemNameInListBox).Product_ID;
          string csID = Mediators.Single(i => i.ID == warehouseCsID).Cooling_system_ID;
          bool socketIsSup = false;

          Cooling_system selectedCS = CoolingSystems.Single(i => i.ID == csID);

          socketIsSup = FindSupportedSocketByCoolingSystem(selectedCS);

          if(!socketIsSup)
          {
            CoolingSystem_ComboBox.Items.Clear();
            foreach(Cooling_system cs in CoolingSystems)
            {
              if(FindSupportedSocketByCoolingSystem(cs))
                CoolingSystem_ComboBox.Items.Add(cs.Name);
            }
            SelectedConfigIntemsListBox.Items.Remove(coolingSystemNameInListBox);
            coolingSystemNameInListBox = "";
            CoolingSystem_ComboBox.SelectedItem = null;
            CoolingSystem_ComboBox.BackColor = Color.White;
          }
        }


        if(Case_ComboBox.SelectedIndex == -1)
        {
          Case_ComboBox.Items.Clear();
          filteredCases.Clear();
          foreach(Case cs in Cases)
          {
            string[] forms = cs.Compatible_motherboard.Split(new char[] { ',' });
            for(int i = 0; i < forms.Length; i++)
            {
              forms[i] = forms[i].Trim();
            }
            foreach(var f in forms)
            {
              if(f.Equals(currentMotherboard.FormFactor))
              {
                Case_ComboBox.Items.Add(cs.Name);
                filteredCases.Add(cs);
                break;
              }
            }
          }
        }
        else
        {
          Case currentCase = Cases.Single(i => i.Name == Case_ComboBox.Text);
          string name = "";
          string[] forms = currentCase.FormFactor.Split(new char[] { ',' });
          for(int i = 0; i < forms.Length; i++)
          {
            forms[i] = forms[i].Trim();
          }
          foreach(var f in forms)
          {
            if(f == currentMotherboard.FormFactor)
            {
              name = currentCase.Name;
              break;
            }
          }
          Case_ComboBox.Items.Clear();
          List<Case> tempCases = new List<Case>();
          foreach(Case cs in filteredCases)
          {
            string[] forms1 = cs.FormFactor.Split(new char[] { ',' });
            for(int i = 0; i < forms1.Length; i++)
            {
              forms1[i] = forms1[i].Trim();
            }
            foreach(var f in forms)
            {
              if(f == currentMotherboard.FormFactor)
              {
                tempCases.Add(cs);
                break;
              }
            }
          }
          filteredCases = tempCases;
          foreach(Case cs in filteredCases)
          {
            Case_ComboBox.Items.Add(cs.Name);
          }
          Case_ComboBox.SelectedItem = name;
        }
      }
      CalculatePCPrice();
    }

    private bool FindSupportedSocketByCoolingSystem(Cooling_system cs)
    {
      string[] supSockets = cs.Supported_sockets.Split(new char[] { ',' });
      //т.к. список идет через пробелы их нужно убрать
      foreach(string s in supSockets)
      {
        string temp = s.TrimStart(' ');
        if(temp.Equals(socket))     
          return true;
      }
      return false;
    }

    //Нужен для отображения детальных сведений о комплектующих
    private void SelectedConfigIntemsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(SelectedConfigIntemsListBox.SelectedIndex != -1)
      {
        Warehouse_info warehouse = WarehouseInformationList.Single(i =>
          i.ProductName == Convert.ToString(SelectedConfigIntemsListBox.SelectedItem));
        ViewInfoAboutComponent(SelectedComponentInfoTextBox, warehouse);
      }
    }

    private void RAM_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(RAM_ComboBox.SelectedIndex != -1)
      {
        AddConfigItemInListBox(RAM_ComboBox, ref ramNameInListBox);
        int warehouseRamID = WarehouseInformationList.Single(i => i.ProductName == ramNameInListBox).Product_ID;
        string ramID = Mediators.Single(i => i.ID == warehouseRamID).RAM_ID;
        RAM currentRAM = Rams.Single(i => i.ID == ramID);
        maxRamFrequency = currentRAM.RAM_frequency;
        ramType = currentRAM.RAM_type;        
        if(CPU_ComboBox.SelectedIndex == -1 && Motherboard_ComboBox.SelectedIndex == -1)
        {
          filteredCpus = (from b in Cpus
                          where b.RAM_type == ramType && b.RAM_frequency >= maxRamFrequency
                          select b).ToList();
          CPU_ComboBox.Items.Clear();
          foreach(CPU c in filteredCpus)
            CPU_ComboBox.Items.Add(c.Name);

          filteredMotherboards = (from b in Motherboards
                                  where b.RAM_type == ramType && b.RAM_frequency >= maxRamFrequency
                                  select b).ToList();
          Motherboard_ComboBox.Items.Clear();
          foreach(Motherboard m in filteredMotherboards)
            Motherboard_ComboBox.Items.Add(m.Name);
        }
        
      }
      CalculatePCPrice();
      CalculatePCConsumption();
    }

    private void CoolingSystem_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(CoolingSystem_ComboBox.SelectedIndex != -1)
      {
        AddConfigItemInListBox(CoolingSystem_ComboBox, ref coolingSystemNameInListBox);
        int warehouseCsID = WarehouseInformationList.Single(i => i.ProductName == coolingSystemNameInListBox).Product_ID;
        string csID = Mediators.Single(i => i.ID == warehouseCsID).Cooling_system_ID;

        Cooling_system selectedCS = CoolingSystems.Single(i => i.ID == csID);
        cpuConsumptionForCooling = selectedCS.Consumption;
        if(CPU_ComboBox.SelectedIndex == -1)
        {
          CPU_ComboBox.Items.Clear();
          foreach(CPU c in Cpus)
          {
            if(c.Consumption <= cpuConsumptionForCooling)
              CPU_ComboBox.Items.Add(c.Name);
          }
          return;
        }
        if(CPU_ComboBox.SelectedIndex != -1)
        {
          int warehouseCpuID = WarehouseInformationList.Single(i => i.ProductName == cpuNameInListBox).Product_ID;
          string cpuID = Mediators.Single(i => i.ID == warehouseCpuID).CPU_ID;

          CPU selectedCPU = Cpus.Single(i => i.ID == cpuID);
          if(selectedCPU.Socket != socket)
          {
            ChangeCPUComboBoxBySocket();
            SelectedConfigIntemsListBox.Items.Remove(cpuNameInListBox);
            cpuNameInListBox = "";
            CPU_ComboBox.SelectedItem = null;
            CPU_ComboBox.BackColor = Color.White;
          }
        }
      }
      
      CalculatePCConsumption();
      CalculatePCPrice();
    }

    private void PSU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(PSU_ComboBox.SelectedIndex != -1)
      {
        PSU currentPSU = Psus.Single(i => i.Name == PSU_ComboBox.Text);
        AddConfigItemInListBox(PSU_ComboBox, ref psuNameInListBox);

        if(Case_ComboBox.SelectedIndex == -1)
        {
          Case_ComboBox.Items.Clear();
          filteredCases.Clear();
          foreach(Case cs in Cases)
          {
            if(cs.Max_PSU_length >= currentPSU.Length)
            {
              Case_ComboBox.Items.Add(cs.Name);
              filteredCases.Add(cs);
            }
          }
        }
        else
        {
          Case currentCase = Cases.Single(i => i.Name == Case_ComboBox.Text);
          string name = currentCase.Max_PSU_length >= currentPSU.Length ? currentCase.Name : "";
          Case_ComboBox.Items.Clear();
          List<Case> tempCases = filteredCases.Where(i => i.Max_PSU_length >= currentPSU.Length).ToList();
          filteredCases = tempCases;
          foreach(Case cs in filteredCases)
          {
            Case_ComboBox.Items.Add(cs.Name);
          }
          Case_ComboBox.SelectedItem = name;
        }

      }
      CalculatePCPrice();
    }


    private void StorageDevice_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(StorageDevice_ComboBox, ref storageNameInListBox);
      CalculatePCPrice();
      CalculatePCConsumption();
    }

    private void Case_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(Case_ComboBox.SelectedIndex != -1)
      {
        AddConfigItemInListBox(Case_ComboBox, ref caseNameInListBox);
        CalculatePCPrice();
        CalculatePCConsumption();
      }
    }

    private void SelectedItemsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(SelectedItemsListBox.SelectedIndex != -1)
      {
        Warehouse_info warehouse = WarehouseInformationList.Single(i =>
          i.ProductName == Convert.ToString(SelectedItemsListBox.SelectedItem));
        ViewInfoAboutComponent(AllProductInfo, warehouse);
      }
    }

    private void обновитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string question = "При обновлении могут пропасть несохранённые данные," +
        "продолжить?";
      DialogResult questionResult = MessageBox.Show(question,
                                    "Обновление информации",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button2,
                                    MessageBoxOptions.DefaultDesktopOnly);
      if(questionResult == DialogResult.Yes)
      {
        Update();
      }
    }

    private void PrintButton_Click(object sender, EventArgs e)
    {
      if(SelectedItemsListBox.Items.Count > 0)
      {
        int count = 0;
        int lastCount = 0;
        //печать
        string printMessage = "";
        string userName = "";
        using(ApplicationContext db = new ApplicationContext())
          userName = db.Users.Single(i => i.ID == user.ID).Name;
        finalPrintMessage = $"\t \t \t \t Работник магазина: {userName}\n";
        List<Product> items = new List<Product>();
        
        foreach(string i in SelectedItemsListBox.Items)
        {
          Product p = new Product();
          p.Name = i;
          p.ID = "0";
          items.Add(p);
        }
        var groupedItems = items.GroupBy(i => i.Name).Select(g => new Product{ Name = g.Key, ID = Convert.ToString(g.Count()) });
        lastCount = items.Count();
        foreach(var b in groupedItems)
        {
          count++;
          Warehouse_info itemInfo = WarehouseInformationList.Single(i => i.ProductName == b.Name);
          switch(itemInfo.ProductType)
          {
            case "CPU":
              CPU currentCPU = Cpus.Single(i => i.Product_ID == itemInfo.Product_ID);
              string integratedGPU = currentCPU.Integrated_graphic ? "поддерживается" : "не поддерживается";
              int multi = currentCPU.Multithreading ? 2 : 1;
              printMessage = $"ID товара: {currentCPU.ID};\n" +
                $"Наименование: {currentCPU.Name};\n" +
                $"Модельный ряд: {currentCPU.SeriesName};\n" +
                $"Тип поставки: {currentCPU.Delivery_type};\n" +               
                $"Сокет: {currentCPU.Socket};\n" +
                $"Кол-во ядер: {currentCPU.Сores_count}, потоков: {currentCPU.Сores_count * multi};\n" +
                $"Частоты (min/max): " +
                $"{(float)currentCPU.Base_state / 1000}/{(float)currentCPU.Max_state / 1000} Ghz;\n" +
                $"Тип памяти / кол-во каналов: {currentCPU.RAM_type} / {currentCPU.RAM_chanel}\n" +
                $"Макс частота ОЗУ: {currentCPU.RAM_frequency} Mhz;\n" +
                $"Встроенная графика: {integratedGPU};\n" +
                $"Энергопотребление: {currentCPU.Consumption} Вт\n" +
                $"Техпроцесс: {currentCPU.Technical_process} нм\n";
              break;
            case "Cooling system":
              Cooling_system currentCoolSys = CoolingSystems.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentCoolSys.ID};\n" +
                $"Наименование: {currentCoolSys.Name};\n" +
                $"Поддерживаемые сокеты: {currentCoolSys.Supported_sockets};\n" +
                $"Тип питания: {currentCoolSys.PowerType};\n" +
                $"Рассеиваемая мощность /  диаметр: {currentCoolSys.Consumption} / {currentCoolSys.Diameter} мм;\n";
              break;
            case "GPU":
              GPU currentGPU = Gpus.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentGPU.ID};\n" +
                $"Наименование: {currentGPU.Name};\n" +
                $"Интерфейс подключения: {currentGPU.ConnectionInterface};\n" +
                $"Объём памяти: {Convert.ToString(currentGPU.Capacity)} Гб;\n" +
                $"Тип видеопамяти: {currentGPU.GPU_type};\n";
              printMessage += $"Энергопотребление: {Convert.ToString(currentGPU.Consumption)} Вт;\n" +
                $"Внешние интерфейсы: {currentGPU.External_interfaces};\n" +
                $"Тип питания: {currentGPU.PowerType};\n" +
                $"Кол-во вентиляторов: {Convert.ToString(currentGPU.Coolers_count)};\n" +
                $"Толщина системы охлаждения: {Convert.ToString(currentGPU.Cooling_system_thikness)} слотов;\n" +
                $"Длина / высота видеокарты: {Convert.ToString(currentGPU.Length)} / " +
                $"{Convert.ToString(currentGPU.Height)} мм;\n";
              break;
            case "Motherboard":
              Motherboard currentMotherboard = Motherboards.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentMotherboard.ID};\n" +
                $"Наименование: {currentMotherboard.Name};\n" +
                $"Поддерживаемые процессоры: {currentMotherboard.Supported_CPU};\n" +
                $"Сокет / чипсет: {currentMotherboard.Socket} / {currentMotherboard.Chipset};\n" +
                $"Форм-фактор: {currentMotherboard.FormFactor};\n" +
                $"Тип / объём ОЗУ: {currentMotherboard.RAM_type} /" +
                $" {Convert.ToString(currentMotherboard.Capacity)} Гб;\n" +
                $"Кол-во слотов / каналов памяти: {Convert.ToString(currentMotherboard.Count_of_memory_slots)} /" +
                $" {Convert.ToString(currentMotherboard.RAM_chanel)}; \n" +
                $"Максимальная частота памяти: {Convert.ToString(currentMotherboard.RAM_frequency)} МГц; \n" +
                $"Слоты расширения: {currentMotherboard.Expansion_slots} \n" +
                $"Интерфейсы накопителей: {currentMotherboard.Storage_interfaces} \n" +
                $"Поддержка IGPU: ";
              printMessage += currentMotherboard.Integrated_graphic ? "Да" : "Нет";
              printMessage += "\n";
              printMessage += $"Разъёмы: {currentMotherboard.Connectors}\n" +
                $"Длина / ширина платы: {Convert.ToString(currentMotherboard.Length)} / " +
                $"{Convert.ToString(currentMotherboard.Width)} мм\n";
              break;
            case "PSU":
              PSU currentPSU = Psus.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentPSU.ID};\n" +
                $"Наименование: {currentPSU.Name};\n" +
                $"Стандарт блока питания: {currentPSU.PSU_standart};\n" +
                $"Кол-во разъёмов SATA: {currentPSU.Sata_power_count};\n" +
                $"{currentPSU.Efficiency} %;\n" +
                $"Мощность: {currentPSU.Consumption} Вт;\n" +
                $"Тип питания материнской платы: {currentPSU.PowerMotherboardType};\n" +
                $"Питание CPU / IDE / PCIe: {currentPSU.Power_CPU} / {currentPSU.Power_IDE} /" +
                $" {currentPSU.Power_PCIe};\n" +
                $"Длина БП: {currentPSU.Length} мм;\n" +
                $"Доп. опции: ";
              var psuTuple = new List<(bool state, string elem)>
          {
            (currentPSU.Power_USB, "поддержка USB power"),
            (currentPSU.Modularity, "модульность")
          };
              foreach(var i in psuTuple)
                if(i.state)
                  printMessage += i.elem + "; ";

              printMessage += "\n";
              break;
            case "RAM":
              RAM currentRAM = Rams.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentRAM.ID};\n" +
                $"Наименование: {currentRAM.Name}; \n" +
                $"Количество в наборе: {Convert.ToString(currentRAM.Kit)} шт.;\n" +
                $"Тип / частота памяти: {currentRAM.RAM_type} / " +
                $"{Convert.ToString(currentRAM.RAM_frequency)} МГц;\n" +
                $"Объём памяти: {Convert.ToString(currentRAM.Capacity * currentRAM.Kit)} Гб;\n" +
                $"Тайминги: {currentRAM.Timings};\n" +
                $"Доп функции: ";
              var ramTuple = new List<(bool state, string elem)>
              {
                (currentRAM.XMP_profile, "поддержка XMP"),
                (currentRAM.Cooling, "охлаждение"),
                (currentRAM.Low_profile_module, "низкопрофильный модуль")
              };
              foreach(var i in ramTuple)
                if(i.state)
                  printMessage += i.elem + "; ";

              printMessage += "\n";
              break;
            case "SD":
              Storage_devices currentStorage = StorageDevices.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentStorage.ID};\n" +
                $"Наименование: {currentStorage.Name};\n" +
                $"Объём / буфер: {currentStorage.Capacity} Гб / {currentStorage.Buffer} Мб;\n" +
                $"Интерфейс подключения: {currentStorage.ConnectionInterface};\n" +
                $"Форм-фактор: {currentStorage.FormFactor};\n" +
                $"Скорость послед. чтения / записи: {currentStorage.Sequential_read_speed} / " +
                $"{currentStorage.Sequeintial_write_speed} Мб/c;\n" +
                $"Скорость случ. чтения / записи: {currentStorage.Random_read_speed} / {currentStorage.Random_write_speed} Мб/c;\n" +
                $"Аппаратное шифрование: ";
              printMessage += currentStorage.Hardware_encryption ? "Да" : "Нет";
              printMessage += "\n";
              break;
            case "Case":
              Case currentCase = Cases.Single(i => i.Product_ID == itemInfo.Product_ID);
              printMessage = $"ID товара: {currentCase.ID};\n" +
                $"Наименование: {currentCase.Name};\n" +
                $"Блок питания / расположение: {currentCase.Power_supply_unit} / {currentCase.PSU_position};\n" +
                $"Тип/материал корпуса: {currentCase.Form_factor_ID} / {currentCase.Material};\n" +
                $"Совместимые мат. платы: {currentCase.Compatible_motherboard};\n" +
                $"Вид охлаждения: {currentCase.Cooling_type};\n" +
                $"Доп функции: ";
              var caseTuple = new List<(bool state, string elem)>
          {
            (currentCase.Gaming, "игровой"),
            (currentCase.Water_cooling_support, "жидкостное охлаждение"),
            (currentCase.Cooler_in_set, "вентилятор в комплекте"),
            (currentCase.Sound_isolation, "шумоизоляция"),
            (currentCase.Dust_filter, "пылевые фильтры")
          };
              foreach(var i in caseTuple)
                if(i.state)
                  printMessage += i.elem + "; ";

              printMessage += "\n" +
                $"Высота/ширина/глубина: {Convert.ToString(currentCase.Height)} / " +
                $"{Convert.ToString(currentCase.Width)} / {Convert.ToString(currentCase.Depth)} мм;\n" +
                $"Макс. длина GPU/высота кулера CPU/длина БП: {Convert.ToString(currentCase.Max_GPU_length)} / " +
                $"{Convert.ToString(currentCase.Max_CPU_cooler_height)} / " +
                $"{Convert.ToString(currentCase.Max_PSU_length)} мм;\n" +
                $"Вес: {Convert.ToString(currentCase.Weight)} Кг.\n";
              break;
            default:
              break;
          }
          
          Price_list price = PriceList.Single(i => i.Product_ID == itemInfo.Product_ID);
          decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
          printMessage += $"Количество товара: {b.ID}, стоимость {Math.Round(currentPrice * Convert.ToInt32(b.ID),2)} руб. \n";
          finalPrintMessage += printMessage + "\n";
          
          if(count % 4 == 0 || (lastCount - count == 0))
          {
            if(lastCount - count == 0)
              finalPrintMessage += $"Общая стоимость составит: {PriceLabel.Text} руб.";
            PrintDocument printDocument = new PrintDocument();
            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler;
            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();
            // установка объекта печати для его настройки
            printDialog.Document = printDocument;
            printDialog.Document.Print();
            finalPrintMessage = "";
          }
        }
        //также нужно печатать при оформлении элементы которые нужно забрать со склада!!!!
      }
      else
        MessageBox.Show("Товар не выбран!!!");
    }
    void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
      e.Graphics.DrawString(finalPrintMessage, new Font("Arial", 12), Brushes.Black, 0, 0);
    }

    private void CalculatePCPrice()
    {
      List<Warehouse_info> items = new List<Warehouse_info>();
      int index = 0;
      ConfigPriceLabel.Text = "0";
      foreach(var i in SelectedConfigIntemsListBox.Items)
      {
        items.Add(WarehouseInformationList.Single(q => q.ProductName == i.ToString()));
        Price_list price = PriceList.Single(q => q.Product_ID == items[index].Product_ID);
        decimal current_price = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
        ConfigPriceLabel.Text = Convert.ToString(Math.Round(Convert.ToDecimal(ConfigPriceLabel.Text) + current_price,2));
        index++;
      }
    }

    private void ClearConfig_Click(object sender, EventArgs e)
    {
      filteredCases.Clear();
      filteredCoolingSystems.Clear();
      filteredCpus.Clear();
      filteredGpus.Clear();
      filteredMotherboards.Clear();
      filteredPsus.Clear();
      filteredRams.Clear();
      filteredStorageDevices.Clear();

      ConfigPriceLabel.Text = "0";
      SelectedComponentInfoTextBox.Clear();
      SelectedConfigIntemsListBox.Items.Clear();

      SystemFunctions.Clear(CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);
      ViewInfoInComboBox<CPU>(Cpus, CPU_ComboBox);
      ViewInfoInComboBox<GPU>(Gpus, GPU_ComboBox);
      ViewInfoInComboBox<Motherboard>(Motherboards, Motherboard_ComboBox);
      ViewInfoInComboBox<RAM>(Rams, RAM_ComboBox);
      ViewInfoInComboBox<Cooling_system>(CoolingSystems, CoolingSystem_ComboBox);
      ViewInfoInComboBox<PSU>(Psus, PSU_ComboBox);
      ViewInfoInComboBox<Case>(Cases, Case_ComboBox);
      ViewInfoInComboBox<Storage_devices>(StorageDevices, StorageDevice_ComboBox);
      
      
    }

    private void ClearConfigInfoAfterChoosePC()
    {
      filteredCases.Clear();
      filteredCoolingSystems.Clear();
      filteredCpus.Clear();
      filteredGpus.Clear();
      filteredMotherboards.Clear();
      filteredPsus.Clear();
      filteredRams.Clear();
      filteredStorageDevices.Clear();

      ConfigPriceLabel.Text = "0";
      SelectedComponentInfoTextBox.Clear();
      //SelectedConfigIntemsListBox.Items.Clear();

      SystemFunctions.Clear(CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);
      ViewInfoInComboBox<CPU>(Cpus, CPU_ComboBox);
      ViewInfoInComboBox<GPU>(Gpus, GPU_ComboBox);
      ViewInfoInComboBox<Motherboard>(Motherboards, Motherboard_ComboBox);
      ViewInfoInComboBox<RAM>(Rams, RAM_ComboBox);
      ViewInfoInComboBox<Cooling_system>(CoolingSystems, CoolingSystem_ComboBox);
      ViewInfoInComboBox<PSU>(Psus, PSU_ComboBox);
      ViewInfoInComboBox<Case>(Cases, Case_ComboBox);
      ViewInfoInComboBox<Storage_devices>(StorageDevices, StorageDevice_ComboBox);
    }

    private void PCConfigsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if(PCConfigsDataGridView.SelectedCells.Count > 0)
      {
        ConfigPriceLabel.Text = "0";
        ClearConfigInfoAfterChoosePC();

        int selectedrowindex = PCConfigsDataGridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = PCConfigsDataGridView.Rows[selectedrowindex];
        Mediator SelectedPC = PCs.Single(i => i.ID == (int)currentRow.Cells[0].Value);
        using(ApplicationContext db = new ApplicationContext())
        {     
          CPU_ComboBox.SelectedItem = db.CPU.Single(i => i.ID == SelectedPC.CPU_ID).Name;
          GPU_ComboBox.SelectedItem = db.GPU.Single(i => i.ID == SelectedPC.GPU_ID).Name;
          Motherboard_ComboBox.SelectedItem = db.Motherboard.Single(i => i.ID == SelectedPC.Motherboard_ID).Name;
          RAM_ComboBox.SelectedItem = db.RAM.Single(i => i.ID == SelectedPC.RAM_ID).Name;
          CoolingSystem_ComboBox.SelectedItem = db.Cooling_system.Single(i => i.ID == SelectedPC.Cooling_system_ID).Name;
          PSU_ComboBox.SelectedItem = db.PSU.Single(i => i.ID == SelectedPC.PSU_ID).Name;
          StorageDevice_ComboBox.SelectedItem = db.Storage_devices.Single(i => i.ID == SelectedPC.SD_ID).Name;
          Case_ComboBox.SelectedItem = db.Case.Single(i => i.ID == SelectedPC.Case_ID).Name;
        }
        
      }
    }
  }
}