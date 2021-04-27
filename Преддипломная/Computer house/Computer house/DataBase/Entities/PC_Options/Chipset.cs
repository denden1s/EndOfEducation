﻿
namespace Computer_house.DataBase.Entities
{
  public class Chipset : ProductWithOnlyName
  {
    public int ID { get; set; }

    public Chipset() { }
    public Chipset(string _name)
    {
      Name = _name;
    }
    public Chipset(int _id,string _name)
    {
      ID = _id;
      Name = _name;
    }
  }
}