using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house.OtherClasses
{
    class SQLRequests
    {
        //Нужен для определения наименования продукта по ID
        public static string[] FindProductID(int _product_ID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var findProductName = db.Mediator.Single(i => i.ID == _product_ID);
                string productID = "";
                switch (findProductName.Components_type)
                {
                    case "CPU":
                        productID = findProductName.CPU_ID;
                        break;
                    case "Case":
                        productID = findProductName.Case_ID;
                        break;
                    case "Cooling system":
                        productID = findProductName.Cooling_system_ID;
                        break;
                    case "GPU":
                        productID = findProductName.GPU_ID;
                        break;
                    case "HDD":
                        productID = findProductName.HDD_ID;
                        break;
                    case "Motherboard":
                        productID = findProductName.Motherboard_ID;
                        break;
                    case "PSU":
                        productID = findProductName.PSU_ID;
                        break;
                    case "RAM":
                        productID = findProductName.RAM_ID;
                        break;
                    case "SSD":
                        productID = findProductName.SSD_ID;
                        break;
                    default:
                        break;
                }
                string[] result = { productID, findProductName.Components_type };
                return result;
            }
        }

        //Нужен для проведения документов при оформлении покупки
        public static void CreateHoldingDocument(Warehouse_info _infoAboutProduct, Users _user, 
                                                 string _deviceType, decimal _price)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Locations_in_warehouse locationInWarehouse = new Locations_in_warehouse();
                locationInWarehouse = db.Locations_in_warehouse.Single(i => i.ID == 9);
                locationInWarehouse.Current_item_count--;
                db.Warehouse_info.Update(_infoAboutProduct);
                db.Locations_in_warehouse.Update(locationInWarehouse);
                Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Расход",
                                                    1, _user.ID, 9);
                db.Holding_document.Add(holding_Document);
                Sellings selling = new Sellings(_infoAboutProduct.Product_ID, "единовременно", _user.ID, _price);
                db.Sellings.Add(selling);
                NeedToUpdate needToUpdate = db.NeedToUpdate.Single(i => i.ID == 1);
                needToUpdate.UpdateStatus = true;
                db.NeedToUpdate.Update(needToUpdate);
                //позже нужно сделать чтобы работало и в обратную сторону
                db.SaveChanges();           
            }
        }
    }
}