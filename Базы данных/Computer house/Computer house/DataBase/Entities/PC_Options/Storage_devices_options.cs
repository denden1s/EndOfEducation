using Computer_house.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.PC_Options
{
    class Storage_devices_options : IStorage_devices_options
    {
        public int Storage_device_ID { get; set; }
        public int Interface_ID { get; set; }
        public int Form_factor_ID { get; set; }
        public int Buffer { get; set; }
        public bool Hardware_encryption { get; set; }
        public int Sequential_read_speed { get; set; }
        public int Sequeintial_write_speed { get ; set; }
        public int Random_read_speed { get; set; }
        public int Random_write_speed { get; set; }

        public void SetStorageOptions()
        {
            throw new NotImplementedException();
        }
    }
}
