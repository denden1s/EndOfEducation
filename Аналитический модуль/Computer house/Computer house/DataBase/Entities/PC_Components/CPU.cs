using System;
using System.Linq;
using System.Windows.Forms;

namespace Computer_house.DataBase.Entities
{
  public class CPU : Product//: IBase_and_max_options, IEnergy_consumption
  {
    
    public CPU() { }

    public CPU(string _id):base (_id){}  
  }
}
