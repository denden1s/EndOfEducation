using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    class Memory_capacity //: IMemory_capacity
    {
        public int Product_ID { get; set; }
        public int Capacity { get; set; }

        //public void SetMemoryCapacity()
        //{
        //    throw new NotImplementedException();
        //}
        public Memory_capacity() { }
        public Memory_capacity(int _ID, int _capacity)
        {
            Product_ID = _ID;
            Capacity = _capacity;
        }
    }
}
