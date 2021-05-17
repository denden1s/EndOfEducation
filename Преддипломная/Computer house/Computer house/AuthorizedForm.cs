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
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
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

    //списки фильтрованных комплектующих
    private List<Case> filteredCases = new List<Case>();
    private List<Cooling_system> filteredCoolingSystems = new List<Cooling_system>();
    private List<CPU> filteredCpus = new List<CPU>();
    private List<GPU> filteredGpus = new List<GPU>();
    //private List<HDD> Hdds;
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

      ViewInfoInComboBox<CPU>(Cpus, CPU_ComboBox);
      ViewInfoInComboBox<GPU>(Gpus, GPU_ComboBox);
      ViewInfoInComboBox<Motherboard>(Motherboards, Motherboard_ComboBox);
      ViewInfoInComboBox<RAM>(Rams, RAM_ComboBox);
      ViewInfoInComboBox<Cooling_system>(CoolingSystems, CoolingSystem_ComboBox);
      ViewInfoInComboBox<PSU>(Psus, PSU_ComboBox);
      ViewInfoInComboBox<Case>(Cases, Case_ComboBox);
      ViewInfoInComboBox<Storage_devices>(StorageDevices, StorageDevice_ComboBox);

      LoadInfoFromDBAndView();

      LockOrEnableComboBox(true, CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox,
        RAM_ComboBox, CoolingSystem_ComboBox, PSU_ComboBox, StorageDevice_ComboBox, Case_ComboBox);
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
        using (ApplicationContext db = new ApplicationContext())
          foreach (Warehouse_info c in db.Warehouse_info)
            WarehouseInformationList.Add(new Warehouse_info(c.Product_ID, c.Current_items_count, c.Items_in_shop));
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

    private void SearchInfo_TextChanged(object sender, EventArgs e)
    {
      AllInfoDatagridView.Rows.Clear();
      AllProductInfo.Clear();
      if (SearchInfo.TextLength > 0)
      {
        try
        {
          List<Warehouse_info> SearchResultList = new List<Warehouse_info>();
          //Поиск по имени
          foreach (Warehouse_info i in WarehouseInformationList)
            if (i.ProductName.ToLower().Contains(SearchInfo.Text.ToLower()))
              SearchResultList.Add(i);

          //Если результат есть то вывести данные
          if (SearchResultList.Count != 0)
          {
            foreach (Warehouse_info wi in SearchResultList)
              AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.ProductType,
                wi.Current_items_count, wi.Items_in_shop);
          }
          //Если результат пустой то поиск осуществляется по ID
          else
          {
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
            //Проверка наличия такого ID как в строке поиска
            if (tempRequest != null)
            {
              foreach (Mediator i in tempRequest)
                SearchResultList.Add(WarehouseInformationList.Single(a => a.Product_ID == i.ID));

              //Добавление и вывод при успешном поиске
              if (SearchResultList.Count != 0)
              {
                foreach (Warehouse_info wi in SearchResultList)
                  AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.ProductType,
                    wi.Current_items_count, wi.Items_in_shop);
              }
              //В противном случае очистить таблицу
              else
                AllInfoDatagridView.Rows.Clear();
            }
            else
              AllInfoDatagridView.Rows.Clear();
          }
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
            int index = WarehouseInformationList.IndexOf(warehouseInfo);
            warehouseInfo.Items_in_shop--;
            WarehouseInformationList[index] = warehouseInfo;
            currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) - 1);
            Price_list price = PriceList.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            decimal currentPrice = price.Purchasable_price + price.Purchasable_price * price.Markup_percent / 100;
            PriceLabel.Text = Convert.ToString(Math.Round(decimal.Parse(PriceLabel.Text) + currentPrice, 2));
          }
          else
            MessageBox.Show($"Нет возможности выбрать {warehouseInfo.ProductName}!");

          ViewInfoInDataGrid();
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

        SelectedItemsListBox.Items.Remove(SelectedItemsListBox.SelectedItem);
        int index = WarehouseInformationList.IndexOf(warehouseInfo);
        warehouseInfo.Items_in_shop++;
        WarehouseInformationList[index] = warehouseInfo;
        ViewInfoInDataGrid();
        currentRow.Cells[4].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[4].Value) + 1);
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
          update[0].UpdateStatus = true;
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
      if(SelectedItemsListBox.Items.Count > 0)
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
          Warehouse_info warehouse = new Warehouse_info();
          for (int i = 0; i < SelectedItemsListBox.Items.Count; i++)
          {
            using (ApplicationContext db = new ApplicationContext())
            {
              warehouse = WarehouseInformationList.Single(k =>
                k.ProductName == Convert.ToString(SelectedItemsListBox.Items[i]));
              deviceType = db.Mediator.Single(k => k.ID == warehouse.Product_ID).Components_type;
            }
            SQLRequests.CreateHoldingDocument(warehouse, user, deviceType, decimal.Parse(PriceLabel.Text));
          }
          SQLRequests.UpdateWarehouseData();
          //Узнать нужно ли вывести информацию на печать
          SelectedItemsListBox.Items.Clear();
          AllProductInfo.Clear();
          PriceLabel.Text = "0";
          MessageBox.Show("Покупка оформлена успешно.");
        }
        else
          MessageBox.Show("Действие отменено");
      }
      else
        MessageBox.Show("Не выбраны комплектующие для продажи");
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
        List<Mediator> Computers = new List<Mediator>();
        using (ApplicationContext db = new ApplicationContext())
        {
          Computers = db.Mediator.Where(i => i.Components_type == "PC").ToList();
          if(Computers != null)
          {
            //проверка на соответствие
          }
          else
          {
            //добавление сборки
          }
        }
      }
      else MessageBox.Show("Не все поля заполнены!");
      // нужно проверить есть ли такая же конфигурация в базе
      //нужно сразу проверить заполнены ли все поля данными
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
    }

    private void ViewRamToSet()
    {
      List<RAM> ram = (from b in Rams
                       where b.RAM_frequency <= maxRamFrequency &&
                       b.RAM_type == ramType && b.Kit * b.Capacity < maxRamCapacity
                       select b).ToList();
      //...
    }

    private void CPU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(CPU_ComboBox.SelectedIndex != -1)
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
          return;
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
            //return;

            
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

       

        //продумать момент, что нужно брать отфильтрованные данные если cpu, cooling или ram заполнены
        //аналогично и с процессорами, оперативкой и системами охлаждения и тд.
      }
      else
      {
        //если материнка выбрана то ничего делать не нужно
      }
    }

    //Нужен для добавления компонентов в конфигураторе ПК
    private void AddConfigItemInListBox(ComboBox comboBox, ref string prevName)
    {
      if (comboBox.SelectedIndex != -1)
      {
        if (prevName != "")
        {
          int index = SelectedConfigIntemsListBox.Items.IndexOf(prevName);
          SelectedConfigIntemsListBox.Items[index] = comboBox.Text;
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
      //переносить в правое окно для оформления 
      //предусмотреть что не все выбранные элементы могут быть в магазине или на складе


      //очистка полей
      SystemFunctions.ClearComboBoxes(CPU_ComboBox, GPU_ComboBox, Motherboard_ComboBox, PSU_ComboBox,
          RAM_ComboBox, Case_ComboBox, CoolingSystem_ComboBox, StorageDevice_ComboBox);
      cpuNameInListBox = "";
      gpuNameInListBox = "";
      motherboardNameInListBox = "";
      ramNameInListBox = "";
      coolingSystemNameInListBox = "";
      psuNameInListBox = "";
      storageNameInListBox = "";
      caseNameInListBox = "";
      SelectedConfigIntemsListBox.Items.Clear();
      SelectedComponentInfoTextBox.Clear();
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
      }
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
    }

    private void PSU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(PSU_ComboBox, ref psuNameInListBox);
    }

    private void StorageDevice_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(StorageDevice_ComboBox, ref storageNameInListBox);
    }

    private void Case_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(Case_ComboBox, ref caseNameInListBox);
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
      //нужно обновлять список комплектующих если был добавлен или изменен определенный товар, 
      //или если произошел привоз на складе
    }
  }
}