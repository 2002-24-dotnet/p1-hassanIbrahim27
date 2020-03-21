using System;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using System.Collections.Generic;
using PizzaBox.Client.Models;
using System.Linq;

namespace PizzaBox.Client.Controllers
{
    public class MenuController : Controller
    {
      public StoreRepository _sr = new StoreRepository();
       public IActionResult UserMenu()
      {
        return View("location",new LocationViewModel());
      }

      [HttpPost]
      public IActionResult Location(string Radio)
      {
         var RadioButton = Int32.Parse(Request.Form["Radio"]);
         var store=_sr.GetStoreById(RadioButton);
         SessionData.StoreId=store.StoreId;
         SessionData.cart = new Dictionary<string, int>();
         return View("Add",new OrderViewModel());
      }


       public IActionResult StoreMenu(User user)
      {
        //Store store = _sr.GetStoreByName(user.UserName);
        // if(store!= null)
        // {
        //   ViewData["location"]=store.Location;
        // }
         
        ViewData["UserID"]=SessionData.UserId;
        return View();
      }
    }

}