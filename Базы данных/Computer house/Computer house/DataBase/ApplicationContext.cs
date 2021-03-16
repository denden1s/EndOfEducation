using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase
{
    class ApplicationContext : DbContext
    {
        private string ip;
        private SetupIP Adress;

        //Warehouse data
        public DbSet<Users> Users { get; set; }
        public DbSet<Holding_document> HoldingDocument { get; set; }
        public DbSet<Locations_in_warehouse> LocationsInWarehouse { get; set; }
        public DbSet<Price_list> PriceList { get; set; }
        public DbSet<Products_location> ProductsLocation { get; set; }
        public DbSet<Warehouse_info> WarehouseInfo { get; set; }

        //Interfaces 


        //Комплектующие


        //Доп сведения


        public ApplicationContext()
        {
            Adress = new SetupIP();
            ip = Adress.GetIP();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //192.168.1.2
            optionsBuilder.UseSqlServer("Server = " + ip + "\\SQLEXPRESS,1433; Database =  Computer_house; Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Server = " + ip + "\\SQLEXPRESS,1433; Database =  Computer_house;  User ID = Inna; Password =123;");
        }
    }
}
