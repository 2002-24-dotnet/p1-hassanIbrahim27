  using System;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Models
{
  public class PizzaStore
  {
    public Pizza Pizza{get;set;}
    public int PizzaId{get; set;}
    public Store Store{get; set;}
    public int StoreId{get;set;}
    public int Quantity{get;set;}
  }
}