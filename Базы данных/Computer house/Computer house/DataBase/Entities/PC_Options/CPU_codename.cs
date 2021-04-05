﻿
namespace Computer_house.DataBase.Entities
{
    public class CPU_codename : ProductWithOnlyName
    {
        public int ID { get; set; }
        //public string Name { get; set; }
        public CPU_codename() { }
        public CPU_codename(string _name)
        {
            Name = _name;
        }
        public CPU_codename(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }
    }
}
