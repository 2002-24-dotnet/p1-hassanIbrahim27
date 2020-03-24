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
      int userid = (int)HttpContext.Session.GetInt32("UserId");

      Dictionary<string, int> cart = SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart-" + userid);
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

      SessionHelper.SetObjectAsJson(HttpContext.Session, "cart-" + userid, cart);
      return View(new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }


    public IActionResult RemoveFromCart(string name)
    {
      int userid = (int)HttpContext.Session.GetInt32("UserId");
      Dictionary<string, int> cart = SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart-" + userid);
      cart.Remove(name);
      SessionHelper.SetObjectAsJson(HttpContext.Session, "cart-" + userid, cart);
      return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }

    public IActionResult CheckOut()
    {
      int userid = (int)HttpContext.Session.GetInt32("UserId");
      int storeid = (int)HttpContext.Session.GetInt32("StoreId");
      Dictionary<string, int> cart = SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart-" + userid);
      if (cart.Count == 0)
      {
        ViewData["CheckOutError"] = "Cart is Empty!";
        return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
      }
      foreach (var item in cart)
      {
        GrandTotal += item.Value * _pr.GetPizzaPrice(item.Key);
      }
      if (GrandTotal > 250)
      {
        ViewData["CheckOutError"] = "Your total can't exceed $250";
        return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
      }
      else
      {
        bool check = _or.LatestOrder(HttpContext.Session.GetString("UserName"));
        if (check == true)
        {
          _or.AddOrder(cart, HttpContext.Session.GetString("UserName"), (int)HttpContext.Session.GetInt32("StoreId"), GrandTotal);
          ViewData["OrderStatus"] = "Order Submitted Successfully";
          foreach (var item in cart)
          {
            _pr.UpdatePizzaQuantity(item.Key, item.Value, storeid);
          }

          SessionHelper.SetObjectAsJson(HttpContext.Session, "cart-" + userid, new Dictionary<string, int>());
          return View("UserMenu");
        }
        else
        {
          ViewData["OrderError"] = "Sorry you cant make more than one order within 2 hours!";
          return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
        }
      }

    }
  }

}