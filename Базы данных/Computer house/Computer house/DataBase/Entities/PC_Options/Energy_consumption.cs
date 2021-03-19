using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    class Energy_consumption //: IEnergy_consumption
    {
        public int Product_ID { get; set; }
        public int Consumption { get; set; }

        //public void SetEnergy_consumption(ApplicationContext db)
        //{
        //    throw new NotImplementedException();
        //}
        public Energy_consumption() { }
        public Energy_consumption(int _ID, int _consumption)
        {
            Product_ID = _ID;
            Consumption = _consumption;
        }
    }
}
