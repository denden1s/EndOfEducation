using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    public class Base_and_max_options //: IBase_and_max_options
    {
        public int Product_ID { get; set; }
        public int Base_state { get; set; }
        public int Max_state { get; set; }

        //Нужен когда нет максимального или базового состояния
        //public Base_and_max_options(int _id, int _unknowState, string _explain)
        //{
        //    Product_ID = _id;
        //    if (_explain == "base")
        //        Base_state = _unknowState;
        //    else if (_explain == "max")
        //        Max_state = _unknowState;
        //}
        public Base_and_max_options() { }
        public Base_and_max_options(int _id, int _bState, int _mState)
        {
            Product_ID = _id;
            Base_state = _bState;
            Max_state = _mState;
        }
            //void IBase_and_max_options.SetBaseAndMaxOptions(ApplicationContext db)
            //{

            //}
        }
}
