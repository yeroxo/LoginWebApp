
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}

public class Orders
{
    public int id { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; }
}

public class OrderItems
{
    public int id { get; set; }
    public int OrdersId { get; set; }
    public Orders Orders { get; set; }
    public int ItemId { get; set; }      
    public Item Item { get; set; }
    public int Amount { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Orders> Order { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=homework;Trusted_Connection=True;");
    }
}
