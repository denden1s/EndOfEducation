using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities.PC_Components
{
  public class Motherboard : Product 
  {
    public string Supported_CPU { get; set; }
    public int Socket_ID { get; set; }
    public int Chipset_ID { get; set; }
    public int Form_factor_ID { get; set; }
    public int RAM_type_ID { get; set; }
    public int Count_of_memory_slots { get; set; }
    public int RAM_chanels_ID { get; set; }
    public string Expansion_slots { get; set; }
    public string Storage_interfaces { get; set; }
    public bool SLI_support { get; set; }
    public bool Integrated_graphic { get; set; }
    public string Connectors { get; set; }

    //Доп сведения из БД
    internal string Socket { get; set; }
    internal string Chipset { get; set; }
    internal string FormFactor { get; set; }
    internal string RAM_type { get; set; }
    internal string RAM_chanel { get; set; }
    internal int RAM_frequency { get; set; }

    //Реализация интерфейса IMemory_capacity
    internal int Product_ID { get; set; }
    internal int Capacity { get; set; } //Максимальный объём ОЗУ

    //Реализация интерфейса ISizes_of_components 
    internal int Length { get; set; } //Длина платы
    internal int Width { get; set; } //Ширина платы

    public Motherboard() { }

    public Motherboard(string _id) : base(_id) { }
    //Выборка данных из БД 
    public void GetDataFromDB()
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          var mbInfo = db.Motherboard.Single(i => i.ID == ID);
          Name = mbInfo.Name;
          Supported_CPU = mbInfo.Supported_CPU;
          Socket_ID = mbInfo.Socket_ID;
          Chipset_ID = mbInfo.Chipset_ID;
          Form_factor_ID = mbInfo.Form_factor_ID;
          RAM_type_ID = mbInfo.RAM_type_ID;
          RAM_chanels_ID = mbInfo.RAM_chanels_ID;
          Count_of_memory_slots = mbInfo.Count_of_memory_slots;
          Expansion_slots = mbInfo.Expansion_slots;
          Storage_interfaces = mbInfo.Storage_interfaces;
          SLI_support = mbInfo.SLI_support;
          Integrated_graphic = mbInfo.Integrated_graphic;
          Connectors = mbInfo.Connectors;
          Socket = db.Sockets.Single(i => i.ID == Socket_ID).Name;
          Chipset = db.Chipset.Single(i => i.ID == Chipset_ID).Name;
          RAM_chanel = db.RAM_chanels.Single(i => i.ID == RAM_chanels_ID).Name;
          GetFormFactorInfo(db);
          GetRAMTypeInfo(db);
          SetMemoryCapacity(db);
          SetSizesOptions(db);
          SetFrequency(db);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
        
    private void GetFormFactorInfo(ApplicationContext db)
    {
      var listOfFormFactors = (from b in db.Form_factors
                                where b.Device_type == "Motherboard"
                                  select b).ToList();

      FormFactor = listOfFormFactors.Single(i => i.ID == Form_factor_ID).Name;
    }

    private void GetRAMTypeInfo(ApplicationContext db)
    {
      var listOfRamTypes = (from b in db.Memory_types
                            where b.Device_type == "RAM"
                              select b).ToList();

      RAM_type = listOfRamTypes.Single(i => i.ID == RAM_type_ID).Name;
    }

    private void SetMemoryCapacity(ApplicationContext db)
    {
      Product_ID = (from b in db.Mediator
                    where b.Components_type == "Motherboard" && b.Motherboard_ID == ID
                    select b.ID).FirstOrDefault();
      Capacity = db.Memory_capacity.Single(i => i.Product_ID == Product_ID).Capacity;
    }

    private void SetSizesOptions(ApplicationContext db)
    {
      var motherboardList = db.Mediator.Where(i => i.Components_type == "Motherboard");
      Product_ID = motherboardList.Single(i => i.Motherboard_ID == ID).ID;
      var sizesInfo = db.Sizes_of_components.Single(i => i.Product_ID == Product_ID);
      Length = sizesInfo.Length;
      Width = sizesInfo.Width;
    }
    private void SetFrequency(ApplicationContext db)
    {     
      var motherboardList = db.Mediator.Where(i => i.Components_type == "Motherboard");
      Product_ID = motherboardList.Single(i => i.Motherboard_ID == ID).ID;
      var baseAndMaxOptions = db.Base_and_max_options.Single(i => i.Product_ID == Product_ID);

      RAM_frequency = baseAndMaxOptions.Max_state;          
    }
  }
}