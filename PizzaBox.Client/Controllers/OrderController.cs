using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using System.Collections.Generic;


namespace PizzaBox.Client.Controllers
{
  public class OrderController : Controller
  {
    private PizzaRepository _pr = new PizzaRepository();

    [HttpGet]
    public IActionResult Add()
    {
      ViewData["StoreId"] = SessionData.StoreId;
      return View(new OrderViewModel());
    }


    [HttpPost]
    public IActionResult Add(OrderViewModel orderviewmodel)
    {
      int totalqty = 0;
      foreach (var item in SessionData.cart)
      {
        totalqty += item.Value;
      }
      int quantity = int.Parse(orderviewmodel.Quantity);
      if (totalqty + quantity > 50)
      {
        ViewData["QtyError"] = "Can't exceed 50 Pizzas";
      }
      else
      {
        if (SessionData.cart.ContainsKey(orderviewmodel.PizzaName))
        {
          SessionData.cart[orderviewmodel.PizzaName] += quantity;
        }
        else
        {
          SessionData.cart.Add(orderviewmodel.PizzaName, quantity);
        }
      }

      return View(orderviewmodel);
    }


    public IActionResult RemoveFromCart(string name)
    {
      SessionData.cart.Remove(name);
      return View("Add", new OrderViewModel());
    }

    public IActionResult CheckOut()
    {
      decimal GrandTotal=0;
      foreach (var item in SessionData.cart)
      {
        GrandTotal += item.Value * _pr.GetPizzaPrice(item.Key);
      }
      if(GrandTotal>250)
      {
        ViewData["CheckOutError"]="Your total can't exceed $250";
      }

      return View("Add", new OrderViewModel());
    }
  }

}