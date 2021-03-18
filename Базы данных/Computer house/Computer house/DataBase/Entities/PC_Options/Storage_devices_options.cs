using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    class Storage_devices_options : IStorage_devices_options, IMemory_capacity
    {
        public string ID { get; set; }
        public int Storage_device_ID { get; set; }
        public int Interface_ID { get; set; }
        public int Form_factor_ID { get; set; }
        public int Buffer { get; set; }
        public bool Hardware_encryption { get; set; }
        public int Sequential_read_speed { get; set; }
        public int Sequeintial_write_speed { get ; set; }
        public int Random_read_speed { get; set; }
        public int Random_write_speed { get; set; }
        
        //Доп данные из БД
        public string FormFactor { get; set; }
        public string ConnectionInterface { get; set; }

        //Реализация интерфейса IMemory_capacity
        public int Product_ID { get; set; } //Аналогично свойству Storage_device_ID
        public int Capacity { get; set; }

        public void SetMemoryCapacity(ApplicationContext db)
        {
            Product_ID = Storage_device_ID;
            Capacity = db.MemoryCapacity.Single(i => i.Product_ID == Product_ID).Capacity;
        }

        public void SetStorageOptions(string deviceType)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (this is SSD)//если не сработает то if deviceType = "HDD" ...
                        Storage_device_ID = db.Mediator.Single(i => i.SSD_ID == ID).ID;
                    else
                        Storage_device_ID = db.Mediator.Single(i => i.HDD_ID == ID).ID;

                    var storageOptions = db.StorageDeviceOptions.Single(i => i.Storage_device_ID == Storage_device_ID);
                    Interface_ID = storageOptions.Interface_ID;
                    Form_factor_ID = storageOptions.Form_factor_ID;
                    Buffer = storageOptions.Buffer;
                    Hardware_encryption = storageOptions.Hardware_encryption;
                    Sequential_read_speed = storageOptions.Sequential_read_speed;
                    Sequeintial_write_speed = storageOptions.Sequeintial_write_speed;
                    Random_read_speed = storageOptions.Random_read_speed;
                    Random_write_speed = storageOptions.Random_write_speed;
                    var listOfForms = (from b in db.FormFactors
                                       where b.Device_type == deviceType
                                       select b).ToList();
                    FormFactor = listOfForms.Single(i => i.ID == Form_factor_ID).Name;
                    ConnectionInterface = db.ConectInterfaces.Single(i => i.ID == Interface_ID).Name;
                    SetMemoryCapacity(db);
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }


        }
    }
}