using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
namespace Computer_house.DataBase.Entities
{
    class GPU : IMemory_capacity
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Interface_ID { get; set; }
        public string Manufacturer { get; set; }
        public bool Overclocking { get; set; }
        public int GPU_type_ID { get; set; }
        public int Bus_width { get; set; }
        public string DirectX { get; set; }
        public bool SLI_support { get; set; }
        public string External_interfaces { get; set; }
        public int Power_type_ID { get; set; }
        public int Coolers_count { get; set; }
        public int Cooling_system_thikness { get; set; }

        //Доп данные из БД
        public string ConnectionInterface { get; set; }
        public string GPU_type { get; set; }
        public string PowerType { get; set; }

        //Реализация интерфейса IMemory_capacity
        public int Product_ID { get; set; }
        public int Capacity { get; set; }

        public GPU() { }
        
        public GPU(string _id)
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
                    var GPU_info = db.GPUs.Single(i => i.ID == ID);
                    Name = GPU_info.Name;
                    Interface_ID = GPU_info.Interface_ID;
                    Manufacturer = GPU_info.Manufacturer;
                    Overclocking = GPU_info.Overclocking;
                    GPU_type_ID = GPU_info.GPU_type_ID;
                    Bus_width = GPU_info.Bus_width;
                    DirectX = GPU_info.DirectX;
                    SLI_support = GPU_info.SLI_support;
                    External_interfaces = GPU_info.External_interfaces;
                    Power_type_ID = GPU_info.Power_type_ID;
                    Coolers_count = GPU_info.Coolers_count;
                    Cooling_system_thikness = GPU_info.Cooling_system_thikness;
                    PowerType = db.PowerConnectors.Single(i => i.ID == Power_type_ID).Connectors;
                    ConnectionInterface = db.ConectInterfaces.Single(i => i.ID == Interface_ID).Name;
                    GetMemTypeInfo(db);
                    SetMemoryCapacity(db);
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
        private void GetMemTypeInfo(ApplicationContext db)
        {
            var listOfGpuTypes = (from b in db.MemoryTypes
                                    where b.Device_type == "GPU"
                                    select b).ToList();

            GPU_type = listOfGpuTypes.Single(i => i.ID == GPU_type_ID).Name;
        }

        public void SetMemoryCapacity(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.GPU_ID == ID).ID;
            Capacity = db.MemoryCapacity.Single(i => i.Product_ID == Product_ID).Capacity;
        }
    }
}