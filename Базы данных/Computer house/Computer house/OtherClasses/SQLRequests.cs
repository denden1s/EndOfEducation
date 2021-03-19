using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.OtherClasses
{
    class SQLRequests
    {
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
                    case "Cooling":
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

        public static List<int> FindIntID(string _componentType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var ID_inInt = (from b in db.Mediator
                                where b.Components_type == _componentType
                                select b.ID).ToList();
                return ID_inInt;
            }
        }

        public static string FindCPU(string productID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.CPU.Single(i => i.ID == productID).ID;
            }
        }
    }
}
