using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
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
    public partial class AuthorizedForm : Form
    {
        //string[][] info;
        private int movePoroductCount;
        private List<Warehouse_info> WarehouseInformationList = new List<Warehouse_info>();
        private List<Case> Cases;
        private List<Cooling_system> CoolingSystems;
        private List<CPU> Cpus = new List<CPU>();
        private List<GPU> Gpus;
        private List<HDD> Hdds;
        private List<Motherboard> Motherboards;
        private List<PSU> Psus;
        private List<RAM> Rams;
        private List<SSD> Ssds;
        private List<Locations_in_warehouse> LocationInWarehouseList = new List<Locations_in_warehouse>();
        private List<Products_location> ProductLocationsList = new List<Products_location>();
        private Thread[] threads = new Thread[9];

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

        private void AuthorizedForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            movePoroductCount = 0;
            LoadAllInfoFromDB();
            threads[0] = new Thread(new ThreadStart(LoadAllInfoFromDB));
            threads[1] = new Thread(new ThreadStart(LoadInfoAboutCPUFromDB));
            //Сделать потоки
            LoadInfoAboutCPUFromDB();
            LoadLocationInWarehouseFromDB();
            LoadProductLocationFromDB();

        }

        private void AuthorizedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoadInfoAboutCases()
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
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
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
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
        private void LoadInfoAboutCPUFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

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
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
        private void LoadInfoAboutGPU()
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
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
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

        private void LoadInfoAboutMotherboard()
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
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
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
                SystemFunctions.SetNewDataBaseAdress(ex);
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
                SystemFunctions.SetNewDataBaseAdress(ex);
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
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (Products_location i in db.Products_location)
                    {
                        ProductLocationsList.Add(new Products_location(i.Product_ID, i.Location_ID, i.Items_count));
                    }
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
        private void LoadLocationInWarehouseFromDB()
        {
            try
            {
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
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void LoadAllInfoFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Warehouse_info.Count() > 0)
                {

                
                    foreach (Warehouse_info c in db.Warehouse_info)
                    {
                        if (c.Current_items_count > 0)
                            WarehouseInformationList.Add(new Warehouse_info(c.Product_ID, c.Current_items_count));
                    }
                }
            }
            ViewInfo();
        }

        public void ViewInfo()
        {
            foreach (Warehouse_info wi in WarehouseInformationList)
            {
                dataGridView1.Rows.Add(wi.Product_ID, wi.ProductName, wi.Current_items_count);
            }
        }


        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow currentRow = dataGridView1.Rows[selectedrowindex];

                Warehouse_info obj = WarehouseInformationList.Single(i => i.Product_ID == (int)currentRow.Cells[0].Value);


                switch (obj.ProductType)
                {
                    case "CPU":
                        CPU currentCPU = Cpus.Single(i => i.Product_ID == obj.Product_ID);
                        string integratedGPU = "не поддерживается";
                        int multi = 1;
                        if (currentCPU.Multithreading)
                            multi = 2;

                        if (currentCPU.Integrated_graphic)
                            integratedGPU = "поддерживается";
                        AllProductInfo.Text = $"Имя: {currentCPU.Name};\n" +
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
                            $"Техпроцесс: {currentCPU.Technical_process} нм\n" +
                            $"Количество на складе: \n";
                            //Вывод данных на складе
                           foreach(var i in LocationInWarehouseList)
                           {

                            var productLocation = (from b in ProductLocationsList
                                                   where b.Location_ID == i.ID && b.Product_ID == obj.Product_ID
                                                   select b).SingleOrDefault();
                            if(productLocation !=null)
                                AllProductInfo.Text += i.Location_label + ": " + productLocation.Items_count + "\n";
                           }
                        break;
                    case "Case":

                        break;
                    case "Cooling":

                        break;
                    case "GPU":

                        break;
                    case "HDD":

                        break;
                    case "Motherboard":

                        break;
                    case "PSU":

                        break;
                    case "RAM":

                        break;
                    case "SSD":

                        break;
                    default:
                        break;
                }
            }
            
        }

        private void plus_Click(object sender, EventArgs e)
        {
            movePoroductCount++;
            AddProduct.Text = Convert.ToString(movePoroductCount);
        }

        private void minus_Click(object sender, EventArgs e)
        {
            movePoroductCount--;
            AddProduct.Text = Convert.ToString(movePoroductCount);
        }

        private void SetIP_Click(object sender, EventArgs e)
        {
            SystemFunctions.SetNewDataBaseAdress();
        }

        private void EnterEditForm_Click(object sender, EventArgs e)
        {
            ComponentsOptionsForm addComponentsOptionsForm = new ComponentsOptionsForm(user, WarehouseInformationList, Cpus);
            this.Hide();
            addComponentsOptionsForm.Show();
        }

        //Нужен для того, чтобы после добавления данных обновить список в таблице
        private void AuthorizedForm_Enter(object sender, EventArgs e)
        {
            AuthorizedForm_Load(sender, e);
        }
    }
}
