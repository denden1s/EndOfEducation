using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities.PC_Components
{
    class Motherboard
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

        public Motherboard() { }

        public Motherboard(bool _SLI,bool _integrated_graphic, 
            int[] _iArr, params string[] _strArr)
        {
            ID = _strArr[0];
            Name = _strArr[1];
            Supported_CPU = _strArr[2];
            Socket_ID = _iArr[0];
            Chipset_ID = _iArr[1];
            Form_factor_ID = _iArr[2];
            RAM_type_ID = _iArr[3];
            RAM_chanels_ID = _iArr[4];
            Count_of_memory_slots = _iArr[5];
            Expansion_slots = _strArr[3];
            Storage_interfaces = _strArr[4];
            SLI_support = _SLI;
            Integrated_graphic = _integrated_graphic;
            Connectors = _strArr[5];
            GetSocketInfo();
            GetChipsetInfo();
            GetFormFactorInfo();
            GetRAMTypeInfo();
            GetRAMChanelsInfo();
        }
        //Выборка данных из БД 
        private void GetSocketInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var socket = db.Sockets.Single(i => i.ID == Socket_ID);
                    Socket = socket.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetChipsetInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var chipset = db.Chipsets.Single(i => i.ID == Chipset_ID);
                    Chipset = chipset.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetFormFactorInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var listOfFormFactors = (from b in db.FormFactors
                                             where b.Device_type == "Motherboard"
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

        private void GetRAMChanelsInfo()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var chanel = db.RAMChanels.Single(i => i.ID == RAM_chanels_ID);
                    RAM_chanel = chanel.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
    }
}