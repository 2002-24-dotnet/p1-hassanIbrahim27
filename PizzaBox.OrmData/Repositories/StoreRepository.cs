using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Databases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.OrmData.Repositories
{
    public class StoreRepository
    {
      private static PizzaBoxDbContext _db = new PizzaBoxDbContext();
      private static PizzaRepository _pr = new PizzaRepository();
   
              
              public Store GetStoreById(int storeid)
              {
                return _db.Store.SingleOrDefault(s =>s.StoreId == storeid);//using p(every record in pizza table) if you find an ID similar to my ID, give it to me
              }

              public Store GetStoreByName(string manager)
              {
                return _db.Store.SingleOrDefault(s =>s.Manager == manager);
              }

              public List<Store> GetAllStore()
            {
              return _db.Store.ToList();
            }

               public bool InventoryUpdate(int pizzaid ,int qty, int storeid) //update method
              {
                List<Pizza> StorePizzas = _pr.GetStorePizzas(storeid);
                foreach (var item in StorePizzas)
                {
                  foreach (var sp in item.PizzaStores)
                  {
                      if(sp.PizzaId==pizzaid && sp.StoreId==storeid)
                  {
                    sp.Quantity=sp.Quantity+qty;
                    return _db.SaveChanges()==1;
                  }
                  }
                  
                }   
                return false;             
              }
  
      }     
    }
