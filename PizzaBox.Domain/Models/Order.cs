using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Domain.Models
{
  public class Order
  {
    public long OrderId {get; set;}
    public User User{get; set;}
    public int UserId{get; set;}
    public int StoreId{get;set;}
    public Store Store{get; set;}
    public decimal TotalPrice{get;set;}
    public DateTime Date{get;set;}
    public List<PizzaOrder> PizzaOrders{get;set;}
  

    public Order()
    {
     // OrderId=DateTime.Now.Ticks;
      Date= DateTime.Now;
      PizzaOrders=new List<PizzaOrder>();
      
    }

  
  }
}
