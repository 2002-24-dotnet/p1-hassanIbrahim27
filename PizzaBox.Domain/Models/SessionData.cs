using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public class SessionData
    {
      public static int UserId{get;set;}
      public static int StoreId{get;set;}
      public static Dictionary<string,int> pizzaorder{get;set;}
    }
}