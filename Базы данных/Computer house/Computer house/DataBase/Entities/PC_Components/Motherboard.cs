using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities.PC_Components
{
    class Motherboard : IMemory_capacity
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Supported_CPU { get; set; }
        public int Socket_ID { get; set; }
        public int Chipset_ID { get; set; }
        public int Form_factor_ID { get; set; }
        public int RAM_type_ID { get; set; }
        public int Count_of_memory_slots { get; set; }
        public int RAM_chanels_ID { get; set; }
        public string Expansion_slots { get; set; }
        public string Storage_interfaces { get; set; }
        public bool SLI_support { get; set; }
        public bool Integrated_graphic { get; set; }
        public string Connectors { get; set; }

        //Доп сведения из БД
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public string FormFactor { get; set; }
        public string RAM_type { get; set; }
        public string RAM_chanel { get; set; }
        
        //Реализация интерфейса IMemory_capacity
        public int Product_ID { get; set; }
        public int Capacity { get; set; } //Максимальный объём ОЗУ

        public Motherboard() { }

        public Motherboard(string _id)
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
                    var mbInfo = db.Motherboards.Single(i => i.ID == ID);
                    Name = mbInfo.Name;
                    Supported_CPU = mbInfo.Supported_CPU;
                    Socket_ID = mbInfo.Socket_ID;
                    Chipset_ID = mbInfo.Chipset_ID;
                    Form_factor_ID = mbInfo.Form_factor_ID;
                    RAM_type_ID = mbInfo.RAM_type_ID;
                    RAM_chanels_ID = mbInfo.RAM_chanels_ID;
                    Count_of_memory_slots = mbInfo.Count_of_memory_slots;
                    Expansion_slots = mbInfo.Expansion_slots;
                    Storage_interfaces = mbInfo.Storage_interfaces;
                    SLI_support = mbInfo.SLI_support;
                    Integrated_graphic = mbInfo.Integrated_graphic;
                    Connectors = mbInfo.Connectors;
                    Socket = db.Sockets.Single(i => i.ID == Socket_ID).Name;
                    Chipset = db.Chipsets.Single(i => i.ID == Chipset_ID).Name;
                    RAM_chanel = db.RAMChanels.Single(i => i.ID == RAM_chanels_ID).Name;
                    GetFormFactorInfo(db);
                    GetRAMTypeInfo(db);
                    SetMemoryCapacity(db);
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
                                        where b.Device_type == "Motherboard"
                                        select b).ToList();

            FormFactor = listOfFormFactors.Single(i => i.ID == Form_factor_ID).Name;
        }

        private void GetRAMTypeInfo(ApplicationContext db)
        {
            var listOfRamTypes = (from b in db.MemoryTypes
                                    where b.Device_type == "RAM"
                                    select b).ToList();

            RAM_type = listOfRamTypes.Single(i => i.ID == RAM_type_ID).Name;
        }

        public void SetMemoryCapacity(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Motherboard_ID == ID).ID;
            Capacity = db.MemoryCapacity.Single(i => i.Product_ID == Product_ID).Capacity;
        }
    }
}