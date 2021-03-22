using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool Warning(bool _act, string _name)
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
                MessageBox.Show(words[1] + " прошло успешно!");
                return true;
            }
            else
            {
                MessageBox.Show(words[1] + " не прошло!");
                return false;   
            }
        }

        //Добавление в бд через объект класса !!!


        public static void ADDCPUInMediatorAndInWarehouseInfo(string _cpuID)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Mediator mediator = new Mediator();
                    mediator.Components_type = "CPU";
                    mediator.CPU_ID = _cpuID;
                    db.Mediator.Add(mediator);
                    db.SaveChanges();
                    int tempMediatorID = (from b in db.Mediator
                                          where b.Components_type == "CPU" && b.CPU_ID == _cpuID
                                          select b.ID).SingleOrDefault();
                    Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
                    db.Warehouse_info.Add(info);
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
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}