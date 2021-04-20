using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public Product() { }
        public Product(string _id)
        {
            ID = _id;
        }
    }
}
