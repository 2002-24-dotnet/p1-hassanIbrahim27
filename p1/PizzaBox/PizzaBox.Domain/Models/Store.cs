using System;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Domain.Models
{
    public class Store
    {

      
      public int StoreId {get; set;}
      public string Location{get; set;}
      public string Manager{get; set;}
      public List<PizzaStore> PizzaStores{get;set;}
      
    }
}