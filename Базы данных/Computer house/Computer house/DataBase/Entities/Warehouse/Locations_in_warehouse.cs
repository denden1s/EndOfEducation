
namespace Computer_house.DataBase.Entities
{
   public  class Locations_in_warehouse
    {
        public int ID { get; set; }
        public string Location_label { get; set; }
        public int Current_item_count { get; set; } = 0;
        public int Max_item_count { get; set; }
        public Locations_in_warehouse() { }
        public Locations_in_warehouse(int _id, string _label, int _currentItemsCount, int _maxItemsCount) 
        {
            ID = _id;
            Location_label = _label;
            Current_item_count = _currentItemsCount;
            Max_item_count = _maxItemsCount;
        }
    }
}
