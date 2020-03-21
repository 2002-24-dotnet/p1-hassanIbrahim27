using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using System.Collections.Generic;


namespace PizzaBox.Client.Controllers
{
    public class OrderController: Controller
    {
      [HttpGet]
       public IActionResult Add()
        {
          ViewData["StoreId"]=SessionData.StoreId;
          return View(new OrderViewModel());
        }

        
        [HttpPost]
         public IActionResult Add(OrderViewModel orderviewmodel)
        {
          int quantity=int.Parse(orderviewmodel.Quantity);
          if(SessionData.cart.ContainsKey(orderviewmodel.PizzaName))
          {
            SessionData.cart[orderviewmodel.PizzaName] += quantity;
          }
          else
          {
             SessionData.cart.Add(orderviewmodel.PizzaName,quantity);
          }
          return View(orderviewmodel);
        }

        public IActionResult RemoveFromCart(string name)
        {
          SessionData.cart.Remove(name);
          return View(new OrderViewModel());
        }
    }
    
}