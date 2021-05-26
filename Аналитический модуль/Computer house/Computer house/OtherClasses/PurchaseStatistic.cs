using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.OtherClasses
{
  class PurchaseStatistic
  {
    public DateTime Time { get; set; }
    public decimal Purchase { get; set; }
    public decimal Sales { get; set; }
    public decimal Income { get; set; }


    public PurchaseStatistic(DateTime time, decimal purchase, decimal sales)
    {
      Time = time;
      Purchase = purchase;
      Sales = sales;
    }

    public void SetIncome()
    {
      Income = Sales - Purchase;
    }

  }
}
