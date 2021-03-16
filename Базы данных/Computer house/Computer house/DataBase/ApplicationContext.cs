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
    class ApplicationContext : DbContext
    {
        private string ip;
        private SetupIP Adress;

        //Информация склада
        public DbSet<Users> Users { get; set; }
        public DbSet<Holding_document> HoldingDocument { get; set; }
        public DbSet<Locations_in_warehouse> LocationsInWarehouse { get; set; }
        public DbSet<Price_list> PriceList { get; set; }
        public DbSet<Products_location> ProductsLocation { get; set; }
        public DbSet<Warehouse_info> WarehouseInfo { get; set; }

        //Классы основанные на интерфейсах 
        public DbSet<Base_and_max_options> BaseMaxOptions { get; set; }
        public DbSet<Energy_consumption> EnergyConsumptions { get; set; }
        public DbSet<Memory_capacity> MemoryCapacity { get; set; }
        public DbSet<Sizes_of_components> Sizes { get; set; }
        public DbSet<Storage_devices_options> StorageDeviceOptions { get; set; }

        //Комплектующие
        public DbSet<Case> Cases { get; set; }
        public DbSet<Chipset> Chipsets { get; set; }
        public DbSet<Cooling_system> CoolingSystems { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<HDD> HDDs { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PSU> PSUs { get; set; }
        public DbSet<RAM> RAM { get; set; }
        public DbSet<SSD> SSDs { get; set; }

        //Доп сведения
        public DbSet<Connection_interfaces> ConectInterfaces { get; set; }
        public DbSet<CPU_codename> CPUCodenames { get; set; }
        public DbSet<CPU_series> CPUSeries { get; set; }
        public DbSet<Form_factors> FormFactors { get; set; }
        public DbSet<Power_connectors> PowerConnectors { get; set; }
        public DbSet<RAM_chanels> RAMChanels { get; set; }
        public DbSet<RAM_frequency> RAMFrequencies { get; set; }
        public DbSet<Sockets> Sockets { get; set; }
        public DbSet<Memory_types> MemoryTypes { get; set; }

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
            modelBuilder.Entity<Users>().HasNoKey();
            modelBuilder.Entity<Holding_document>().HasNoKey();
            modelBuilder.Entity<Locations_in_warehouse>().HasNoKey();
            modelBuilder.Entity<Price_list>().HasNoKey();
            modelBuilder.Entity<Products_location>().HasNoKey();
            modelBuilder.Entity<Warehouse_info>().HasNoKey();
            modelBuilder.Entity<Base_and_max_options>().HasNoKey();
            modelBuilder.Entity<Energy_consumption>().HasNoKey();
            modelBuilder.Entity<Memory_capacity>().HasNoKey();
            modelBuilder.Entity<Sizes_of_components>().HasNoKey();
            modelBuilder.Entity<Storage_devices_options>().HasNoKey();
            modelBuilder.Entity<Case>().HasNoKey();
            modelBuilder.Entity<Chipset>().HasNoKey();
            modelBuilder.Entity<Cooling_system>().HasNoKey();
            modelBuilder.Entity<CPU>().HasNoKey();
            modelBuilder.Entity<GPU>().HasNoKey();
            modelBuilder.Entity<HDD>().HasNoKey();
            modelBuilder.Entity<Motherboard>().HasNoKey();
            modelBuilder.Entity<PSU>().HasNoKey();
            modelBuilder.Entity<RAM>().HasNoKey();
            modelBuilder.Entity<SSD>().HasNoKey();
            modelBuilder.Entity<Connection_interfaces>().HasNoKey();
            modelBuilder.Entity<CPU_series>().HasNoKey();
            modelBuilder.Entity<Form_factors>().HasNoKey();
            modelBuilder.Entity<Power_connectors>().HasNoKey();
            modelBuilder.Entity<RAM_chanels>().HasNoKey();
            modelBuilder.Entity<RAM_frequency>().HasNoKey();
            modelBuilder.Entity<Sockets>().HasNoKey();
            modelBuilder.Entity<Memory_types>().HasNoKey();
            modelBuilder.Entity<Mediator>().HasNoKey();
        }
    }
}
