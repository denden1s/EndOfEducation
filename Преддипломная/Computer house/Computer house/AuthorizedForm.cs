using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
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

    private string cpuNameInListBox = "";
    private string gpuNameInListBox = "";
    private string motherboardNameInListBox = "";
    private string ramNameInListBox = "";
    private string coolingSystemNameInListBox = "";
    private string psuNameInListBox = "";
    private string storageNameInListBox = "";
    private string caseNameInListBox = "";

    private Users user;

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

      ViewInfoInComboBox<CPU>(Cpus, CPU_ComboBox);
      ViewInfoInComboBox<GPU>(Gpus, GPU_ComboBox);
      ViewInfoInComboBox<Motherboard>(Motherboards, Motherboard_ComboBox);
      ViewInfoInComboBox<RAM>(Rams, RAM_ComboBox);
      ViewInfoInComboBox<Cooling_system>(CoolingSystems, CoolingSystem_ComboBox);
      ViewInfoInComboBox<PSU>(Psus, PSU_ComboBox);
      //накопители
      ViewInfoInComboBox<Case>(Cases, Case_ComboBox);
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

    //private void LoadInfoAboutHDD()
    //{
    //    try
    //    {
    //        using (ApplicationContext db = new ApplicationContext())
    //        {
    //            int counter = 0;
    //            foreach (GPU c in db.GPUs)
    //            {
    //                Gpus.Add(new GPU(c.ID));
    //                Gpus[counter].GetDataFromDB();
    //                counter++;
    //            }
    //            Hdds;
    //            Motherboards;
    //            Psus;
    //            Rams;
    //            Ssds;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SystemFunctions.SetNewDataBaseAdress(ex);
    //    }
    //}

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

    //private void LoadInfoAboutSSD()
    //{
    //    try
    //    {
    //        using (ApplicationContext db = new ApplicationContext())
    //        {
    //            int counter = 0;
    //            foreach (SSD c in db.SSDs)
    //            {
    //                Ssds.Add(new SSD(c.ID));
    //                Ssds[counter].GetDataFromDB();
    //                counter++;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SystemFunctions.SetNewDataBaseAdress(ex);
    //    }
    //}

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
          AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count, wi.Items_in_shop);
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
              AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName,
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

            tempRequestCPU.AddRange(GetSearchInfo("CPU"));
            tempRequestGPU.AddRange(GetSearchInfo("GPU"));
            tempRequestMotherboard.AddRange(GetSearchInfo("Motherboard"));
            tempRequestCase.AddRange(GetSearchInfo("Case"));
            tempRequestRAM.AddRange(GetSearchInfo("RAM"));
            tempRequestCoolingSys.AddRange(GetSearchInfo("Cooling system"));
            tempRequestPSU.AddRange(GetSearchInfo("PSU"));
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

            //Проверка наличия такого ID как в строке поиска
            if (tempRequest != null)
            {
              foreach (Mediator i in tempRequest)
                SearchResultList.Add(WarehouseInformationList.Single(a => a.Product_ID == i.ID));

              //Добавление и вывод при успешном поиске
              if (SearchResultList.Count != 0)
              {
                foreach (Warehouse_info wi in SearchResultList)
                  AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName,
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
            AddProduct.Value = (int)currentRow.Cells[2].Value;
            AddProduct.Maximum = (int)currentRow.Cells[2].Value;
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
        currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) + 1);
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
          //false - означает что запрос необработан на складе
          var items = (from b in db.ShopRequests
                        where b.Product_ID == warehouseInfo.Product_ID && b.Status == false
                        select b).ToList();
          int requestedItemsCountBefore = items.Sum(i => i.Count);
          //Нужно проверить есть ли запросы на товар, во избежание превышения кол-ва элементов в запросе
          //над кол-вом элементов на складе
          if (AddProduct.Value + requestedItemsCountBefore <= warehouseInfo.Current_items_count)
          {
            db.ShopRequests.Add(newRequest);
            List<NeedToUpdate> update = db.NeedToUpdate.ToList();
            update[0].UpdateStatus = true;
            db.NeedToUpdate.Update(update[0]);
            db.SaveChanges();
            MessageBox.Show("Товар успешно запрошен");
          }
          else
            MessageBox.Show("Невозможно запросить такое кол-во товара, возможно товар уже был запрошен.");
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
              $"Энергопотребление: {currentCPU.Consumption} нм\n" +
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
        case "HDD":
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
        case "SSD":
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

    private void CPU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(CPU_ComboBox, ref cpuNameInListBox);
    }

    private void GPU_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

      AddConfigItemInListBox(GPU_ComboBox, ref gpuNameInListBox);
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

    private void Motherboard_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(Motherboard_ComboBox, ref motherboardNameInListBox);
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
      AddConfigItemInListBox(RAM_ComboBox, ref ramNameInListBox);
    }

    private void CoolingSystem_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AddConfigItemInListBox(CoolingSystem_ComboBox, ref coolingSystemNameInListBox);
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
  }
}