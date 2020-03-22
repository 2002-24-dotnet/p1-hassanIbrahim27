using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Models
{

  public class UserStoreViewModel
  {
    private PizzaRepository _pr = new PizzaRepository();
    private OrderRepository _or = new OrderRepository();
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string UserName { get; set; }
    public List<Order> orderHistory;

    public UserStoreViewModel(){}

    public UserStoreViewModel(int id,string username)
    {
      UserName=username;
      if(UserName=="")
      {
        orderHistory=new List<Order>();
      }
      else
      {
        orderHistory = _or.UserStoreHistory(UserName, id);
      }
      
    }

    public string GetPizzaName(int pizzaId)
    {
      Pizza pizza = _pr.GetPizzaById(pizzaId);
      return pizza.Name;
    }

  }
}