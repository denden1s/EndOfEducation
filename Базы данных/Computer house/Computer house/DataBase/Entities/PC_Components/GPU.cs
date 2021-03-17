using Computer_house.OtherClasses;
using System;
using System.Linq;
namespace Computer_house.DataBase.Entities
{
    class GPU
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

        public GPU() { }
        
        public GPU(string[] _strArgs, int[] _intArgs, bool _overClock, bool _sli)
        {
            ID = _strArgs[0];
            Name = _strArgs[1];
            Interface_ID = _intArgs[0];
            Manufacturer = _strArgs[2];
            Overclocking = _overClock;
            GPU_type_ID = _intArgs[1];
            Bus_width = _intArgs[2];
            DirectX = _strArgs[3];
            SLI_support = _sli;
            External_interfaces = _strArgs[4];
            Power_type_ID = _intArgs[3];
            Coolers_count = _intArgs[4];
            Cooling_system_thikness = _intArgs[5];
            GetMemTypeInfo();
            GetPowerTypeInfo();
            GetConnectionInterfaceInfo();
        }
        //Выборка данных из БД
        private void GetMemTypeInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var listOfGpuTypes = (from b in db.MemoryTypes
                                          where b.Device_type == "GPU"
                                          select b).ToList();

                    var gpuType = listOfGpuTypes.Single(i => i.ID == GPU_type_ID);
                    GPU_type = gpuType.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetPowerTypeInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var powerType = db.PowerConnectors.Single(i => i.ID == Power_type_ID);
                    PowerType = powerType.Connectors;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetConnectionInterfaceInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var connectInterface = db.ConectInterfaces.Single(i => i.ID == Interface_ID);
                    ConnectionInterface = connectInterface.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
    }
}