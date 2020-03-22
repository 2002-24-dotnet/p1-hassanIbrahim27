  using System;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Models
{
  public class PizzaOrder
  {
    public Pizza Pizza{get;set;}
    public int PizzaId{get; set;}
    public Order Order{get; set;}
    public long OrderId{get;set;}
    public int Quantity{get;set;}
    public decimal Price {get;set;}


  }

  
}