
using System;

namespace Computer_house.DataBase.Entities
{
    class Holding_document
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public string State { get; set; }
        public int Items_count_in_move { get; set; }
        public DateTime Time { get; set; } //Console.WriteLine("f: {0:f}", now); нужное форматирование
        public int User_ID { get; set; }
        public int Location_ID { get; set; }
    }
}
