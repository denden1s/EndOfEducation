using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities
{
    class CPU
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Series_ID { get; set; }
        public string Delivery_type { get; set; }
        public int Codename_ID { get; set; }
        public int Socket_ID { get; set; }
        public int Сores_count { get; set; }
        public bool Multithreading { get; set; }
        public int RAM_frequency_ID { get; set; }
        public int RAM_type_ID { get; set; }
        public int RAM_chanels_ID { get; set; }
        public bool Integrated_graphic { get; set; }
        public int Technical_process { get; set; }

        //Дополнения к БД
        public string SeriesName { get; set; }
        public string CodeName { get; set; }
        public string Socket { get; set; }
        public int RAM_frequency { get; set; }
        public string RAM_type { get; set; }
        public string RAM_chanel { get; set; }

        public CPU() { }

        
        public CPU(string _id, string _name, string _deliveryType,
            bool _multiCore, bool _integratedGraphic, params int[] _iArr)
        {
            ID = _id;
            Name = _name;
            Series_ID = _iArr[0];
            Delivery_type = _deliveryType;
            Codename_ID = _iArr[1];
            Socket_ID = _iArr[2];
            Сores_count = _iArr[3];
            RAM_frequency_ID = _iArr[4];
            Multithreading = _multiCore;
            Integrated_graphic = _integratedGraphic;
            Technical_process = _iArr[5];
            RAM_type_ID = _iArr[6];
            RAM_chanels_ID = _iArr[7];
            GetSeriesName();
            GetCodeName();
            GetSocketInfo();
            GetRAMFrequencyInfo();
            GetRAMTypeInfo();
            GetRAMChanelsInfo();
        }
        //Выгрузка данных из бд
        private void GetSeriesName()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var series = db.CPUSeries.Single(i => i.ID == Series_ID);
                    SeriesName = series.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetCodeName()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var codeName = db.CPUCodenames.Single(i => i.ID == Codename_ID);
                    CodeName = codeName.Name;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

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
