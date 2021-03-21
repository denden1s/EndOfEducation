
using Computer_house.OtherClasses;
using System;
using System.Linq;

namespace Computer_house.DataBase.Entities
{
   public  class Holding_document
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public string State { get; set; }
        public int Items_count_in_move { get; set; }
        public DateTime Time { get; set; } //Console.WriteLine("f: {0:f}", now); нужное форматирование
        public int User_ID { get; set; }
        public int Location_ID { get; set; }

        //Доп данные из БД
        internal string User_name { get; set; }
        internal string Product_name { get; set; }
        internal string Location_name { get; set; }

        public Holding_document() { }
        public Holding_document(int _productID, string _state, int _itemsCountInMove,
            DateTime _time, int _userID, int _locationID) 
        {
            Product_ID = _productID;
            State = _state;
            Items_count_in_move = _itemsCountInMove;
            Time = _time;
            User_ID = _userID;
            Location_ID = _locationID;
        }
        //Выборка данных из БД
        public void GetDataFromDB()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    //User_name = db.Users.Single(i => i.ID == User_ID).Login;
                    //var findProductName = db.Mediator.Single(i => i.ID == Product_ID).;
                    //string productID = "";
                    //if (findProductName.Case_ID != null) productID = findProductName.Case_ID;
                    //if (findProductName.Cooling_system_ID != null) productID = findProductName.Cooling_system_ID;
                    //if (findProductName.CPU_ID != null) productID = findProductName.CPU_ID;
                    //if (findProductName.GPU_ID != null) productID = findProductName.GPU_ID;
                    //if (findProductName.HDD_ID != null) productID = findProductName.HDD_ID;
                    //if (findProductName.Motherboard_ID != null) productID = findProductName.Motherboard_ID;
                    //if (findProductName.PSU_ID != null) productID = findProductName.PSU_ID;
                    //if (findProductName.RAM_ID != null) productID = findProductName.RAM_ID;
                    //if (findProductName.SSD_ID != null) productID = findProductName.SSD_ID;

                    //Product_ID = 
                    //Location_name = db.LocationsInWarehouse.Single(i => i.ID == Location_ID).Location_label;
                }
            }
            catch (Exception ex)
            {
                SystemFunctions.SetNewDataBaseAdress(ex);
            }
            
        }

    }
}
