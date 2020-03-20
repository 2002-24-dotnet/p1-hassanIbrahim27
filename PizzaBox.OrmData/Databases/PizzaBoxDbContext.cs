using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;

namespace PizzaBox.OrmData.Databases
{
    public class PizzaBoxDbContext : DbContext
    {
      public DbSet<Pizza> Pizza{get; set; }
      public DbSet<User> User{get; set; }
      public DbSet<Store> Store{get; set; }
      public DbSet<Order> Order{get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer("server=localhost;database=pizzaboxdb;user id=sa;password=Password12345;"); //database location   
    }

     protected override void OnModelCreating(ModelBuilder builder)//convert c# obj to sql
      {
        builder.Entity<Pizza>().HasKey(p => p.PizzaId);//to make sure that every obj has a unique ID
        builder.Entity<User>().HasKey(u => u.UserId);
        builder.Entity<Store>().HasKey(s => s.StoreId);
        builder.Entity<Order>().HasKey(o => o.OrderId);
        builder.Entity<PizzaOrder>().HasKey(po => new { po.PizzaId, po.OrderId });
        builder.Entity<PizzaStore>().HasKey(ps => new { ps.PizzaId, ps.StoreId });

        // builder.Entity<User>().HasMany<Pizza>().WithOne(p => p.User);
        // builder.Entity<Store>().HasMany<Pizza>().WithOne(p => p.Store);
        // builder.Entity<Order>().HasMany<Pizza>().WithOne(p => p.Order);


        builder.Entity<Pizza>().HasMany(p => p.PizzaOrders).WithOne(po => po.Pizza).HasForeignKey(po => po.PizzaId);
        builder.Entity<Order>().HasMany(t => t.PizzaOrders).WithOne(po => po.Order).HasForeignKey(po => po.OrderId);
        builder.Entity<Pizza>().HasMany(p => p.PizzaStores).WithOne(ps => ps.Pizza).HasForeignKey(ps => ps.PizzaId);
        builder.Entity<Store>().HasMany(t => t.PizzaStores).WithOne(ps => ps.Store).HasForeignKey(ps => ps.StoreId);


        builder.Entity<User>().HasData(new User[]
        {
          new User(){UserId=1 , UserName = "user1", Password="123", Type="U"},
          new User(){UserId=2, UserName = "user2", Password="234", Type="U"},
          new User(){UserId=3, UserName = "user3", Password="345", Type="U"},
          new User(){UserId=4, UserName = "user4", Password="456", Type="U"},
          new User(){UserId=5, UserName = "store1", Password="123", Type="S"},
          new User(){UserId=6, UserName = "store2", Password="234", Type="S"}
        });

        builder.Entity<Store>().HasData(new Store[]
        {
          new Store(){StoreId=1, Location="Down Town", Manager="Store1"},
          new Store(){StoreId=2, Location="City",Manager="Store2"},
        });

        builder.Entity<Pizza>().HasData(new Pizza[]
        {
          new Pizza(){PizzaId=1, Name = "Vege", Price=10.99M},
          new Pizza(){PizzaId=2, Name = "Cheese", Price=11.99M},
          new Pizza(){PizzaId=3, Name = "Peppe", Price=9.99M},
          new Pizza(){PizzaId=4, Name = "Vegan", Price=12.99M},
          new Pizza(){PizzaId=5, Name = "Sushi", Price=12.99M},
          new Pizza(){PizzaId=6, Name = "Mini", Price=3.99M}
        });

         builder.Entity<PizzaStore>().HasData(new PizzaStore[]
         {
           new PizzaStore(){PizzaId=1, StoreId=1, Quantity=100},
           new PizzaStore(){PizzaId=2, StoreId=1, Quantity=100},
           new PizzaStore(){PizzaId=3, StoreId=1, Quantity=0},
           new PizzaStore(){PizzaId=4, StoreId=1, Quantity=100},
           new PizzaStore(){PizzaId=5, StoreId=1, Quantity=100},
           new PizzaStore(){PizzaId=6, StoreId=1, Quantity=100},
           new PizzaStore(){PizzaId=1, StoreId=2, Quantity=100},
           new PizzaStore(){PizzaId=2, StoreId=2, Quantity=100},
           new PizzaStore(){PizzaId=3, StoreId=2, Quantity=100},
           new PizzaStore(){PizzaId=4, StoreId=2, Quantity=100},
           new PizzaStore(){PizzaId=5, StoreId=2, Quantity=0},
           new PizzaStore(){PizzaId=6, StoreId=2, Quantity=100},

         });

    }

}
}

