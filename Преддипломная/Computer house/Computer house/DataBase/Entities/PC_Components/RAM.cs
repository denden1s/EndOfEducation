using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities.PC_Components
{
    public class RAM : Product//: IBase_and_max_options, IMemory_capacity
    {
        //public string ID { get; set; }
        //public string Name { get; set; }
        public int Kit { get; set; }
        public int RAM_type_ID { get; set; }
        public int RAM_frequency_ID { get; set; }
        public float Voltage { get; set; }
        public bool XMP_profile { get; set; }
        public bool Cooling { get; set; }
        public bool Low_profile_module { get; set; }
        public string Timings { get; set; }

        //Доп данные из БД
        internal string RAM_type { get; set; }
        internal int RAM_frequency { get; set; }
        internal int Product_ID { get; set; }

        //Реализация интерфейса IMemory_capacity
        internal int Capacity { get; set; }

        public RAM() { }

        public RAM(string _id) : base(_id) { }
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
                    Timings = RAM_info.Timings;
                    RAM_frequency = db.RAM_frequency.Single(i => i.ID == RAM_frequency_ID).Frequency;
                    GetRAMTypeInfo(db);
                    SetMemoryCapacity(db);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetRAMTypeInfo(ApplicationContext db)
        {
            var listOfRamTypes = (from b in db.Memory_types
                                  where b.Device_type == "RAM"
                                    select b).ToList();

            RAM_type = listOfRamTypes.Single(i => i.ID == RAM_type_ID).Name;
        }

        private void SetMemoryCapacity(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.RAM_ID == ID).ID;
            Capacity = db.Memory_capacity.Single(i => i.Product_ID == Product_ID).Capacity;
        }
    }
}