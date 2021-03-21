using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.DataBase.Interfaces;
using Computer_house.OtherClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_house.DataBase
{
    public class ApplicationContext : DbContext
    {
        private string ip;
        private SetupIP Adress;

        //Все имена совпадают с именами из БД

        //Информация склада
        public DbSet<Users> Users { get; set; }
        public DbSet<Holding_document> Holding_document { get; set; }
        public DbSet<Locations_in_warehouse> Locations_in_warehouse { get; set; }
        public DbSet<Price_list> Price_list { get; set; }
        public DbSet<Products_location> Products_location { get; set; }
        public DbSet<Warehouse_info> Warehouse_info { get; set; }

        //Классы основанные на интерфейсах 
        public DbSet<Base_and_max_options> Base_and_max_options { get; set; }
        public DbSet<Energy_consumption> Energy_consumption { get; set; }
        public DbSet<Memory_capacity> Memory_capacity { get; set; }
        public DbSet<Sizes_of_components> Sizes_of_components { get; set; }
        public DbSet<Storage_devices_options> Storage_devices_options { get; set; }

        //Комплектующие
        public DbSet<Case> Case { get; set; }
        public DbSet<Chipset> Chipset { get; set; }
        public DbSet<Cooling_system> Cooling_system { get; set; }
        public DbSet<CPU> CPU { get; set; }
        public DbSet<GPU> GPU { get; set; }
        public DbSet<HDD> HDD { get; set; }
        public DbSet<Motherboard> Motherboard { get; set; }
        public DbSet<PSU> PSU { get; set; }
        public DbSet<RAM> RAM { get; set; }
        public DbSet<SSD> SSD { get; set; }

        //Доп сведения
        public DbSet<Connection_interfaces> Connection_interfaces { get; set; }
        public DbSet<CPU_codename> CPU_codename { get; set; }
        public DbSet<CPU_series> CPU_series { get; set; }
        public DbSet<Form_factors> Form_factors { get; set; }
        public DbSet<Power_connectors> Power_connectors { get; set; }
        public DbSet<RAM_chanels> RAM_chanels { get; set; }
        public DbSet<RAM_frequency> RAM_frequency { get; set; }
        public DbSet<Sockets> Sockets { get; set; }
        public DbSet<Memory_types> Memory_types { get; set; }

        //Посредник
        public DbSet<Mediator> Mediator { get; set; }
        public ApplicationContext()
        {
            Adress = new SetupIP();
            ip = Adress.GetIP();
        }

        //Нужен для конфигурации подключения к БД
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //192.168.1.2
            
            optionsBuilder.UseSqlServer("Server = " + ip + "\\SQLEXPRESS, 1433;" +
                " Database = Computer_house; User ID = root; Password = root; ");
                //optionsBuilder.UseSqlServer("Server = " + ip + "\\SQLEXPRESS,1433; Database =  Computer_house;  User ID = Inna; Password =123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //первичные ключи
            //modelBuilder.Entity<Users>().HasKey(k => k.ID);
            //modelBuilder.Entity<Holding_document>().HasKey(k => k.ID);
            //modelBuilder.Entity<Holding_document>().HasKey(k => k.ID);
            //ПОКА СОЙДЕТ ТАК 

            //ToDo: Исправить 
            modelBuilder.Entity<Users>().HasKey(i => i.ID);
            modelBuilder.Entity<Holding_document>().HasKey(i => i.ID);
            modelBuilder.Entity<Locations_in_warehouse>().HasKey(i => i.ID);
            modelBuilder.Entity<Price_list>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Products_location>().HasKey(k => new { k.Product_ID, k.Location_ID });
            modelBuilder.Entity<Warehouse_info>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Base_and_max_options>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Energy_consumption>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Memory_capacity>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Sizes_of_components>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Storage_devices_options>().HasKey(i => i.Product_ID);
            modelBuilder.Entity<Case>().HasKey(i => i.ID);
            modelBuilder.Entity<Chipset>().HasKey(i => i.ID);
            modelBuilder.Entity<Cooling_system>().HasKey(i => i.ID);
            modelBuilder.Entity<CPU>().HasKey(i => i.ID);
            modelBuilder.Entity<GPU>().HasKey(i => i.ID);
            modelBuilder.Entity<HDD>().HasKey(i => i.ID);
            modelBuilder.Entity<Motherboard>().HasKey(i => i.ID);
            modelBuilder.Entity<PSU>().HasKey(i => i.ID);
            modelBuilder.Entity<RAM>().HasKey(i => i.ID);
            modelBuilder.Entity<SSD>().HasKey(i => i.ID);
            modelBuilder.Entity<Connection_interfaces>().HasKey(i => i.ID);
            modelBuilder.Entity<CPU_series>().HasKey(i => i.ID);
            modelBuilder.Entity<Form_factors>().HasKey(i => i.ID);
            modelBuilder.Entity<Power_connectors>().HasKey(i => i.ID);
            modelBuilder.Entity<RAM_chanels>().HasKey(i => i.ID);
            modelBuilder.Entity<RAM_frequency>().HasKey(i => i.ID);
            modelBuilder.Entity<Sockets>().HasKey(i => i.ID);
            modelBuilder.Entity<Memory_types>().HasKey(i => i.ID);
            modelBuilder.Entity<Mediator>().HasKey(i => i.ID);
        }
    }
}
