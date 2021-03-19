
namespace Computer_house.DataBase.Entities.Warehouse
{
    class Products_location
    {
        public int Product_ID { get; set; }
        public int Location_ID { get; set; }
        public int Items_count { get; set; }

        public Products_location() { }
        public Products_location(int _productID, int _locationID, int _itemsCount) 
        {
            Product_ID = _productID;
            Location_ID = _locationID;
            Items_count = _itemsCount;
        }
    }
}
