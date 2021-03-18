using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities
{
    class CPU : IBase_and_max_options
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

        //Реализация интерфейса IBase_and_max_options
        public int Product_ID { get; set; }
        public int Base_state { get; set; }//Частота процессора
        public int Max_state { get; set; }//Частота процессора

        public CPU() { }

        public CPU(string _id)
        {
            ID = _id;
        }
        //Выгрузка данных из бд
        public void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var cpuInfo = db.CPUs.Single(i => i.ID == ID);
                    Series_ID = cpuInfo.Series_ID;
                    Delivery_type = cpuInfo.Delivery_type;
                    Сores_count = cpuInfo.Сores_count;
                    RAM_frequency_ID = cpuInfo.RAM_frequency_ID;
                    Multithreading = cpuInfo.Multithreading;
                    Integrated_graphic = cpuInfo.Integrated_graphic;
                    Technical_process = cpuInfo.Technical_process;
                    RAM_type_ID = cpuInfo.RAM_type_ID;
                    RAM_chanels_ID = cpuInfo.RAM_chanels_ID;
                    SeriesName = db.CPUSeries.Single(i => i.ID == Series_ID).Name;
                    CodeName = db.CPUCodenames.Single(i => i.ID == Codename_ID).Name;
                    Socket = db.Sockets.Single(i => i.ID == Socket_ID).Name;
                    RAM_frequency = db.RAMFrequencies.Single(i => i.ID == RAM_frequency_ID).Frequency;
                    RAM_chanel = db.RAMChanels.Single(i => i.ID == RAM_chanels_ID).Name;
                    GetRAMTypeInfo(db);
                    SetBaseAndMaxOptions(db);
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        private void GetRAMTypeInfo(ApplicationContext db)
        {
            var listOfRamTypes = (from b in db.MemoryTypes
                                    where b.Device_type == "RAM"
                                    select b).ToList();

            RAM_type = listOfRamTypes.Single(i => i.ID == RAM_type_ID).Name;
        }

        public void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.CPU_ID == ID).ID;
            var baseAndMaxOptions = db.BaseMaxOptions.Single(i => i.Product_ID == Product_ID);
            Base_state = baseAndMaxOptions.Base_state;
            Max_state = baseAndMaxOptions.Max_state;
        }
    }
}
