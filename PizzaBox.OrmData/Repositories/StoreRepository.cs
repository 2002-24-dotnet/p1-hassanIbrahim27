using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.OrmData.Repositories
{
    public class StoreRepository
    {
      private static readonly PizzaBoxDbContext _db = new PizzaBoxDbContext();
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
      }     
    }
