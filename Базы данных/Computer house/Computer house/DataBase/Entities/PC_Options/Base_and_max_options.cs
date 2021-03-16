using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    class Base_and_max_options : IBase_and_max_options
    {
        public int Product_ID { get; set; }
        public int Base_state { get; set; }
        public int Max_state { get; set; }

        void IBase_and_max_options.SetBaseAndMaxOptions(){}
    }
}
