using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;

namespace PizzaBox.Client.Models
{
  public class HistoryViewModel
  {
    private OrderRepository _or = new OrderRepository();
    private PizzaRepository _pr = new PizzaRepository();
    public List<Order> orderHistory = new List<Order>();

    public HistoryViewModel(string name)
    {
      orderHistory = _or.UserHistory(name);
    }

    public string GetPizzaName(int pizzaId)
  {
    Pizza pizza = _pr.GetPizzaById(pizzaId);
    return pizza.Name;
  }
    

  }
}