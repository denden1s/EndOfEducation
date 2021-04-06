using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
    public class GPU: Product //: IMemory_capacity, IEnergy_consumption, ISizes_of_components 
    {
        //public string ID { get; set; }
        //public string Name { get; set; }
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
        internal string ConnectionInterface { get; set; }
        internal string GPU_type { get; set; }
        internal string PowerType { get; set; }

        //Реализация интерфейса IMemory_capacity
        internal int Product_ID { get; set; }
        internal int Capacity { get; set; }

        //Реализация интерфейса IEnergy_consumption
        internal int Consumption { get; set; }

        //Реализация интерфейса ISizes_of_components
        internal int Length { get; set; } //Длина видеокарты
        internal int Height { get; set; } //Высота видеокарты

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
                    var GPU_info = db.GPU.Single(i => i.ID == ID);
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
                    PowerType = db.Power_connectors.Single(i => i.ID == Power_type_ID).Connectors;
                    ConnectionInterface = db.Connection_interfaces.Single(i => i.ID == Interface_ID).Name;
                    GetMemTypeInfo(db);
                    SetMemoryCapacity(db);
                    SetEnergy_consumption(db);
                    SetSizesOptions(db);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GetMemTypeInfo(ApplicationContext db)
        {
            var listOfGpuTypes = (from b in db.Memory_types
                                  where b.Device_type == "GPU"
                                    select b).ToList();

            GPU_type = listOfGpuTypes.Single(i => i.ID == GPU_type_ID).Name;
        }

        private void SetMemoryCapacity(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.GPU_ID == ID).ID;
            Capacity = db.Memory_capacity.Single(i => i.Product_ID == Product_ID).Capacity;
        }

        private void SetEnergy_consumption(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.GPU_ID == ID).ID;
            Consumption = db.Energy_consumption.Single(i => i.Product_ID == Product_ID).Consumption;
        }

        private void SetSizesOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.GPU_ID == ID).ID;
            var sizes = db.Sizes_of_components.Single(i => i.Product_ID == Product_ID);
            Length = sizes.Length;
            Height = sizes.Height;
        }
    }
}