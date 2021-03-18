﻿using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
namespace Computer_house.DataBase.Entities.PC_Components
{
    class RAM : IBase_and_max_options, IMemory_capacity
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Kit { get; set; }
        public int RAM_type_ID { get; set; }
        public int RAM_frequency_ID { get; set; }
        public float Voltage { get; set; }
        public bool XMP_profile { get; set; }
        public bool Cooling { get; set; }
        public bool Low_profile_module { get; set; }

        //Доп данные из БД
        public string RAM_type { get; set; }
        public int RAM_frequency { get; set; }
        //Реализация интерфейса IBase_and_max_options
        public int Product_ID { get; set; }
        public int Base_state { get; set; } //Базовая частота
        public int Max_state { get; set; } //Не используется

        //Реализация интерфейса IMemory_capacity
        public int Capacity { get; set; }

        public RAM() { }

        public RAM(string _id)
        {
            ID = _id;
        }
        //Выборка данных из БД

        public void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var RAM_info = db.RAM.Single(i => i.ID == ID);
                    Name = RAM_info.Name;
                    Kit = RAM_info.Kit;
                    RAM_type_ID = RAM_info.RAM_type_ID;
                    RAM_frequency_ID = RAM_info.RAM_frequency_ID;
                    Voltage = RAM_info.Voltage;
                    XMP_profile = RAM_info.XMP_profile;
                    Cooling = RAM_info.Cooling;
                    Low_profile_module = RAM_info.Low_profile_module;
                    RAM_frequency = db.RAMFrequencies.Single(i => i.ID == RAM_frequency_ID).Frequency;
                    GetRAMTypeInfo(db);
                    SetBaseAndMaxOptions(db);
                    SetMemoryCapacity(db);
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetRAMTypeInfo(ApplicationContext db)
        {
            var listOfRamTypes = (from b in db.MemoryTypes
                                    where b.Device_type == "RAM"
                                    select b).ToList();

            RAM_type = listOfRamTypes.Single(i => i.ID == RAM_type_ID).Name;
        }

        public void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.RAM_ID == ID).ID;
            var baseAndMaxOptions = db.BaseMaxOptions.Single(i => i.Product_ID == Product_ID);
            Base_state = baseAndMaxOptions.Base_state;
        }

        public void SetMemoryCapacity(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.RAM_ID == ID).ID;
            Capacity = db.MemoryCapacity.Single(i => i.Product_ID == Product_ID).Capacity;
        }
    }
}