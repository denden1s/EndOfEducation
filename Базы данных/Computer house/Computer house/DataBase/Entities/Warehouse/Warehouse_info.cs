
using Computer_house.OtherClasses;
using System.Linq;

namespace Computer_house.DataBase.Entities.Warehouse
{
    public class Warehouse_info
    {
        public int Product_ID { get; set; }
        public int Current_items_count { get; set; }
        public int Items_in_shop { get; set; } = 0;
        internal string ProductName { get; set; }
        internal string ProductType { get; set; }

        public Warehouse_info() { }
        public Warehouse_info(int _productID, int _currentCount) 
        {
            Product_ID = _productID;
            Current_items_count = _currentCount;
            SetName();
        }

        public void SetName()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string[] temp = SQLRequests.FindProductID(Product_ID);
                ProductType = temp[1];
                switch (temp[1])
                {
                    case "CPU":
                        ProductName = db.CPU.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "Case":
                        ProductName = db.Case.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "Cooling system":
                        ProductName = db.Cooling_system.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "GPU":
                        ProductName = db.GPU.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "SD":
                        ProductName = db.Storage_devices.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "Motherboard":
                        ProductName = db.Motherboard.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "PSU":
                        ProductName = db.PSU.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "RAM":
                        ProductName = db.RAM.Single(i => i.ID == temp[0]).Name;
                        break;
                    case "SSD":
                        break;
                    default:
                        break;
                }
            }
                
        }

    }
}
