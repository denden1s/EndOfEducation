﻿using System;
using System.Linq;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;

namespace Computer_house.DataBase.Entities
{
    class HDD : Storage_devices_options
    {
        //public string ID { get; set; }
        public string Name { get; set; }
        public int Speed_of_spindle { get; set; }
        public float Noise_level { get; set; }       

        public HDD() { }
        public HDD(string _id)
        {
            ID = _id;
        }
        //Выборка данных из БД
        public void GetInfoFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var hddInfo = db.HDDs.Single(i => i.ID == ID);
                    Name = hddInfo.Name;
                    Speed_of_spindle = hddInfo.Speed_of_spindle;
                    Noise_level = hddInfo.Noise_level;
                    SetStorageOptions("HDD");
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }    
    }
}
