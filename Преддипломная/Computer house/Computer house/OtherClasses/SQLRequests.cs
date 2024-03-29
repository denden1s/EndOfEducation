﻿using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.Warehouse;
using System.Linq;
using System.Threading;
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

    //Нужен для проведения документов при оформлении покупки
    public static void CreateHoldingDocument(Warehouse_info _infoAboutProduct, Users _user, 
      string _deviceType, decimal _price, string paymentMethod)
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
        Sellings selling = new Sellings(_infoAboutProduct.Product_ID, paymentMethod, _user.ID, _price);
        db.Sellings.Add(selling);
        //позже нужно сделать чтобы работало и в обратную сторону
        db.SaveChanges();
      }
    }

    public static void UpdateWarehouseData()
    {
      using(ApplicationContext db = new ApplicationContext())
      {
        NeedToUpdate needToUpdate = db.NeedToUpdate.Single(i => i.ID == 1);
        needToUpdate.UpdateStatusForWarehouse = true;
        db.NeedToUpdate.Update(needToUpdate);
        db.SaveChanges();
      }
    }

    public static void CreateAuthentificationLog(Authentification_logs log)
    {
      using(ApplicationContext db = new ApplicationContext())
      {
        db.Authentification_logs.Add(log);
        db.SaveChanges();
      }
    }
  }
}