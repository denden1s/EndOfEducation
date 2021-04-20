using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
    public class Case : Product//: IBase_and_max_options, ISizes_of_components
    {
        //public string ID { get; set; }
        //public string Name { get; set; }
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
        internal string FormFactor { get; set; }
        //Реализация интерфейса IBase_and_max_options
        internal int Product_ID { get; set; }
        internal int Coolers_count { get; set; }//Количество установленных кулеров
        internal int Coolers_slots { get; set; }//Всего слотов для установки кулеров

        //Реализация интерфейса ISizes_of_components
        internal int Height { get; set; } //Высота корпуса
        internal int Width { get; set; } //Ширина корпуса
        internal int Depth { get; set; } //Глубина корпуса

        public Case() { }

        public Case(string _id) : base(_id) { }
        //Выборка из БД

        public void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var caseInfo = db.Case.Single(i => i.ID == ID);
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
                    Max_PSU_length = caseInfo.Max_PSU_length;
                    Weight = caseInfo.Weight;
                    GetFormFactorInfo(db);
                    SetBaseAndMaxOptions(db);
                    SetSizesOptions(db);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GetFormFactorInfo(ApplicationContext db)
        {
            var listOfFormFactors = (from b in db.Form_factors
                                     where b.Device_type == "Case"
                                     select b).ToList();

            FormFactor = listOfFormFactors.Single(i => i.ID == Form_factor_ID).Name;
        }

        private void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Case_ID == ID).ID;
            var baseAndMaxOptions = db.Base_and_max_options.Single(i => i.Product_ID == Product_ID);
            Coolers_count = baseAndMaxOptions.Base_state;
            Coolers_slots = baseAndMaxOptions.Max_state;
        }

        private void SetSizesOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Case_ID == ID).ID;
            var sizes = db.Sizes_of_components.Single(i => i.Product_ID == Product_ID);
            Height = sizes.Height;
            Width = sizes.Width;
            Depth = sizes.Depth;
        }
    }
}