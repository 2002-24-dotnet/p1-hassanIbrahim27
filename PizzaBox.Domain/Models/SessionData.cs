using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public class SessionData
    {
      public static int UserId{get;set;}
      public static string UserName{get;set;}
      public static int StoreId{get;set;}
      public static Dictionary<string,int> cart = new Dictionary<string,int>();
    }

    
}