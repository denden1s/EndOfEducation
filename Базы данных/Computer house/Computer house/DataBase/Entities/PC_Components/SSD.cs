using System;
using System.Linq;
using Computer_house.OtherClasses;
using Computer_house.DataBase.Entities.PC_Options;

namespace Computer_house.DataBase.Entities.PC_Components
{
    class SSD //: Storage_devices_options
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Record_resource { get; set; }
        public int Work_time_for_fail { get; set; }

        public SSD() { }
        public SSD(string _id)
        {
            ID = _id;
        }
        public void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var ssdInfo = db.SSD.Single(i => i.ID == ID);
                    Name = ssdInfo.Name;
                    Record_resource = ssdInfo.Record_resource;
                    Work_time_for_fail = ssdInfo.Work_time_for_fail;
                    //SetStorageOptions("SSD");
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
        }
    }
}