using System;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class User
    
    {
      
      public string UserName{get; set;}
      public string Password{get; set;}
      public string Type {get; set;}
       public long UserId {get; set;}
      public User()
      {
        UserId = DateTime.Now.Ticks;

      }
        
            
    }
}