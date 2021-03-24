using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
    public class Cooling_system //: IBase_and_max_options, IEnergy_consumption, ISizes_of_components
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Supported_sockets { get; set; }
        public int Count_of_heat_pipes { get; set; }
        public bool Evaporation_chamber { get; set; }
        public string Type_of_bearing { get; set; }
        public bool Rotation_speed_control { get; set; }
        public int Power_type_ID { get; set; }
        public float Noise_level { get; set; }

        //Доп сведения из БД
        internal string PowerType { get; set; }
        //Реализация интерфейса IBase_and_max_options
        internal int Product_ID { get; set; }
        internal int Max_state { get; set; } //Максимальная скорость вращения кулера

        //Реализация интерфейса IEnergy_consumption
        internal int Consumption { get; set; }
        //Реализация интерфейса ISizes_of_component

        internal int Diameter { get; set; } //Диаметр вентилятора

        public Cooling_system() { }

        public Cooling_system(string _id)
        {
            ID = _id;
        }
        //Выборка данных из БД

        public  void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var CoolingSys = db.Cooling_system.Single(i => i.ID == ID);
                    Name = CoolingSys.Name;
                    Supported_sockets = CoolingSys.Supported_sockets;
                    Count_of_heat_pipes = CoolingSys.Count_of_heat_pipes;
                    Evaporation_chamber = CoolingSys.Evaporation_chamber;
                    Type_of_bearing = CoolingSys.Type_of_bearing;
                    Rotation_speed_control = CoolingSys.Rotation_speed_control;
                    Power_type_ID = CoolingSys.Power_type_ID;
                    Noise_level = CoolingSys.Noise_level;
                    PowerType = db.Power_connectors.Single(i => i.ID == Power_type_ID).Connectors;
                    SetBaseAndMaxOptions(db);
                    SetEnergy_consumption(db);
                    SetSizesOptions(db);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Cooling_system_ID == ID).ID;
            var baseAndMaxOptions = db.Base_and_max_options.Single(i => i.Product_ID == Product_ID);
            Max_state = baseAndMaxOptions.Max_state;
        }

        private void SetEnergy_consumption(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Cooling_system_ID == ID).ID;
            Consumption = db.Energy_consumption.Single(i => i.Product_ID == Product_ID).Consumption;
        }

        private void SetSizesOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Cooling_system_ID == ID).ID;
            var sizes = db.Sizes_of_components.Single(i => i.Product_ID == Product_ID);
            Diameter = sizes.Diameter;
        }
    }
}