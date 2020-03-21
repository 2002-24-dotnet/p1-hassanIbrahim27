using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PizzaBox.Client.Helpers;


namespace PizzaBox.Client.Controllers
{
  public class OrderController : Controller
  {
    public decimal GrandTotal = 0;
    private PizzaRepository _pr = new PizzaRepository();
    private OrderRepository _or = new OrderRepository();

    [HttpGet]
    public IActionResult Add()
    {
      ViewData["StoreId"] = HttpContext.Session.GetInt32("StoreId");
      return View(new OrderViewModel((int)ViewData["StoreId"]));
    }


    [HttpPost]
    public IActionResult Add(OrderViewModel orderviewmodel)
    {
      int quantity = int.Parse(orderviewmodel.Quantity);
      if (SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart") == null)
      {
        Dictionary<string, int> cart = new Dictionary<string, int>();
        cart.Add(orderviewmodel.PizzaName, quantity);
        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      }
      else
      {
        Dictionary<string, int> cart = SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart");
        int totalqty = 0;
        foreach (var item in cart)
        {
          totalqty += item.Value;
        }
        
        if (totalqty + quantity > 50)
        {
          ViewData["QtyError"] = "Can't exceed 50 Pizzas";
        }
        else
        {
          if (cart.ContainsKey(orderviewmodel.PizzaName))
          {
            cart[orderviewmodel.PizzaName] += quantity;
          }
          else
          {
            cart.Add(orderviewmodel.PizzaName, quantity);
          }
        }
        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      }


      return View(new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }


    public IActionResult RemoveFromCart(string name)
    {

      Dictionary<string, int> cart = SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart");
      cart.Remove(name);
      SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }

    public IActionResult CheckOut()
    {
      Dictionary<string, int> cart = SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart");

      foreach (var item in cart)
      {
        GrandTotal += item.Value * _pr.GetPizzaPrice(item.Key);
      }
      if (GrandTotal > 250)
      {
        ViewData["CheckOutError"] = "Your total can't exceed $250";
      }
      else
      {
        _or.AddOrder(cart, HttpContext.Session.GetString("UserName"), (int)HttpContext.Session.GetInt32("StoreId"), GrandTotal);
        ViewData["OrderStatus"] = "Order Submit";
        return View("UserMenu");
      }
      SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", new Dictionary<string,int>());
      return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }
  }

}