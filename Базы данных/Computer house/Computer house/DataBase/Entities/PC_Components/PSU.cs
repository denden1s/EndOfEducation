using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities.PC_Components
{
    public class PSU //: IEnergy_consumption
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string PSU_standart { get; set; }
        public int Line_plus_twelve_V_count { get; set; }
        public int Max_amperage_on_line_plus_twelve { get; set; }
        public int Efficiency { get; set; }
        public int Modularity { get; set; }
        public int Power_motherboard_type_ID { get; set; }
        public string Power_CPU { get; set; }
        public string Power_PCIe { get; set; }
        public string Power_IDE { get; set; }
        public int Sata_power_count { get; set; }
        public bool Power_USB { get; set; }

        //Доп сведения из БД
        internal string PowerMotherboardType { get; set; }

        //Реализация интерфейса IEnergy_consumption
        internal int Product_ID { get; set; }
        internal int Consumption { get; set; }

        public PSU() { }
        public PSU(string _id)
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
                    var psuInfo = db.PSU.Single(i => i.ID == ID);
                    Name = psuInfo.Name;
                    PSU_standart = psuInfo.PSU_standart;
                    Line_plus_twelve_V_count = psuInfo.Line_plus_twelve_V_count;
                    Max_amperage_on_line_plus_twelve = psuInfo.Max_amperage_on_line_plus_twelve;
                    Efficiency = psuInfo.Efficiency;
                    Modularity = psuInfo.Modularity;
                    Power_motherboard_type_ID = psuInfo.Power_motherboard_type_ID;
                    Power_CPU = psuInfo.Power_CPU;
                    Power_PCIe = psuInfo.Power_PCIe;
                    Power_IDE = psuInfo.Power_IDE;
                    Sata_power_count = psuInfo.Sata_power_count;
                    Power_USB = psuInfo.Power_USB;
                    PowerMotherboardType = db.Power_connectors.Single(i => i.ID == Power_motherboard_type_ID).Connectors;
                    SetEnergy_consumption(db);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetEnergy_consumption(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.PSU_ID == ID).ID;
            Consumption = db.Energy_consumption.Single(i => i.Product_ID == Product_ID).Consumption;
        }
    }
}
