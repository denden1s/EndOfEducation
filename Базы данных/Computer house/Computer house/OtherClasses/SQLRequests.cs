using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house.OtherClasses
{
    class SQLRequests
    {
        public static string[] FindProductID(int _product_ID)
        {
            using (ApplicationContext db = new ApplicationContext())
                {

                    var findProductName = db.Mediator.Single(i => i.ID == _product_ID);
                    string productID = "";
                    switch (findProductName.Components_type)
                    {
                        case "CPU":
                            productID = findProductName.CPU_ID;
                            break;
                        case "Case":
                            productID = findProductName.Case_ID;
                            break;
                        case "Cooling":
                            productID = findProductName.Cooling_system_ID;
                            break;
                        case "GPU":
                            productID = findProductName.GPU_ID;
                            break;
                        case "HDD":
                            productID = findProductName.HDD_ID;
                            break;
                        case "Motherboard":
                            productID = findProductName.Motherboard_ID;
                            break;
                        case "PSU":
                            productID = findProductName.PSU_ID;
                            break;
                        case "RAM":
                            productID = findProductName.RAM_ID;
                            break;
                        case "SSD":
                            productID = findProductName.SSD_ID;
                            break;
                        default:
                            break;
                    }
                    string[] result = { productID, findProductName.Components_type };
                    return result;
                }
        }

        public static List<int> FindIntID(string _componentType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var ID_inInt = (from b in db.Mediator
                                where b.Components_type == _componentType
                                select b.ID).ToList();
                return ID_inInt;
            }
        }

        public static string FindCPU(string productID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.CPU.Single(i => i.ID == productID).ID;
            }
        }

        //Добавление данных 
        public static void AddCPUSeries(CPU_series _series, bool _option)
        {
            try
            {
                if (Warning(_option, _series.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.CPU_series.Add(_series);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
        public static void AddCPUCodeName(CPU_codename _codeName, bool _option)
        {
            try
            {
                if(Warning(_option, _codeName.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.CPU_codename.Add(_codeName);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddSocket(Sockets _socket, bool _options)
        {
            try
            {
                if(Warning(_options, _socket.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Sockets.Add(_socket);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddChipset(Chipset _chipset, bool _option)
        {
            try
            {
                if(Warning(_option, _chipset.Name))
                {
                    using(ApplicationContext db = new ApplicationContext())
                    {
                        db.Chipset.Add(_chipset);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddChanel(RAM_chanels _chanel, bool _option)
        {
            try
            {
                if (Warning(_option, _chanel.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.RAM_chanels.Add(_chanel);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AddFrequency(RAM_frequency _frequency, bool _option)
        {
            try
            { 
                if (Warning(_option, Convert.ToString(_frequency.Frequency)))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.RAM_frequency.Add(_frequency);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void AddFormFactor(Form_factors _formFactor, bool _option)
        {
            try
            {
                if (Warning(_option, _formFactor.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Form_factors.Add(_formFactor);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void AddMemoryType(Memory_types _type, bool _option)
        {
            try
            { 
                if (Warning(_option, _type.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Memory_types.Add(_type);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void AddConnectionInterface(Connection_interfaces _interface, bool _option)
        {
            try
            { 
                if (Warning(_option, _interface.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Connection_interfaces.Add(_interface);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void AddPowerConnector(Power_connectors _connector, bool _option)
        {
            try
            { 
                if (Warning(_option, _connector.Connectors))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Power_connectors.Add(_connector);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Изменение данных
        public static void ChangeSeries(CPU_series _series, bool _option)
        {
            try
            {
                if (Warning(_option, _series.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.CPU_series.Update(_series);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeCodeName(CPU_codename _codeName, bool _option)
        {
            try
            { 
                if (Warning(_option, _codeName.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.CPU_codename.Update(_codeName);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeSocketInfo(Sockets _socket, bool _option)
        {
            try
            { 
                if(Warning(_option, _socket.Name))
                {
                    using(ApplicationContext db = new ApplicationContext())
                    {
                        db.Sockets.Update(_socket);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeChipsetInfo(Chipset _chipset, bool _option)
        {
            try
            {
                if(Warning(_option, _chipset.Name))
                {
                    using(ApplicationContext db = new ApplicationContext())
                    {
                        db.Chipset.Update(_chipset);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeChanelInfo(RAM_chanels _chanel, bool _option)
        {
            try
            { 
                if (Warning(_option, _chanel.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.RAM_chanels.Update(_chanel);
                        MessageBox.Show("Изменение прошло успешно!");
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeFrequencyInfo(RAM_frequency _frequency, bool _option)
        {
            try
            { 
                if (Warning(_option, Convert.ToString(_frequency.Frequency)))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.RAM_frequency.Update(_frequency);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ChangeFormFactors(Form_factors _formFactor, bool _option)
        {
            try
            { 
                if (Warning(_option, _formFactor.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Form_factors.Update(_formFactor);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ChangeMemoryType(Memory_types _type, bool _option)
        {
            try
            { 
                if (Warning(_option, _type.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Memory_types.Update(_type);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ChangeConnectionInterface(Connection_interfaces _interface, bool _option)
        {
            try
            { 
                if (Warning(_option, _interface.Name))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Connection_interfaces.Update(_interface);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ChangePowerConnector(Power_connectors _connector, bool _option)
        {
            try
            {
                if (Warning(_option, _connector.Connectors))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Power_connectors.Update(_connector);
                        db.SaveChanges();
                        MessageBox.Show("Изменение прошло успешно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool Warning(bool _act, string _name)
        {
            string[] words;
            if (_act)
                words = new string[]{ "добавить", "Добавление"};
            else
                words = new string[2] { "изменить", "Изменение" };

            DialogResult questionResult = MessageBox.Show($"Вы действительно хотите {words[0]} " +
                                $"элемент {_name}",
                                       $"{words[1]} элемента!",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information,
                                        MessageBoxDefaultButton.Button2,
                                        MessageBoxOptions.DefaultDesktopOnly);
            if (questionResult == DialogResult.Yes)
            {
                
                return true;
            }
            else
            {
                MessageBox.Show(words[1] + " не прошло!");
                return false;   
            }
        }

        //Добавление в бд через объект класса !!!


        public static void EditCPUInMediator(CPU _cpu, string _method)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Mediator mediator = new Mediator();

                    mediator.Components_type = "CPU";
                    mediator.CPU_ID = _cpu.ID;
                    if(_method == "Add")
                    {
                        db.Mediator.Add(mediator);
                        db.SaveChanges();
                    }
                    int tempMediatorID = (from b in db.Mediator
                                          where b.Components_type == "CPU" && b.CPU_ID == _cpu.ID
                                          select b.ID).SingleOrDefault();
                    Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
                    if (_method == "Add")
                        db.Warehouse_info.Add(info);
                    else if(_method == "Edit")
                        db.Warehouse_info.Update(info);

                    Energy_consumption energy = new Energy_consumption(tempMediatorID, _cpu.Consumption);
                    if (_method == "Add")
                        db.Energy_consumption.Add(energy);
                    else if (_method == "Edit")
                        db.Energy_consumption.Update(energy);

                    Base_and_max_options baseAndMaxOptions = new Base_and_max_options(tempMediatorID, _cpu.Base_state, _cpu.Max_state); ;
                    if (_method == "Add")
                        db.Base_and_max_options.Add(baseAndMaxOptions);
                    else if (_method == "Edit")
                        db.Base_and_max_options.Update(baseAndMaxOptions);
                    db.SaveChanges();
                }
                //После добавления в медиатор вытянуть этот же объект 
                //и добавить по int-товому id в warehouseinfo с количеством 0
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public static void AddCPU(CPU _cpu)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.CPU.Add(_cpu);
                    db.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeCPU(CPU _cpu)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.CPU.Update(_cpu);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void AddGPU(GPU _gpu)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.GPU.Add(_gpu);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeGPU(GPU _gpu)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.GPU.Update(_gpu);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void EditGPUInMediator(GPU _gpu, string _method)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Mediator mediator = new Mediator();

                    mediator.Components_type = "GPU";
                    mediator.GPU_ID = _gpu.ID;
                    if (_method == "Add")
                    {
                        db.Mediator.Add(mediator);
                        db.SaveChanges();
                    }
                    int tempMediatorID = (from b in db.Mediator
                                          where b.Components_type == "GPU" && b.GPU_ID == _gpu.ID
                                          select b.ID).SingleOrDefault();
                    Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
                    if (_method == "Add")
                        db.Warehouse_info.Add(info);
                    else if (_method == "Edit")
                        db.Warehouse_info.Update(info);
                    //настроить возможные варианты если происходит добавление или изменение

                    Energy_consumption energy_Consumption = new Energy_consumption(info.Product_ID, _gpu.Consumption);
                    if(_method == "Add")
                        db.Energy_consumption.Add(energy_Consumption);
                    else if(_method == "Edit")
                        db.Energy_consumption.Update(energy_Consumption);

                    Memory_capacity capacity = new Memory_capacity(info.Product_ID, _gpu.Capacity);
                    if (_method == "Add")
                        db.Memory_capacity.Add(capacity);
                    else if (_method == "Edit")
                        db.Memory_capacity.Update(capacity);

                    Sizes_of_components sizes = new Sizes_of_components();
                    sizes.Product_ID = info.Product_ID;
                    sizes.Length = _gpu.Length;
                    sizes.Height = _gpu.Height;
                    if (_method == "Add")
                        db.Sizes_of_components.Add(sizes);
                    else if (_method == "Edit")
                        db.Sizes_of_components.Update(sizes);

                    db.SaveChanges();
                }
                //После добавления в медиатор вытянуть этот же объект 
                //и добавить по int-товому id в warehouseinfo с количеством 0
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void AddMotherboard(Motherboard _motherboard)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Motherboard.Add(_motherboard);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void ChangeMotherboard(Motherboard _motherboard)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Motherboard.Update(_motherboard);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void EditMotherboardMediator(Motherboard _motherboard, string _method)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Mediator mediator = new Mediator();

                    mediator.Components_type = "Motherboard";
                    mediator.Motherboard_ID = _motherboard.ID;
                    if (_method == "Add")
                    {
                        db.Mediator.Add(mediator);
                        db.SaveChanges();
                    }
                    int tempMediatorID = (from b in db.Mediator
                                          where b.Components_type == "Motherboard" && b.Motherboard_ID == _motherboard.ID
                                          select b.ID).SingleOrDefault();
                    Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
                    if (_method == "Add")
                        db.Warehouse_info.Add(info);
                    else if (_method == "Edit")
                        db.Warehouse_info.Update(info);
                    //настроить возможные варианты если происходит добавление или изменение

                    Sizes_of_components sizes = new Sizes_of_components();
                    sizes.Product_ID = info.Product_ID;
                    sizes.Width = _motherboard.Width;
                    sizes.Length = _motherboard.Length;
                    if (_method == "Add")
                        db.Sizes_of_components.Add(sizes);
                    else if (_method == "Edit")
                        db.Sizes_of_components.Update(sizes);


                    Base_and_max_options maxOptions = new Base_and_max_options();
                    maxOptions.Product_ID = info.Product_ID;
                    maxOptions.Base_state = 0;
                    maxOptions.Max_state = _motherboard.RAM_frequency;
                    if (_method == "Add")
                        db.Base_and_max_options.Add(maxOptions);
                    else if (_method == "Edit")
                        db.Base_and_max_options.Update(maxOptions);
                    Memory_capacity capacity = new Memory_capacity(info.Product_ID, _motherboard.Capacity);
                    if (_method == "Add")
                        db.Memory_capacity.Add(capacity);
                    else if (_method == "Edit")
                        db.Memory_capacity.Update(capacity);

                    db.SaveChanges();
                }
                //После добавления в медиатор вытянуть этот же объект 
                //и добавить по int-товому id в warehouseinfo с количеством 0
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        public static void AddCase(Case _case)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Case.Add(_case);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public static void ChangeCase(Case _case)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Case.Update(_case);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public static void EditCaseMediator(Case _case, string _method)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Mediator mediator = new Mediator();

                    mediator.Components_type = "Case";
                    mediator.Case_ID = _case.ID;
                    if (_method == "Add")
                    {
                        db.Mediator.Add(mediator);
                        db.SaveChanges();
                    }
                    int tempMediatorID = (from b in db.Mediator
                                          where b.Components_type == "Case" && b.Case_ID == _case.ID
                                          select b.ID).SingleOrDefault();
                    Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
                    if (_method == "Add")
                        db.Warehouse_info.Add(info);
                    else if (_method == "Edit")
                        db.Warehouse_info.Update(info);
                    //настроить возможные варианты если происходит добавление или изменение
                    Sizes_of_components sizesOfCase = new Sizes_of_components();
                    sizesOfCase.Product_ID = info.Product_ID;
                    sizesOfCase.Height = _case.Height;
                    sizesOfCase.Width = _case.Width;
                    sizesOfCase.Depth = _case.Depth;
                    if (_method == "Add")
                        db.Sizes_of_components.Add(sizesOfCase);
                    else if (_method == "Edit")
                        db.Sizes_of_components.Update(sizesOfCase);
                    Base_and_max_options options = new Base_and_max_options(info.Product_ID, 
                                                  _case.Coolers_count, _case.Coolers_slots);
                    if (_method == "Add")
                        db.Base_and_max_options.Add(options);
                    else if (_method == "Edit")
                        db.Base_and_max_options.Update(options);
                    db.SaveChanges();
                }
                //После добавления в медиатор вытянуть этот же объект 
                //и добавить по int-товому id в warehouseinfo с количеством 0
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void CreateHoldingDocument(Warehouse_info _infoAboutProduct, int _itemsCount, Users _user, string _item)
        {
            if (_itemsCount < 0)
            {
                //расход 
                using (ApplicationContext db = new ApplicationContext())
                {
                    //Выбор мест откуда можно взять компонент
                    //Сделать switch case
                    List<Products_location> locations = (from b in db.Products_location
                                                         where b.Product_ID == _infoAboutProduct.Product_ID
                                                         && b.Items_count + _itemsCount >= 0
                                                         select b).ToList();
                    if (locations.Count != 0)
                    {
                        locations[0].Items_count += _itemsCount;
                        _infoAboutProduct.Current_items_count += _itemsCount;
                        Locations_in_warehouse locationInWarehouse = new Locations_in_warehouse();
                        locationInWarehouse = db.Locations_in_warehouse.Single(i => i.ID == locations[0].Location_ID);
                        locationInWarehouse.Current_item_count += _itemsCount;
                        db.Warehouse_info.Update(_infoAboutProduct);
                        db.Locations_in_warehouse.Update(locationInWarehouse);
                        db.Products_location.Update(locations[0]);
                        //добавить холдинг документ
                        Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Расход",
                           _itemsCount, _user.ID, locations[0].Location_ID);
                        db.Holding_document.Add(holding_Document);
                        db.SaveChanges();
                        MessageBox.Show("Снятие со склада прошло успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Товар отсутствует на складе");
                    }

                }
            }
            else if (_itemsCount > 0)
            {
                int location_ID = -1;
                //приход
                using (ApplicationContext db = new ApplicationContext())
                {
                    //Выбор мест куда можно определить компонент
                    //Сделать switch case

                    List<Locations_in_warehouse> locations = (from b in db.Locations_in_warehouse
                                                              where b.Location_label.Contains(_item) &&
                                                              b.Max_item_count > b.Current_item_count + _itemsCount
                                                              select b).ToList();

                    if (locations.Count != 0)
                    {
                        int numerator = 0;
                        List<Products_location> LocationsWithThisItem = new List<Products_location>();
                        LocationsWithThisItem = db.Products_location.Where(i => i.Product_ID == _infoAboutProduct.Product_ID).ToList();
                        foreach (var i in locations)
                        {
                            foreach (var a in LocationsWithThisItem)
                            {
                                if ((i.ID == a.Location_ID) && (a.Product_ID == _infoAboutProduct.Product_ID))
                                    location_ID = a.Location_ID;
                            }
                        }
                        //Если такого нет то добавить в первый блок
                        if (location_ID == -1)
                        {
                            location_ID = locations[0].ID;
                            Products_location productsLocation1 = new Products_location(_infoAboutProduct.Product_ID, location_ID, _itemsCount);
                            db.Products_location.Add(productsLocation1);
                        }
                        else
                        {
                            Products_location productsLocation = (from b in db.Products_location
                                                                  where b.Location_ID == location_ID && b.Product_ID == _infoAboutProduct.Product_ID
                                                                  select b).Single();
                            productsLocation.Items_count += _itemsCount;
                            db.Products_location.Update(productsLocation);
                        }
                        Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Приход",
                            _itemsCount, _user.ID, location_ID);


                        Locations_in_warehouse locations_InWarehouse = db.Locations_in_warehouse.Single(i => i.ID == location_ID);
                        locations_InWarehouse.Current_item_count += _itemsCount;
                        db.Locations_in_warehouse.Update(locations_InWarehouse);

                        db.Holding_document.Add(holding_Document);
                        _infoAboutProduct.Current_items_count += _itemsCount;
                        db.Warehouse_info.Update(_infoAboutProduct);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно");
                    }
                    else
                        MessageBox.Show("На складе нет места");
                }


            }
        }

    }
}