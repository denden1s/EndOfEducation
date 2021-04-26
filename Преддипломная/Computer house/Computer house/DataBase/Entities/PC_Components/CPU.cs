using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
    public class CPU : Product//: IBase_and_max_options, IEnergy_consumption
    {
        //public string ID { get; set; }
        //public string Name { get; set; }
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
        internal string SeriesName { get; set; }
        internal string CodeName { get; set; }
        internal string Socket { get; set; }
        internal int RAM_frequency { get; set; }
        internal string RAM_type { get; set; }
        internal string RAM_chanel { get; set; }

        //Реализация интерфейса IBase_and_max_options
        internal int Product_ID { get; set; }
        internal int Base_state { get; set; }//Частота процессора
        internal int Max_state { get; set; }//Частота процессора

        //Реализация интерфейса IEnergy_consumption
        internal int Consumption { get; set; }

        public CPU() { }

        public CPU(string _id):base (_id){}
        //Выгрузка данных из бд
        public override void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var cpuInfo = db.CPU.Single(i => i.ID == ID);
                    Name = cpuInfo.Name;
                    Series_ID = cpuInfo.Series_ID;
                    Delivery_type = cpuInfo.Delivery_type;
                    Codename_ID = cpuInfo.Codename_ID;
                    Socket_ID = cpuInfo.Socket_ID;
                    Сores_count = cpuInfo.Сores_count;
                    RAM_frequency_ID = cpuInfo.RAM_frequency_ID;
                    Multithreading = cpuInfo.Multithreading;
                    Integrated_graphic = cpuInfo.Integrated_graphic;
                    Technical_process = cpuInfo.Technical_process;
                    RAM_type_ID = cpuInfo.RAM_type_ID;
                    RAM_chanels_ID = cpuInfo.RAM_chanels_ID;
                    SeriesName = db.CPU_series.Single(i => i.ID == Series_ID).Name;
                    CodeName = db.CPU_codename.Single(i => i.ID == Codename_ID).Name;
                    Socket = db.Sockets.Single(i => i.ID == Socket_ID).Name;
                    RAM_frequency = db.RAM_frequency.Single(i => i.ID == RAM_frequency_ID).Frequency;
                    RAM_chanel = db.RAM_chanels.Single(i => i.ID == RAM_chanels_ID).Name;
                    GetRAMTypeInfo(db);
                    SetBaseAndMaxOptions(db);
                    SetEnergy_consumption(db);
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

        public void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.CPU_ID == ID).ID;
            var baseAndMaxOptions = db.Base_and_max_options.Single(i => i.Product_ID == Product_ID);
            Base_state = baseAndMaxOptions.Base_state;
            Max_state = baseAndMaxOptions.Max_state;
        }

        public void SetEnergy_consumption(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.CPU_ID == ID).ID;
            Consumption = db.Energy_consumption.Single(i => i.Product_ID == Product_ID).Consumption;
        }
    }
}
