using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities.PC_Components
{
public class PSU : Product
  {
    public PSU() { }
    public PSU(string _id)
    {
        ID = _id;
    }
  }
}
