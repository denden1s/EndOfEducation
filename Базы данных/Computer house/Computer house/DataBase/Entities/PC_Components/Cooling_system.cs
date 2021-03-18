using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using System;
using System.Linq;
namespace Computer_house.DataBase.Entities
{
    class Cooling_system : IBase_and_max_options
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
        public string PowerType { get; set; }
        //Реализация интерфейса IBase_and_max_options
        public int Product_ID { get; set; }
        public int Base_state { get; set; } //Не используется
        public int Max_state { get; set; } //Максимальная скорость вращения кулера

        public Cooling_system() { }

        public Cooling_system(string _id)
        {
            ID = _id;
        }
        //Выборка данных из БД

        private void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var CoolingSys = db.CoolingSystems.Single(i => i.ID == ID);
                    Name = CoolingSys.Name;
                    Supported_sockets = CoolingSys.Supported_sockets;
                    Count_of_heat_pipes = CoolingSys.Count_of_heat_pipes;
                    Evaporation_chamber = CoolingSys.Evaporation_chamber;
                    Type_of_bearing = CoolingSys.Type_of_bearing;
                    Rotation_speed_control = CoolingSys.Rotation_speed_control;
                    Power_type_ID = CoolingSys.Power_type_ID;
                    Noise_level = CoolingSys.Noise_level;
                    PowerType = db.PowerConnectors.Single(i => i.ID == Power_type_ID).Connectors;
                    SetBaseAndMaxOptions(db);
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }

        public void SetBaseAndMaxOptions(ApplicationContext db)
        {
            Product_ID = db.Mediator.Single(i => i.Cooling_system_ID == ID).ID;
            var baseAndMaxOptions = db.BaseMaxOptions.Single(i => i.Product_ID == Product_ID);
            Max_state = baseAndMaxOptions.Max_state;
        }
    }
}