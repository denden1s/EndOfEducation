using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using Microsoft.EntityFrameworkCore;

namespace Computer_house.DataBase
{
  public class ApplicationContext : DbContext
  {
    private string ip;
    private SetupIP Adress;

    //Информация склада
    public DbSet<Users> Users { get; set; }
    public DbSet<Holding_document> Holding_document { get; set; }
    public DbSet<Locations_in_warehouse> Locations_in_warehouse { get; set; }
    public DbSet<Price_list> Price_list { get; set; }
    public DbSet<Products_location> Products_location { get; set; }
    public DbSet<Warehouse_info> Warehouse_info { get; set; }
    public DbSet<Storage_devices> Storage_devices { get; set; }

    //Комплектующие
    public DbSet<Case> Case { get; set; }
    public DbSet<NeedToUpdate> NeedToUpdate { get; set; }
    public DbSet<Cooling_system> Cooling_system { get; set; }
    public DbSet<CPU> CPU { get; set; }
    public DbSet<GPU> GPU { get; set; }
    public DbSet<Motherboard> Motherboard { get; set; }
    public DbSet<PSU> PSU { get; set; }
    public DbSet<RAM> RAM { get; set; }
    public DbSet<ShopRequests> ShopRequests { get; set;}
 
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
      optionsBuilder.UseSqlServer("Server = " + ip + "\\SQLEXPRESS, 1433;" +
          " Database = Computer_house; User ID = root; Password = root; ");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Users>().HasKey(i => i.ID);
      modelBuilder.Entity<Holding_document>().HasKey(i => i.ID);
      modelBuilder.Entity<Locations_in_warehouse>().HasKey(i => i.ID);
      modelBuilder.Entity<Price_list>().HasKey(i => i.Product_ID);
      modelBuilder.Entity<Products_location>().HasKey(k => new { k.Product_ID, k.Location_ID });
      modelBuilder.Entity<Warehouse_info>().HasKey(i => i.Product_ID); 
      modelBuilder.Entity<Case>().HasKey(i => i.ID);
      modelBuilder.Entity<Cooling_system>().HasKey(i => i.ID);
      modelBuilder.Entity<CPU>().HasKey(i => i.ID);
      modelBuilder.Entity<GPU>().HasKey(i => i.ID);
      modelBuilder.Entity<Motherboard>().HasKey(i => i.ID);
      modelBuilder.Entity<PSU>().HasKey(i => i.ID);
      modelBuilder.Entity<RAM>().HasKey(i => i.ID);
      modelBuilder.Entity<Mediator>().HasKey(i => i.ID);
      modelBuilder.Entity<Storage_devices>().HasKey(i => i.ID);
      modelBuilder.Entity<NeedToUpdate>().HasKey(i => i.ID);
      modelBuilder.Entity<ShopRequests>().HasKey(i => i.ID);
    }
  }
}
