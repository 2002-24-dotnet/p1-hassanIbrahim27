using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Databases;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace PizzaBox.OrmData.Repositories
{
  public class OrderRepository
  {
     private static readonly PizzaBoxDbContext _db = new PizzaBoxDbContext();
     private static readonly UserRepository _ur = new UserRepository();
     
     private static readonly StoreRepository _sr = new StoreRepository();
     private static readonly PizzaRepository _pr = new PizzaRepository();




     public bool AddOrder(Dictionary<string,int> PizzaOrder, string username, int storeId, decimal tally)
     {
       Order newOrder= new Order();
       newOrder.UserId= _ur.GetUserByName(username).UserId;
       newOrder.StoreId=storeId;
       newOrder.TotalPrice=tally;
       foreach (var pizza in PizzaOrder)
       {
         PizzaOrder newPizzaOrder = new PizzaOrder();
         newPizzaOrder.Quantity=pizza.Value;
         newPizzaOrder.OrderId=newOrder.OrderId;
         Pizza p =_pr.GetPizzaByName(pizza.Key);
         newPizzaOrder.PizzaId=p.PizzaId;
         newPizzaOrder.Price=p.Price;
         newOrder.PizzaOrders.Add(newPizzaOrder);
       }

        _db.Order.Add(newOrder);
        return _db.SaveChanges() == 1;

     }

      public List<Order> UserHistory(string username)
      {
        List<Order> UserOrders = new List<Order>();
        int userId= _ur.GetUserByName(username).UserId;
        List<Order> Order = _db.Order.Include(o =>o.PizzaOrders).ToList();
        foreach (var order in Order)
        {
          
              if(userId== order.UserId)
            {
              UserOrders.Add(order);
            }
          
            
        }

        return UserOrders;
        
      }

       public List<Order> UserStoreHistory(string username,int storeid)
      {
        List<Order> UserStoreOrders = new List<Order>();

        if(_ur.GetUserByName(username) == null) 
        {
          return UserStoreOrders;
        }

        int userId= _ur.GetUserByName(username).UserId;
        List<Order> Order = _db.Order.Include(o =>o.PizzaOrders).ToList();
        foreach (var order in Order)
        {
          
              if(userId== order.UserId && storeid==order.StoreId)
            {
              UserStoreOrders.Add(order);
            }
          
            
        }
        return UserStoreOrders;
      }


        public List<Order> StoreOrders(int id)
      {
        List<Order> StoreOrders = new List<Order>();
        List<Order> Order = _db.Order.Include(o =>o.PizzaOrders).ToList();
        foreach (var order in Order)
        {
          
              if(id== order.StoreId)
            {
              StoreOrders.Add(order);
            }
          
            
        }
        return StoreOrders;
        
        }

         public List<Order> StoreOrdersByDate(int id, DateTime start, DateTime end)
      {
        List<Order> StoreOrders = new List<Order>();
        List<Order> Order = _db.Order.Include(o =>o.PizzaOrders).ToList();
        foreach (var order in Order)
        {
          
              if(id== order.StoreId)
            {
              if(DateTime.Compare(order.Date, start) >= 0 && DateTime.Compare(order.Date, end) <= 0)
              {
                StoreOrders.Add(order);
              }
              
            }
          
            
        }
        return StoreOrders;
        
        }



         public bool LatestOrder(string name)
        {
          List<Order> UserOrders = new List<Order>();
          List<Order> Order = UserHistory(name);
          if (Order.Count == 0)
          {
            return true;
          }
          else
          {
          TimeSpan ts = DateTime.Now - Order.Max(o => o.Date);
          
          
             if(ts.TotalHours > 2)
            {
              return true;
            }
            else
            {
              return false;
            }
          }
           
        }



         public bool LatestStore(string name,int storeid)
        {
          List<Order> UserOrders = new List<Order>();
          List<Order> Order = UserHistory(name);
          if (Order.Count == 0)
          {
            return false;
          }
          else
          {
            foreach (var item in Order)
              {
                if(item.StoreId!=storeid)
                {
                  UserOrders.Add(item);
                }
          }
          if(UserOrders.Count==0)
          {
            return false;
          }
          else
          {

          TimeSpan ts = DateTime.Now - UserOrders.Max(o => o.Date);          
          if(ts.TotalHours > 24)
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

  }
}