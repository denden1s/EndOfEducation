
namespace Computer_house.DataBase.Entities
{
    class Locations_in_warehouse
    {
        public int ID { get; set; }
        public string Location_label { get; set; }
        public int Current_item_count { get; set; }
        public int Max_item_count { get; set; }
    }
}
