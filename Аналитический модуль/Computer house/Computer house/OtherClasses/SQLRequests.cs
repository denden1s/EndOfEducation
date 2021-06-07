using System.Linq;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house.OtherClasses
{
  class SQLRequests
  {
    public static string[] FindProductID(int _product_ID)
    {
      using(ApplicationContext db = new ApplicationContext())
      {
        var findProductName = db.Mediator.Single(i => i.ID == _product_ID);
        string productID = "";
        switch(findProductName.Components_type)
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
          case "SD":
            productID = findProductName.SD_ID;
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
          default:
            break;
        }
        string[] result = { productID, findProductName.Components_type };
        return result;
      }
    }
  }
}