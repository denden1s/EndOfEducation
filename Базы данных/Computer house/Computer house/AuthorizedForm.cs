﻿using Computer_house.DataBase;
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
        private List<PSU> Psus;
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
            foreach (var th in threads)
            {
                th.Start();
            }
            LoadInfoFromDBAndView();
            AllInfoDatagridView.Rows.Clear();
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
                                WarehouseInformationList.Add(new Warehouse_info(c.Product_ID, c.Current_items_count));
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
                    AllInfoDatagridView.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (AllInfoDatagridView.SelectedCells.Count > 0)
            {
                AddProduct.Value = 0;
                int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];

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
                            $"{(float)currentCPU.Base_state/1000}/{(float)currentCPU.Max_state/1000} Ghz;\n" +
                            $"Тип памяти / кол-во каналов: {currentCPU.RAM_type} / {currentCPU.RAM_chanel}\n" +
                            $"Макс частота ОЗУ: {currentCPU.RAM_frequency} Mhz;\n" +
                            $"Встроенная графика: {integratedGPU};\n" +
                             $"Энергопотребление: {currentCPU.Consumption} нм\n" +
                            $"Техпроцесс: {currentCPU.Technical_process} нм\n" +
                            $"Количество на складе: \n";
                        break;
                    case "Cooling system":

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
                            $"{Convert.ToString(currentGPU.Height)} мм;\n" +
                            $"Количество на складе: \n";
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
                            $"{Convert.ToString(currentMotherboard.Width)} ММ \n";
                        break;
                    case "PSU":

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
                            $"{Convert.ToString(currentCase.Width)} / {Convert.ToString(currentCase.Depth)} ММ;\n" +
                            $"Отсеки под накопители/слоты расширения: " +
                            $"{Convert.ToString(currentCase.Storage_sections_count)} / " +
                            $"{Convert.ToString(currentCase.Expansion_slots_count)} шт.;\n" +
                            $"Макс. длина GPU/высота кулера CPU/длина БП: {Convert.ToString(currentCase.Max_GPU_length)} / " +
                            $"{Convert.ToString(currentCase.Max_CPU_cooler_height)} / " +
                            $"{Convert.ToString(currentCase.Max_PSU_length)} ММ;\n" +
                            $"Вес: {Convert.ToString(currentCase.Weight)}.\n";
                        break;
                    default:
                        break;
                }
                bool countMoreThanZero = false;
                foreach (var i in LocationInWarehouseList)
                {
                    //countMoreThanZero = false;

                    var productLocation = (from b in ProductLocationsList
                                           where b.Location_ID == i.ID && b.Product_ID == warehouseInfo.Product_ID
                                           select b).SingleOrDefault();
                    if (productLocation != null)
                    {
                        AllProductInfo.Text += i.Location_label + ": " + productLocation.Items_count + "\n";
                        countMoreThanZero = true;
                    }
                }
                if (!countMoreThanZero)
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
            ComponentsOptionsForm addComponentsOptionsForm = new ComponentsOptionsForm(user, 
                WarehouseInformationList, Cpus, Gpus, Motherboards, Cases, Rams, CoolingSystems);
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
                        tempRequestCPU.AddRange(GetSearchInfo("CPU"));
                        tempRequestGPU.AddRange(GetSearchInfo("GPU"));
                        tempRequestMotherboard.AddRange(GetSearchInfo("Motherboard"));
                        tempRequestCase.AddRange(GetSearchInfo("Case"));
                        tempRequestRAM.AddRange(GetSearchInfo("RAM"));
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
                        //Проверка наличия такого ID как в строке поиска
                        if (tempRequest != null)
                            {                        
                                foreach (Mediator i in tempRequest)
                                {
                                    SearchResultList.Add( WarehouseInformationList.Single(a => a.Product_ID == i.ID));
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
        private List<Mediator> FindIDInMediator(string _componentType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                 var mediatorRequest = (from b in db.Mediator
                                         where b.Components_type == _componentType
                                        select b).ToList();
                return mediatorRequest;
            }
            
        }

        private void Move_Click(object sender, EventArgs e)
        {
            string question = "Сейчас будет ";
            if (Convert.ToInt32(AddProduct.Value) > 0)
                question += "добавлено на склад ";
            else
                question += "снято со склада ";
            question += Convert.ToInt32(AddProduct.Value) + " элементов товара.";
            DialogResult questionResult = MessageBox.Show(question,
                                        "Проведение товара",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information,
                                         MessageBoxDefaultButton.Button2,
                                         MessageBoxOptions.DefaultDesktopOnly);
            if (questionResult == DialogResult.Yes)
            {
                string deviceType = "";
                   int selectedrowindex = AllInfoDatagridView.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = AllInfoDatagridView.Rows[selectedrowindex];
                using (ApplicationContext db = new ApplicationContext())
                {
                    deviceType = db.Mediator.Single(i => i.ID == (int)currentRow.Cells[0].Value).Components_type;
                }    
                
                SQLRequests.CreateHoldingDocument(WarehouseInformationList[AllInfoDatagridView.SelectedCells[0].RowIndex],
                Convert.ToInt32(AddProduct.Value), user,deviceType);
                AllProductInfo.Clear();
                LoadInfoFromDBAndView();
                //LoadAllInfoFromDB();
                //ViewInfoInDataGrid();
                LoadLocationInWarehouseFromDB();
                LoadProductLocationFromDB();
            }
            else
                MessageBox.Show("Действие отменено");
            AddProduct.Value = 0;
        }
    }
}