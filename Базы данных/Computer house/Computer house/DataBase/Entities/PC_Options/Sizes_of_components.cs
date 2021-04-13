using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    public class Sizes_of_components //: ISizes_of_components
    {
        public int Product_ID { get; set; }
        public int Length { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Diameter { get; set; } = 0;
        public float Thickness { get; set; } = 0;
        public int Depth { get; set; } = 0;

        //public void SetSezesOptions(ApplicationContext db)
        //{
        //    throw new NotImplementedException();
        //}
        public Sizes_of_components() { }
    }
}
