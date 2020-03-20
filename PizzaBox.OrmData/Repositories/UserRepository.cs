using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Databases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.OrmData.Repositories
{
    public class UserRepository
    {
      private static PizzaBoxDbContext _db = new PizzaBoxDbContext();
      private static readonly OrderRepository _or= new OrderRepository();
     
           
            public List<User> GetUser()
            {
              return _db.User.ToList();
            }

              public User GetUserByName(string name, string password)
              {
                return _db.User.SingleOrDefault(u =>(u.UserName== name && u.Password==password) );//using p(every record in pizza table) if you find an ID similar to my ID, give it to me
              }

              public User GetUserByName(string name)
              {
                return _db.User.SingleOrDefault(u =>u.UserName == name);//using p(every record in pizza table) if you find an ID similar to my ID, give it to me
              }

             
          
      }     
    }
