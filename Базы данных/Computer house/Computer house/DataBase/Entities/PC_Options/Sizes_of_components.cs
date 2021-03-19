using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    class Sizes_of_components //: ISizes_of_components
    {
        public int Product_ID { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Diameter { get; set; }
        public float Thickness { get; set; }
        public int Depth { get; set; }

        //public void SetSezesOptions(ApplicationContext db)
        //{
        //    throw new NotImplementedException();
        //}
        public Sizes_of_components() { }
    }
}
