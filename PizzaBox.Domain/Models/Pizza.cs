using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Domain.Models
{
    public class Pizza
    {
      public int PizzaId {get; set;}
      public decimal Price{get; set;}
      public string Name{get; set;}
      
     
      
      public int DisplayNumber;
      
      // public Pizza()
      // {
      //   PizzaId=DateTime.Now.Ticks;
      // }

      #region NAVIGATIONAL PROPERTIES
      // public User User{get; set;}
      // public Store Store {get; set;}
      // public Order Order {get; set;}

      public List<PizzaOrder> PizzaOrders{get;set;}
      public List<PizzaStore> PizzaStores{get;set;}
      #endregion


      public int GetStoreQuantity(int storeId)
      {
        foreach (var item in PizzaStores)
        {
            if(storeId==item.StoreId)
            {
              return item.Quantity;
            }
        }
        return -1;
      }

      public string GetNote(int storeId)
      {
        if(GetStoreQuantity(storeId)==0)
        {
          return "This item is out of stock";
        }
        else
        {
          return "";
        }
      }

      public bool IsAvailable(int storeId)
      {
        if(GetStoreQuantity(storeId)==0)
        {
          return false;
        }
        else
        {
          return true;
        }
      }


    }

}