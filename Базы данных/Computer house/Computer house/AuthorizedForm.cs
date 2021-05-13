using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data;
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
    private List<Motherboard> Motherboards = new List<Motherboard>();
    private List<PSU> Psus = new List<PSU>();
    private List<RAM> Rams = new List<RAM>();
    private List<Storage_devices> StorageDevices = new List<Storage_devices>();
    private List<Locations_in_warehouse> LocationInWarehouseList = new List<Locations_in_warehouse>();
    private List<Products_location> ProductLocationsList = new List<Products_location>();
    private List<Mediator> Mediators = new List<Mediator>();
    private List<ShopRequests> ShopRequests = new List<ShopRequests>();

    private string searchComponent = "";
    private List<Warehouse_info> FilteredInfo = new List<Warehouse_info>();
    private Users user;
    private bool firstLoad = false;
    private bool checkUpdate = true;

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
    public AuthorizedForm(Users _user, bool isFirstLoad)
    {
      checkUpdate = true;
      firstLoad = isFirstLoad;
      user = _user;
      InitializeComponent();
    }

    private async void AuthorizedForm_Load(object sender, EventArgs e)
    {
      Width = Convert.ToInt32(DesktopScreen.Width / DesktopScreen.GetScalingFactor());
      Height = Convert.ToInt32(DesktopScreen.Height / DesktopScreen.GetScalingFactor());
      CPUViewRadio.Checked = true;
      CPUViewRadio.Checked = false;
      ResetFilters.Enabled = false;
      Task.Run(() => LoadInfoAboutCPUFromDB());
      Task.Run(() => LoadInfoAboutCasesFromDB());
      Task.Run(() => LoadInfoAboutGPUFromDB());
      Task.Run(() => LoadInfoAboutMotherboardsFromDB());
      Task.Run(() => LoadProductLocationFromDB());
      Task.Run(() => LoadInfoAboutMediatorFromDB());
      Task.Run(() => LoadLocationInWarehouseFromDB());
      Task.Run(() => LoadInfoAboutRAM());
      Task.Run(() => LoadInfoAboutCooling());
      Task.Run(() => LoadInfoAboutPSU());
      Task.Run(() => LoadInfoAboutSD());
      await Task.Run(() => LoadShopRequestsFromDB(firstLoad));
      LoadInfoFromDBAndView();
      AllInfoDatagridView.Rows.Clear();
      Task.Run(() => UpdateInfo());
    }
    private async void UpdateInfo()
    {
      while(checkUpdate)
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          NeedToUpdate needToUpdate = db.NeedToUpdate.Single(i => i.ID == 1);
          if(needToUpdate.UpdateStatus)
          {
            ShopRequests.Clear();
            foreach(ShopRequests s in db.ShopRequests)
              if(!s.Status)
                ShopRequests.Add(s);


            foreach(ShopRequests r in ShopRequests)
              r.GetDataFromDB();
            MessageBox.Show("Появился новый запрос из магазина!");
            needToUpdate.UpdateStatus = false;
            db.NeedToUpdate.Update(needToUpdate);
            db.SaveChanges();
          }
        }
      }
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
        using(ApplicationContext db = new ApplicationContext())
          Mediators = db.Mediator.ToList();
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
        using(ApplicationContext db = new ApplicationContext())
          Cases = db.Case.ToList();

        foreach(Case c in Cases)
          c.GetDataFromDB();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutCooling()
    {
      try
      {
        CoolingSystems.Clear();
        using (ApplicationContext db = new ApplicationContext())
          CoolingSystems = db.Cooling_system.ToList();

        foreach(Cooling_system i in CoolingSystems)
          i.GetDataFromDB();
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
          Cpus = db.CPU.ToList();
          
        foreach(CPU c in Cpus)
          c.GetDataFromDB();
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
          Gpus = db.GPU.ToList();

        foreach(GPU g in Gpus)
          g.GetDataFromDB();
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
          Motherboards = db.Motherboard.ToList();

        foreach(Motherboard m in Motherboards)
          m.GetDataFromDB();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutPSU()
    {
      try
      {
        Psus.Clear();
        using (ApplicationContext db = new ApplicationContext())
          Psus = db.PSU.ToList();

        foreach(PSU p in Psus)
          p.GetDataFromDB();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutRAM()
    {
      try
      {
        Rams.Clear();
        using (ApplicationContext db = new ApplicationContext())
          Rams = db.RAM.ToList();

        foreach(RAM r in Rams)
          r.GetDataFromDB();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadInfoAboutSD()
    {
      try
      {
        StorageDevices.Clear();
        using(ApplicationContext db = new ApplicationContext())
          StorageDevices = db.Storage_devices.ToList();

        foreach(Storage_devices s in StorageDevices)
          s.GetDataFromDB();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadProductLocationFromDB()
    {
      try
      {
        ProductLocationsList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          ProductLocationsList = db.Products_location.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadLocationInWarehouseFromDB()
    {
      try
      {
        LocationInWarehouseList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          LocationInWarehouseList = db.Locations_in_warehouse.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }      
    private async void LoadInfoFromDBAndView()
    {
      await Task.Run(() => LoadAllInfoFromDB());
      ViewInfoInDataGrid(WarehouseInformationList);
    }
    private void LoadAllInfoFromDB()
    {
      try
      {
        WarehouseInformationList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          if(db.Warehouse_info.Count() > 0)
            WarehouseInformationList = db.Warehouse_info.ToList();

        foreach(Warehouse_info w in WarehouseInformationList)
          w.SetName();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      } 
    }
    private void LoadShopRequestsFromDB(bool isFirst)
    {
      try
      {
        bool needUpdate = false;
        ShopRequests.Clear();
        using(ApplicationContext db = new ApplicationContext())
        {
          if(db.ShopRequests.Count() > 0)
          {
            foreach(ShopRequests r in db.ShopRequests)
              if(!r.Status)
                ShopRequests.Add(r);

            foreach(ShopRequests r in ShopRequests)
              r.GetDataFromDB();
          }
          needUpdate = db.NeedToUpdate.Single(i => i.ID == 1).UpdateStatus;
        }
          
        if(isFirst && !needUpdate)
          if(ShopRequests.Count > 0)
            MessageBox.Show($"На данный момент не обработано {ShopRequests.Count} запроса(ов) из магазина!");
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }


    public void ViewInfoInDataGrid(List<Warehouse_info> warehouseInfo)
    {
      AllInfoDatagridView.Rows.Clear();
      try
      {
        foreach(Warehouse_info wi in warehouseInfo)
          AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if(AllInfoDatagridView.SelectedCells.Count > 0)
      {
        AddProduct.Value = AddProduct.Minimum;
        int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];

        Warehouse_info warehouseInfo = WarehouseInformationList.Single(i => 
          i.Product_ID == (int)currentRow.Cells[0].Value);

        switch(warehouseInfo.ProductType)
        {
          case "CPU":
            CPU currentCPU = Cpus.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            string integratedGPU = currentCPU.Integrated_graphic ? "поддерживается" : "не поддерживается";
            int multi = currentCPU.Multithreading? 2 : 1;
            AllProductInfo.Text = $"ID товара: {currentCPU.ID};\n" +
              $"Наименование: {currentCPU.Name};\n" +
              $"Модельный ряд: {currentCPU.SeriesName};\n" +
              $"Тип поставки: {currentCPU.Delivery_type};\n" +
              $"Кодовое название кристалла: {currentCPU.CodeName};\n" +
              $"Сокет: {currentCPU.Socket};\n" +
              $"Кол-во ядер: {currentCPU.Сores_count}, потоков: {currentCPU.Сores_count * multi};\n" +
              $"Частоты (min/max): " +
              $"{(float)currentCPU.Base_state/1000}/{(float)currentCPU.Max_state/1000} Ghz;\n" +
              $"Тип памяти / кол-во каналов: {currentCPU.RAM_type} / {currentCPU.RAM_chanel}\n" +
              $"Макс частота ОЗУ: {currentCPU.RAM_frequency} Mhz;\n" +
              $"Встроенная графика: {integratedGPU};\n" +
                $"Энергопотребление: {currentCPU.Consumption} нм\n" +
              $"Техпроцесс: {currentCPU.Technical_process} нм\n";
            break;
          case "Cooling system":
            Cooling_system currentCoolSys = CoolingSystems.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentCoolSys.ID};\n" +
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
            GPU currentGPU = Gpus.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentGPU.ID};\n" +
              $"Наименование: {currentGPU.Name};\n" +
              $"Интерфейс подключения: {currentGPU.ConnectionInterface};\n" +
              $"Производитель: {currentGPU.Manufacturer};\n" +
              $"Объём памяти: {Convert.ToString(currentGPU.Capacity)} Гб;\n" +
              $"Тип видеопамяти: {currentGPU.GPU_type};\n" +
              $"Ширина шины: {Convert.ToString(currentGPU.Bus_width)} бит; \n" +
              $"Разогнанная версия: ";

            AllProductInfo.Text += currentGPU.Overclocking ? "да; \n" : "нет; \n";
            AllProductInfo.Text += $"Энергопотребление: {Convert.ToString(currentGPU.Consumption)} Вт;\n" +
              $"Версия DirectX: {currentGPU.DirectX};\n" +
              $"Внешние интерфейсы: {currentGPU.External_interfaces};\n" +
              $"Тип питания: {currentGPU.PowerType};\n" +
              $"Кол-во вентиляторов: {Convert.ToString(currentGPU.Coolers_count)};\n" +
              $"Толщина системы охлаждения: {Convert.ToString(currentGPU.Cooling_system_thikness)} слотов;\n" +
              $"Длина / высота видеокарты: {Convert.ToString(currentGPU.Length)} / " +
              $"{Convert.ToString(currentGPU.Height)} мм;\n";
            break;
          case "SD":
            Storage_devices currentStorage = StorageDevices.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentStorage.ID};\n" +
              $"Наименование: {currentStorage.Name};\n" +
              $"Объём / буфер: {currentStorage.Capacity} Гб / {currentStorage.Buffer} Мб;\n" +
              $"Интерфейс подключения: {currentStorage.ConnectionInterface};\n" +
              $"Форм-фактор: {currentStorage.FormFactor};\n" +
              $"Скорость послед. чтения / записи: {currentStorage.Sequential_read_speed} / " +
              $"{currentStorage.Sequeintial_write_speed} Мб/c;\n" +
              $"Скорость случ. чтения / записи: {currentStorage.Random_read_speed} / {currentStorage.Random_write_speed} Мб/c;\n" +
              $"Энергопотребление / толщина: {currentStorage.Consumption} Вт / {Math.Round(currentStorage.Thickness, 2)} мм\n" +
              $"Аппаратное шифрование: ";
            AllProductInfo.Text += currentStorage.Hardware_encryption ? "Да" : "Нет";
            AllProductInfo.Text += "\n";
            break;
          case "Motherboard":
            Motherboard currentMotherboard = Motherboards.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentMotherboard.ID};\n" +
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
            AllProductInfo.Text += currentMotherboard.SLI_support ? "Да" : "Нет";
            AllProductInfo.Text += " / ";
            AllProductInfo.Text += currentMotherboard.Integrated_graphic ? "Да" : "Нет";
            AllProductInfo.Text += "\n";
            AllProductInfo.Text += $"Разъёмы: {currentMotherboard.Connectors}\n" +
              $"Длина / ширина платы: {Convert.ToString(currentMotherboard.Length)} / " +
              $"{Convert.ToString(currentMotherboard.Width)} мм\n";
            break;
          case "PSU":
            PSU currentPSU = Psus.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentPSU.ID};\n" +
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
            foreach(var i in psuTuple)
              if(i.state)
                AllProductInfo.Text += i.elem + "; ";

            AllProductInfo.Text += "\n";
            break;
          case "RAM":
            RAM currentRAM = Rams.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentRAM.ID};\n" +
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
            foreach(var i in ramTuple)
              if(i.state)
                AllProductInfo.Text += i.elem + "; ";

            AllProductInfo.Text += "\n";
            break; 
          case "Case":
            Case currentCase = Cases.Single(i => i.Product_ID == warehouseInfo.Product_ID);
            AllProductInfo.Text = $"ID товара: {currentCase.ID};\n" +
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
            foreach(var i in caseTuple)
              if(i.state)
                AllProductInfo.Text += i.elem + "; ";

            AllProductInfo.Text += "\n" +
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
        bool countMoreThanZero = false;
        AllProductInfo.Text += "Количество на складе: \n";
        foreach(var i in LocationInWarehouseList)
        {
          //countMoreThanZero = false;
          var productLocation = (from b in ProductLocationsList
                                 where b.Location_ID == i.ID && b.Product_ID == warehouseInfo.Product_ID
                                 select b).SingleOrDefault();
          if(productLocation != null)
          {
            AllProductInfo.Text += i.Location_label + ": " + productLocation.Items_count + "\n";
            countMoreThanZero = true;
          }
        }
        if(!countMoreThanZero)
          AllProductInfo.Text += "0";
      }    
    }

    //Нужен для того, чтобы после добавления данных обновить список в таблице
    private void AuthorizedForm_Enter(object sender, EventArgs e)
    {
      AuthorizedForm_Load(sender, e);
    }

    private void настроитьIPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SystemFunctions.SetNewDataBaseAdress();
    }

    private void перейтиВРазделРедактированияToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ComponentsOptionsForm addComponentsOptionsForm = new ComponentsOptionsForm(user, Cpus, Gpus, 
        Motherboards, Cases, Rams, CoolingSystems, Psus, StorageDevices, ShopRequests, WarehouseInformationList);
      checkUpdate = false;
      this.Hide();
      addComponentsOptionsForm.Show();
    }

    private void выйтиИзУчётнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AuthentificationForm authentificationForm = new AuthentificationForm();
      authentificationForm.Show();
      this.Hide();
    }

    private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //Открыть PDF
      string fbPath = Application.StartupPath;
      string fname = "Справка.pdf";
      string filename = fbPath + @"\" + fname;
      Help.ShowHelp(this, filename, HelpNavigator.Find, "");
    }

    //Нужен для поиска данных в таблице WarehouseInfo
    private void SearchInfo_TextChanged(object sender, EventArgs e)
    {
      try
      {
        AllInfoDatagridView.Rows.Clear();
        AllProductInfo.Clear();
        if(SystemFunctions.IsSetFilterRadio(CPUViewRadio, GPUViewRadio, MothersViewRadio, CasesViewRadio, RAMViewRadio,
          CoolingSystemViewRadio, PSUViewRadio, SDViewRadio))
        {
          if(SearchInfo.TextLength > 0)
            SearchInfoWithFilter();//поиск по фильтрам
          else
            ViewInfoInDataGrid(FilteredInfo);
        }
        else
        {
          if(SearchInfo.TextLength > 0)
            SearchInfoInWarehouse();//поиск без фильтров
          else
            ViewInfoInDataGrid(WarehouseInformationList);
        } 
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      } 
    }

    //Нужен для поиска информации с учётом фильтрации
    private void SearchInfoWithFilter()
    {
      //проверка на выбор радиокнопки
      List<Warehouse_info> SearchResultList = SearchByName(FilteredInfo);
      //Если результат есть то вывести данные
      if(SearchResultList.Count != 0)
      {
        foreach(Warehouse_info wi in SearchResultList)
          AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
      }
      //Если результат пустой то делает поиск по ID
      else
      {
        //Вытягивает числовой ID из посредника

        //MediatorRequest = FindIDInMediator("CPU");
        //Проверка на наличие товара в целом
        List<Mediator> tempRequest = new List<Mediator>();
        tempRequest.AddRange(GetSearchInfo(searchComponent));

        List<Mediator> finalResult = new List<Mediator>();
        switch(searchComponent)
        {
          case "CPU":
            finalResult.AddRange((from b in tempRequest
                                  where b.CPU_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "GPU":
            finalResult.AddRange((from b in tempRequest
                                  where b.GPU_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "Motherboard":
            finalResult.AddRange((from b in tempRequest
                                  where b.Motherboard_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "Case":
            finalResult.AddRange((from b in tempRequest
                                  where b.Case_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "RAM":
            finalResult.AddRange((from b in tempRequest
                                  where b.RAM_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "Cooling system":
            finalResult.AddRange((from b in tempRequest
                                  where b.Cooling_system_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "PSU":
            finalResult.AddRange((from b in tempRequest
                                  where b.PSU_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          case "SD":
            finalResult.AddRange((from b in tempRequest
                                  where b.SD_ID.ToLower().Contains(SearchInfo.Text.ToLower())
                                  select b).ToList());
            break;
          default:
            break;
        }
        //Проверка наличия такого ID как в строке поиска
        ViewInfoAfterSearch(finalResult, SearchResultList);
      }
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

    //Нужен для поиска информации без учёта фильтров
    private void SearchInfoInWarehouse()
    {
      //проверка на выбор радиокнопки
      List<Warehouse_info> SearchResultList = SearchByName(WarehouseInformationList);
      //Если результат есть то вывести данные
      if(SearchResultList.Count != 0)
      {
        foreach(Warehouse_info wi in SearchResultList)
          AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
      }
      //Если результат пустой то делает поиск по ID
      else
      {
        //Вытягивает числовой ID из посредника

        //MediatorRequest = FindIDInMediator("CPU");
        //Проверка на наличие товара в целом
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
        ViewInfoAfterSearch(tempRequest, SearchResultList);
      }
    }

    //Нужен для отображения информации после поиска
    private void ViewInfoAfterSearch(List<Mediator> RequestList, List<Warehouse_info> SearchResultList)
    {
      if(RequestList != null)
      {
        foreach(Mediator i in RequestList)
          SearchResultList.Add(WarehouseInformationList.Single(a => a.Product_ID == i.ID));
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

    //Аналог  FilterInfo только по таблице Nediator
    private List<Mediator> GetSearchInfo(string _deviceType)
    {
      return Mediators.Where(i => i.Components_type == _deviceType).ToList();
    }

    private void Move_Click(object sender, EventArgs e)
      {
        string question = "Сейчас будет добавлено на склад " + Convert.ToInt32(AddProduct.Value) + " шт. товара.";
        DialogResult questionResult = MessageBox.Show(question,
                                      "Проведение товара",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Information,
                                      MessageBoxDefaultButton.Button2,
                                      MessageBoxOptions.DefaultDesktopOnly);
      if(questionResult == DialogResult.Yes)
      {
        string deviceType = "";
            int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
        DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];
        using(ApplicationContext db = new ApplicationContext())
        {
          deviceType = db.Mediator.Single(i => i.ID == (int)currentRow.Cells[0].Value).Components_type;
        }
        Warehouse_info adedItemInfo = new Warehouse_info();
        if(searchComponent != "")
          adedItemInfo = FilteredInfo[AllInfoDatagridView.SelectedCells[0].RowIndex];
        else
          adedItemInfo = WarehouseInformationList[AllInfoDatagridView.SelectedCells[0].RowIndex];
        SQLRequests.CreateHoldingDocument(adedItemInfo,Convert.ToInt32(AddProduct.Value), user,deviceType);
        AllProductInfo.Clear();
        LoadAllInfoFromDB();

        //LoadInfoFromDBAndView();
        //LoadAllInfoFromDB();
        //ViewInfoInDataGrid();
        if(searchComponent != "")
          ViewInfoAfterChangeRadio();
        else
          ViewInfoInDataGrid(WarehouseInformationList);

        LoadLocationInWarehouseFromDB();
        LoadProductLocationFromDB();
      }
      else
        MessageBox.Show("Действие отменено");

      AddProduct.Value = AddProduct.Minimum;
    }

    private void CPUViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(CPUViewRadio.Checked)
      {
        searchComponent = "CPU";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    private void GPUViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(GPUViewRadio.Checked)
      {
        searchComponent = "GPU";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    private void MothersViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(MothersViewRadio.Checked)
      {
        searchComponent = "Motherboard";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    private void CasesViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(CasesViewRadio.Checked)
      {
        searchComponent = "Case";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    private void RAMViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(RAMViewRadio.Checked)
      {
        searchComponent = "RAM";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    private void CoolingSystemViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(CoolingSystemViewRadio.Checked)
      {
        searchComponent = "Cooling system";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    //Нужен для того чтобы выгрузить данные на основе фильтра
    private List<Warehouse_info> FilterInfo()
    {
      List<Warehouse_info> info = WarehouseInformationList.Where(i => i.ProductType == searchComponent).ToList();
      return info;
    }

    private void PSUViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(PSUViewRadio.Checked)
      {
        searchComponent = "PSU";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }

    //Нужен для отображения информации после выбора фильтра
    private void ViewInfoAfterChangeRadio()
    {
      if(SearchInfo.TextLength == 0)
        ViewInfoInDataGrid(FilteredInfo);
      else
        SearchInfo.Clear();
      AllProductInfo.Clear();
      ResetFilters.Enabled = true;
    }

    //Нужен для сброса фильтров поиска
    private void ResetFilters_Click(object sender, EventArgs e)
    {
      SystemFunctions.UnsetRadio(CPUViewRadio, GPUViewRadio, MothersViewRadio,
       CasesViewRadio, RAMViewRadio, CoolingSystemViewRadio, PSUViewRadio, SDViewRadio);
      ViewInfoInDataGrid(WarehouseInformationList);
      AllProductInfo.Clear();
      ResetFilters.Enabled = false;
    }

    private void SDViewRadio_CheckedChanged(object sender, EventArgs e)
    {
      if(SDViewRadio.Checked)
      {
        searchComponent = "SD";
        FilteredInfo = FilterInfo();
      }
      ViewInfoAfterChangeRadio();
    }
  }
}