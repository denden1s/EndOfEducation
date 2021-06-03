using System.Linq;

namespace Computer_house.DataBase.Entities
{
  public class Mediator
  {
    public int ID { get; set; }
    public string Components_type { get; set; }
    public string CPU_ID { get; set; }
    public string GPU_ID { get; set; }
    public string Case_ID { get; set; }
    public string RAM_ID { get; set; }
    public string Cooling_system_ID { get; set; }
    public string PSU_ID { get; set; }
    public string SD_ID { get; set; }
    public string Motherboard_ID { get; set; }

    public Mediator() { }

    public Mediator(params string[] components)
    {
      Components_type = "PC";
      using(ApplicationContext db = new ApplicationContext())
      {
        CPU_ID = db.CPU.Single(i => i.Name == components[0]).ID;
        GPU_ID = db.GPU.Single(i => i.Name ==  components[1]).ID;
        Case_ID = db.Case.Single(i => i.Name == components[2]).ID;
        RAM_ID = db.RAM.Single(i => i.Name ==  components[3]).ID;
        Cooling_system_ID = db.Cooling_system.Single(i => i.Name == components[4]).ID;
        PSU_ID = db.PSU.Single(i => i.Name == components[5]).ID;
        SD_ID = db.Storage_devices.Single(i => i.Name == components[6]).ID;
        Motherboard_ID = db.Motherboard.Single(i => i.Name == components[7]).ID;
      }
    }
  }
}