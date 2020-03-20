using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;


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
          //string name = pizzaViewModel.pizza.Name;
          int quantity=(int)(orderviewmodel.Quantity);
          // SessionData.pizzaorder.Add("aaa",quantity);
           ViewData["piz"]=quantity;
          return View();
        }
    }
    
}