using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Databases;

namespace PizzaBox.OrmData.Repositories
{
  public class PizzaRepository
  {

     private static readonly PizzaBoxDbContext _db = new PizzaBoxDbContext();
    public List<Pizza> GetPizza()
    {

      List<Pizza> Pizzas= _db.Pizza.ToList();//eager loading to include all components of Pizza in the navigational properties
      for(int i=0 ; i<Pizzas.Count; i++)
      {
        Pizzas.ElementAt(i).DisplayNumber=i+1;
      }
      return Pizzas;

    }

    public List<Pizza> GetStorePizzas(int id)
    {
      List<Pizza> StorePizzas= new List<Pizza>();
      List<Pizza> Pizzas = _db.Pizza.Include(p => p.PizzaStores).ToList();
      foreach(var pizza in Pizzas)
      {
        foreach(var store in pizza.PizzaStores)
        {
          if(store.StoreId==id)
          {
            StorePizzas.Add(pizza);
            break;
          }
        }
      }
      for(int i=0 ; i<StorePizzas.Count; i++)
      {
        StorePizzas.ElementAt(i).DisplayNumber=i+1;
      }
      return StorePizzas;
    }


    

    public List<Store> GetStore()
    {

      return _db.Store.ToList();
    }


     
     
    public Pizza GetPizzaByName(string name)
    {
      return _db.Pizza.SingleOrDefault(p =>p.Name == name);
      }


      public Pizza GetPizzaById(int id)
    {
      return _db.Pizza.SingleOrDefault(p =>p.PizzaId == id);
      }

     
     public bool UpdatePizzaQuantity(string name ,int qty,int storeId) //update method
    {
      List<Pizza> StorePizzas = GetStorePizzas(storeId);
      foreach (var item in StorePizzas)
      {
        if(item.Name==name)
        {
          foreach (var store in item.PizzaStores)
          {
              if(store.StoreId==storeId)
              {
                store.Quantity= store.Quantity- qty;
                break;
              }
          }
        }
      }
      
       return _db.SaveChanges()==1;
      
    }
  }
  }


