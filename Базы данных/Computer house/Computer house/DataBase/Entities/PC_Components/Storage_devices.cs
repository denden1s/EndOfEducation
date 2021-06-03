using System.Linq;

namespace Computer_house.DataBase.Entities.PC_Options
{
  public class Storage_devices : Product
  {
    public int Interface_ID { get; set; }
    public int Form_factor_ID { get; set; }
    public int Buffer { get; set; }
    public bool Hardware_encryption { get; set; }
    public int Sequential_read_speed { get; set; }
    public int Sequeintial_write_speed { get ; set; }
    public int Random_read_speed { get; set; }
    public int Random_write_speed { get; set; }

    //Доп данные из БД
    internal string FormFactor { get; set; }
    internal string ConnectionInterface { get; set; }

    //Реализация интерфейса IMemory_capacity
    internal int Product_ID { get; set; } //Аналогично свойству Storage_device_ID
    internal int Capacity { get; set; }

    //Реализация интерфейса IEnergy_consumption
    internal int Consumption { get; set; }

    //Реализация интерфейса ISizes_of_components
    internal float Thickness { get; set; } //Толщина устройства

    public Storage_devices() { }

    public Storage_devices(string id)
    {
      ID = id;
    }

    public void GetDataFromDB()
    {
      using(ApplicationContext db = new ApplicationContext())
      {
        Product_ID = (from b in db.Mediator
                      where b.Components_type == "SD" && b.SD_ID == ID
                      select b.ID).FirstOrDefault();
        Storage_devices storageDevice = db.Storage_devices.Single(i => i.ID == ID);
        Name = storageDevice.Name;
        Interface_ID = storageDevice.Interface_ID;
        Form_factor_ID = storageDevice.Form_factor_ID;
        FormFactor = (from b in db.Form_factors
                      where b.Device_type == "SD" && b.ID == Form_factor_ID
                      select b.Name).Single();
        ConnectionInterface = db.Connection_interfaces.Single(i => i.ID == Interface_ID).Name;
        Buffer = storageDevice.Buffer;
        Hardware_encryption = storageDevice.Hardware_encryption;
        Sequential_read_speed = storageDevice.Sequential_read_speed;
        Sequeintial_write_speed = storageDevice.Sequeintial_write_speed;
        Random_read_speed = storageDevice.Random_read_speed;
        Random_write_speed = storageDevice.Random_write_speed;
        Capacity = db.Memory_capacity.Single(i => i.Product_ID == Product_ID).Capacity;
        Consumption = db.Energy_consumption.Single(i => i.Product_ID == Product_ID).Consumption;
        Thickness = db.Sizes_of_components.Single(i => i.Product_ID == Product_ID).Thickness;
      }
    }
  }
}