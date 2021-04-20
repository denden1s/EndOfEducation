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

        //Добавление в бд через объект класса !!!

        public static void CreateHoldingDocument(Warehouse_info _infoAboutProduct, int _itemsCount, Users _user, string _deviceType)
        {
            if (_itemsCount < 0)
            {
                //расход 
                using (ApplicationContext db = new ApplicationContext())
                {
                    //Выбор мест откуда можно взять компонент
                    //Сделать switch case
                    List<Products_location> locations = (from b in db.Products_location
                                                         where b.Product_ID == _infoAboutProduct.Product_ID
                                                         && b.Items_count + _itemsCount >= 0
                                                         select b).ToList();
                    if (locations.Count != 0)
                    {
                        locations[0].Items_count += _itemsCount;
                        _infoAboutProduct.Current_items_count += _itemsCount;
                        Locations_in_warehouse locationInWarehouse = new Locations_in_warehouse();
                        locationInWarehouse = db.Locations_in_warehouse.Single(i => i.ID == locations[0].Location_ID);
                        locationInWarehouse.Current_item_count += _itemsCount;
                        db.Warehouse_info.Update(_infoAboutProduct);
                        db.Locations_in_warehouse.Update(locationInWarehouse);
                        db.Products_location.Update(locations[0]);
                        //добавить холдинг документ
                        Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Расход",
                           _itemsCount, _user.ID, locations[0].Location_ID);
                        db.Holding_document.Add(holding_Document);
                        db.SaveChanges();
                        MessageBox.Show("Снятие со склада прошло успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Товар отсутствует на складе");
                    }

                }
            }
            else if (_itemsCount > 0)
            {
                int location_ID = -1;
                //приход
                using (ApplicationContext db = new ApplicationContext())
                {
                    //Выбор мест куда можно определить компонент
                    //Сделать switch case

                    List<Locations_in_warehouse> locations = (from b in db.Locations_in_warehouse
                                                              where b.Location_label.Contains(_deviceType) &&
                                                              b.Max_item_count > b.Current_item_count + _itemsCount
                                                              select b).ToList();

                    if (locations.Count != 0)
                    {
                        int numerator = 0;
                        List<Products_location> LocationsWithThisItem = new List<Products_location>();
                        LocationsWithThisItem = db.Products_location.Where(i => i.Product_ID == _infoAboutProduct.Product_ID).ToList();
                        foreach (var i in locations)
                        {
                            foreach (var a in LocationsWithThisItem)
                            {
                                if ((i.ID == a.Location_ID) && (a.Product_ID == _infoAboutProduct.Product_ID))
                                    location_ID = a.Location_ID;
                            }
                        }
                        //Если такого нет то добавить в первый блок
                        if (location_ID == -1)
                        {
                            location_ID = locations[0].ID;
                            Products_location productsLocation1 = new Products_location(_infoAboutProduct.Product_ID, location_ID, _itemsCount);
                            db.Products_location.Add(productsLocation1);
                        }
                        else
                        {
                            Products_location productsLocation = (from b in db.Products_location
                                                                  where b.Location_ID == location_ID && b.Product_ID == _infoAboutProduct.Product_ID
                                                                  select b).Single();
                            productsLocation.Items_count += _itemsCount;
                            db.Products_location.Update(productsLocation);
                        }
                        Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Приход",
                            _itemsCount, _user.ID, location_ID);


                        Locations_in_warehouse locations_InWarehouse = db.Locations_in_warehouse.Single(i => i.ID == location_ID);
                        locations_InWarehouse.Current_item_count += _itemsCount;
                        db.Locations_in_warehouse.Update(locations_InWarehouse);

                        db.Holding_document.Add(holding_Document);
                        _infoAboutProduct.Current_items_count += _itemsCount;
                        db.Warehouse_info.Update(_infoAboutProduct);
                        db.SaveChanges();
                        MessageBox.Show("Добавление прошло успешно");
                    }
                    else
                        MessageBox.Show("На складе нет места");
                }


            }
        }

    }
}