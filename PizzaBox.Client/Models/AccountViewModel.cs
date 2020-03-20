using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;

namespace PizzaBox.Client.Models
{
  public class AccountViewModel
  {
    public int UserId {get;set;}
    public string UserName{get;set;}
    public string Password { get; set; }
    public string Type {get;set;}

    
  }
}