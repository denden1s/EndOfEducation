﻿using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities
{
    class Case : IBase_and_max_options
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Power_supply_unit { get; set; }
        public int Form_factor_ID { get; set; }
        public bool Gaming { get; set; }
        public string Material { get; set; }
        public string Compatible_motherboard { get; set; }
        public string PSU_position { get; set; }
        public string Cooling_type { get; set; }
        public bool Cooler_in_set { get; set; }
        public bool Water_cooling_support { get; set; }
        public bool Sound_isolation { get; set; }
        public int Storage_sections_count { get; set; }
        public int Expansion_slots_count { get; set; }
        public bool Dust_filter { get; set; }
        public int Max_GPU_length { get; set; }
        public int Max_CPU_cooler_height { get; set; }
        public int Max_PSU_length { get; set; }
        public float Weight { get; set; }

        //Доп сведения из БД
        public string FormFactor { get; set; }
        //Реализация интерфейса IBase_and_max_options
        public int Product_ID { get; set; }
        public int Base_state { get; set; }//Количество установленных кулеров
        public int Max_state { get; set; }//Всего слотов для установки кулеров

        public Case() { }

        public Case(string _id)
        {
            ID = _id;
        }
        //Выборка из БД

        public void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var caseInfo = db.Cases.Single(i => i.ID == ID);
                    Name = caseInfo.Name;
                    Power_supply_unit = caseInfo.Power_supply_unit;
                    Form_factor_ID = caseInfo.Form_factor_ID;
                    Gaming = caseInfo.Gaming;
                    Material = caseInfo.Material;
                    Compatible_motherboard = caseInfo.Compatible_motherboard;
                    PSU_position = caseInfo.PSU_position;
                    Cooling_type = caseInfo.Cooling_type;
                    Cooler_in_set = caseInfo.Cooler_in_set;
                    Water_cooling_support = caseInfo.Water_cooling_support;
                    Sound_isolation = caseInfo.Sound_isolation;
                    Storage_sections_count = caseInfo.Storage_sections_count;
                    Expansion_slots_count = caseInfo.Expansion_slots_count;
                    Dust_filter = caseInfo.Dust_filter;
                    Max_GPU_length = caseInfo.Max_GPU_length;
                    Max_CPU_cooler_height = caseInfo.Max_CPU_cooler_height;
                    Max_PSU_length = Max_PSU_length;
                    Weight = caseInfo.Weight;
                    GetFormFactorInfo(db);
                    SetBaseAndMaxOptions(db);
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
        private void GetFormFactorInfo(ApplicationContext db)
        {
            var listOfFormFactors = (from b in db.FormFactors
                                        where b.Device_type == "Case"
                                        select b).ToList();

            FormFactor = listOfFormFactors.Single(i => i.ID == Form_factor_ID).Name;
        }

        public void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Case_ID == ID).ID;
            var baseAndMaxOptions = db.BaseMaxOptions.Single(i => i.Product_ID == Product_ID);
            Base_state = baseAndMaxOptions.Base_state;
            Max_state = baseAndMaxOptions.Max_state;
        }
    }
}