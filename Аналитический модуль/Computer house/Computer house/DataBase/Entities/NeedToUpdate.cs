using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities
{
  public class NeedToUpdate
  {
    public int ID { get; set; }
    public bool UpdateStatusForWarehouse { get; set; }
    public bool UpdateStatusForShop { get; set; } = false;
  }
}
