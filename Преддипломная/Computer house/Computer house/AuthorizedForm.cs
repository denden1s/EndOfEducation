using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private Users user;

        static object locker = new object();
        private Thread [] threads = new Thread[7];
        public AuthorizedForm()
        {
            InitializeComponent();
        }

        public AuthorizedForm(Users _user)
        {
            user = _user;
            InitializeComponent();
        }

        private void AuthorizedForm_Load(object sender, EventArgs e)
        {
            
            threads[0] = new Thread(new ThreadStart(LoadInfoAboutCPUFromDB));
            threads[1] = new Thread(new ThreadStart(LoadInfoAboutCasesFromDB));
            threads[2] = new Thread(new ThreadStart(LoadInfoAboutGPUFromDB));
            threads[3] = new Thread(new ThreadStart(LoadInfoAboutMotherboardsFromDB));
            threads[4] = new Thread(new ThreadStart(LoadLocationInWarehouseFromDB));
            threads[5] = new Thread(new ThreadStart(LoadProductLocationFromDB));
            threads[6] = new Thread(new ThreadStart(LoadInfoAboutMediatorFromDB));
            Task.Run(() => LoadInfoAboutRAM());
            Task.Run(() => LoadInfoAboutCooling());
            Task.Run(() => LoadInfoAboutPSU());
            foreach (var th in threads)
            {
                th.Start();
            }
            LoadInfoFromDBAndView();
            //AllInfoDatagridView.Rows.Clear();
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
                {
                    foreach (Mediator c in db.Mediator)
                    {
                        Mediators.Add(c);
                    }
                }
                threads[6].Interrupt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadInfoAboutCasesFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    int counter = 0;
                    foreach (Case c in db.Case)
                    {
                        List<int> IDs = SQLRequests.FindIntID("Case");

                        Cases.Add(new Case(c.ID));
                        Cases[counter].GetDataFromDB();
                        counter++;
                    }
                }
                threads[1].Interrupt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadInfoAboutCooling()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    int counter = 0;
                    foreach (Cooling_system c in db.Cooling_system)
                    {
                        CoolingSystems.Add(new Cooling_system(c.ID));
                        CoolingSystems[counter].GetDataFromDB();
                        counter++;
                    }
                }
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
                {
                    Cpus.Clear();
                    foreach (CPU c in db.CPU)
                    {
                        Cpus.Add(new CPU(c.ID));
                    }
                    for (int i = 0; i < Cpus.Count; i++)
                    {
                        Cpus[i].GetDataFromDB();
                    }
                }
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
                {
                    int counter = 0;
                    foreach (GPU c in db.GPU)
                    {
                        Gpus.Add(new GPU(c.ID));
                        Gpus[counter].GetDataFromDB();
                        counter++;
                    }
                }
                threads[2].Interrupt();
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
                {
                    int counter = 0;
                    foreach (Motherboard c in db.Motherboard)
                    {
                        Motherboards.Add(new Motherboard(c.ID));
                        Motherboards[counter].GetDataFromDB();
                        counter++;
                    }
                }
                threads[3].Interrupt();
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
                using (ApplicationContext db = new ApplicationContext())
                {
                    int counter = 0;
                    foreach (PSU c in db.PSU)
                    {
                        Psus.Add(new PSU(c.ID));
                        Psus[counter].GetDataFromDB();
                        counter++;
                    }
                }
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
                using (ApplicationContext db = new ApplicationContext())
                {
                    int counter = 0;
                    foreach (RAM c in db.RAM)
                    {
                        Rams.Add(new RAM(c.ID));
                        Rams[counter].GetDataFromDB();
                        counter++;
                    }
                }
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
                    {
                        ProductLocationsList.Add(new Products_location(i.Product_ID, i.Location_ID, i.Items_count));
                    }
                }
                threads[5].Interrupt();
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
                threads[4].Interrupt();
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
                lock(locker)
                {
                    WarehouseInformationList.Clear();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        if (db.Warehouse_info.Count() > 0)
                        {
                            foreach (Warehouse_info c in db.Warehouse_info)
                            {
                                WarehouseInformationList.Add(new Warehouse_info(c.Product_ID, c.Current_items_count, c.Items_in_shop));
                            }
                        }
                    }
                }
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
                {
                    AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count, wi.Items_in_shop);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

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
                    SearchResultList = (from b in WarehouseInformationList
                                        where b.ProductName.Contains(SearchInfo.Text)
                                        select b).ToList();
                    //Если результат есть то вывести данные
                    if (SearchResultList.Count != 0)
                    {
                        foreach (Warehouse_info wi in SearchResultList)
                        {
                            AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
                        }
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

                        tempRequestCPU.AddRange(GetSearchInfo("CPU"));
                        tempRequestGPU.AddRange(GetSearchInfo("GPU"));
                        tempRequestMotherboard.AddRange(GetSearchInfo("Motherboard"));
                        tempRequestCase.AddRange(GetSearchInfo("Case"));
                        tempRequestRAM.AddRange(GetSearchInfo("RAM"));
                        tempRequestCoolingSys.AddRange(GetSearchInfo("Cooling system"));
                        tempRequestPSU.AddRange(GetSearchInfo("PSU"));
                        tempRequest.AddRange((from b in tempRequestCPU
                                              where b.CPU_ID == SearchInfo.Text
                                              select b).ToList());
                        tempRequest.AddRange((from b in tempRequestGPU
                                              where b.GPU_ID.Contains(SearchInfo.Text)
                                              select b).ToList());
                        tempRequest.AddRange((from b in tempRequestMotherboard
                                              where b.Motherboard_ID.Contains(SearchInfo.Text)
                                              select b).ToList());
                        tempRequest.AddRange((from b in tempRequestCase
                                              where b.Case_ID.Contains(SearchInfo.Text)
                                              select b).ToList());
                        tempRequest.AddRange((from b in tempRequestRAM
                                              where b.RAM_ID.Contains(SearchInfo.Text)
                                              select b).ToList());
                        tempRequest.AddRange((from b in tempRequestCoolingSys
                                              where b.Cooling_system_ID.Contains(SearchInfo.Text)
                                              select b).ToList());
                        tempRequest.AddRange((from b in tempRequestPSU
                                              where b.PSU_ID.Contains(SearchInfo.Text)
                                              select b).ToList());
                        //Проверка наличия такого ID как в строке поиска
                        if (tempRequest != null)
                        {
                            foreach (Mediator i in tempRequest)
                            {
                                SearchResultList.Add(WarehouseInformationList.Single(a => a.Product_ID == i.ID));
                            }
                            //Добавление и вывод при успешном поиске
                            if (SearchResultList.Count != 0)
                            {
                                foreach (Warehouse_info wi in SearchResultList)
                                {
                                    AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
                                }
                            }
                            //В противном случае очистить таблицу
                            else
                            {
                                AllInfoDatagridView.Rows.Clear();
                                // ViewInfoInDataGrid();
                            }
                        }
                        else
                        {
                            AllInfoDatagridView.Rows.Clear();
                            //ViewInfoInDataGrid();
                        }
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

        private void AllInfoDatagridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //добавление данных в список выбранных элементов
            if (AllInfoDatagridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];

                Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
                                               i.Product_ID == (int)currentRow.Cells[0].Value);
                if(warehouseInfo.Items_in_shop != 0)
                {
                    SelectedItemsListBox.Items.Add(warehouseInfo.ProductName);
                    warehouseInfo.Items_in_shop--;
                    currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value)- 1);
                }
                else
                {
                    MessageBox.Show($"Нет возможности выбрать {warehouseInfo.ProductName}!");
                }
                AllInfoDatagridView.Refresh();
            }
        }

        private void AllInfoDatagridView_RowEnter(object sender, DataGridViewCellEventArgs e)
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


                switch (warehouseInfo.ProductType)
                {
                    case "CPU":
                        CPU currentCPU = Cpus.Single(i => i.Product_ID == warehouseInfo.Product_ID);
                        string integratedGPU = "не поддерживается";
                        int multi = 1;
                        if (currentCPU.Multithreading)
                            multi = 2;

                        if (currentCPU.Integrated_graphic)
                            integratedGPU = "поддерживается";
                        AllProductInfo.Text = $"ID товара: {currentCPU.ID};\n" +
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

                        if (currentGPU.Overclocking)
                            AllProductInfo.Text += "да; \n";
                        else
                            AllProductInfo.Text += "нет; \n";
                        AllProductInfo.Text += $"Энергопотребление: {Convert.ToString(currentGPU.Consumption)} Вт;\n" +
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
                        if (currentMotherboard.SLI_support) AllProductInfo.Text += "Да";
                        else AllProductInfo.Text += "Нет";
                        AllProductInfo.Text += " / ";
                        if (currentMotherboard.Integrated_graphic) AllProductInfo.Text += "Да";
                        else AllProductInfo.Text += "Нет";
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
                        foreach (var i in psuTuple)
                        {
                            if (i.state)
                                AllProductInfo.Text += i.elem + "; ";
                        }
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
                        foreach (var i in ramTuple)
                        {
                            if (i.state)
                                AllProductInfo.Text += i.elem + "; ";
                        }
                        AllProductInfo.Text += "\n";

                        break;
                    case "SSD":

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
                        foreach (var i in caseTuple)
                        {
                            if (i.state)
                                AllProductInfo.Text += i.elem + "; ";
                        }
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
            }
        }

        private void SelectedItemsListBox_DoubleClick(object sender, EventArgs e)
        {
            if(SelectedItemsListBox.SelectedIndex != -1)
            {
                Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
                                              i.ProductName == SelectedItemsListBox.Items[SelectedItemsListBox.SelectedIndex]);
         
                DataGridViewRow currentRow = AllInfoDatagridView.Rows.Cast<DataGridViewRow>().Where(i => 
                                        i.Cells[1].Value == warehouseInfo.ProductName).First(); 

                
                SelectedItemsListBox.Items.Remove(SelectedItemsListBox.SelectedItem);
                warehouseInfo.Items_in_shop++;
                currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) + 1);

            }
        }

        private void RequestComponents_Click(object sender, EventArgs e)
        {
            if((AddProduct.Value != 0)&&(AllInfoDatagridView.SelectedCells.Count > 0))
            {
                int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];

                Warehouse_info warehouseInfo = WarehouseInformationList.Single(i =>
                                               i.Product_ID == (int)currentRow.Cells[0].Value);
                
                ShopRequests newRequest = new ShopRequests(warehouseInfo.Product_ID, Convert.ToInt32(AddProduct.Value),user.ID);
                using (ApplicationContext db = new ApplicationContext())
                {
                    var items = db.ShopRequests.Where(i => i.Product_ID == warehouseInfo.Product_ID);
                    int requestedItemsCountBefore = items.Sum(i => i.Count);
                    //Нужно проверить есть ли запросы на товар, во избежание превышения кол-ва элементов в запросе
                    //над кол-вом элементов на складе
                    if (AddProduct.Value + requestedItemsCountBefore < warehouseInfo.Current_items_count)
                    {
                        db.ShopRequests.Add(newRequest);
                        db.SaveChanges();
                        MessageBox.Show("Товар успешно запрошен");
                        warehouseInfo.Items_in_shop--;
                        currentRow.Cells[3].Value = Convert.ToString(Convert.ToInt32(currentRow.Cells[3].Value) - 1);
                    }
                    else
                        MessageBox.Show("Невозможно запросить такое кол-во товара, возможно товар уже был запрошен.");
                }
            }
        }
    }
}