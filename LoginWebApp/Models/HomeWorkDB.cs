
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class EventItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Start { get ; set; }
    public DateTime End { get; set; }
    public bool Status { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserEvents
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int EventItemId { get; set; }      
    public EventItem EventItem { get; set; }
}



public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<EventItem> EventItem { get; set; }
    public DbSet<UserEvents> UserEvents { get; set; }

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
