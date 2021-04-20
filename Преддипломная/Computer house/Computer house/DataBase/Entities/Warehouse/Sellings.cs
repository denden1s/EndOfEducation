using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase.Entities.Warehouse
{
    public class Sellings
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }

        public string Payment_method { get; set; }

        public DateTime Selling_date { get; set; } = DateTime.Now;

        public decimal Price { get; set; }

        public int Worker_ID { get; set; }
    }
}
