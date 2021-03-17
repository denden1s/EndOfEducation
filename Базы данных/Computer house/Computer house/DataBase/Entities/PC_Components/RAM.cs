using Computer_house.OtherClasses;
using System;
using System.Linq;
namespace Computer_house.DataBase.Entities.PC_Components
{
    class RAM
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

        public RAM() { }

        public RAM(string[] _strArgs, int[] _intArgs, bool[] _boolArgs, float _voltage)
        {
            ID = _strArgs[0];
            Name = _strArgs[1];
            Kit = _intArgs[0];
            RAM_type_ID = _intArgs[1];
            RAM_frequency_ID = _intArgs[2];
            Voltage = _voltage;
            XMP_profile = _boolArgs[0];
            Cooling = _boolArgs[1];
            Low_profile_module = _boolArgs[2];
            GetRAMFrequencyInfo();
            GetRAMTypeInfo();
        }
        //Выборка данных из БД
        private void GetRAMFrequencyInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var frequency = db.RAMFrequencies.Single(i => i.ID == RAM_frequency_ID);
                    RAM_frequency = frequency.Frequency;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetRAMTypeInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var listOfRamTypes = (from b in db.MemoryTypes
                                          where b.Device_type == "RAM"
                                          select b).ToList();

                    var ramType = listOfRamTypes.Single(i => i.ID == RAM_type_ID);
                    RAM_type = ramType.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
    }
}