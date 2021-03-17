using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities
{
    class Case
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

        public Case() { }

        public Case(string[] _strArgs, int[] _intArgs, bool[] _boolArgs, float _weight)
        {
            ID = _strArgs[0];
            Name = _strArgs[1];
            Power_supply_unit = _strArgs[2];
            Form_factor_ID = _intArgs[0];
            Gaming = _boolArgs[0];
            Material = _strArgs[3];
            Compatible_motherboard = _strArgs[4];
            PSU_position = _strArgs[5];
            Cooling_type = _strArgs[6];
            Cooler_in_set = _boolArgs[1];
            Water_cooling_support = _boolArgs[2];
            Sound_isolation = _boolArgs[3];
            Storage_sections_count = _intArgs[1];
            Expansion_slots_count = _intArgs[2];
            Dust_filter = _boolArgs[4];
            Max_GPU_length = _intArgs[3];
            Max_CPU_cooler_height = _intArgs[4];
            Max_PSU_length = _intArgs[5];
            Weight = _weight;
            GetFormFactorInfo();
        }
        //Выборка из БД
        private void GetFormFactorInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var listOfFormFactors = (from b in db.FormFactors
                                             where b.Device_type == "Case"
                                             select b).ToList();

                    var formFactor = listOfFormFactors.Single(i => i.ID == Form_factor_ID);
                    FormFactor = formFactor.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

    }
}
