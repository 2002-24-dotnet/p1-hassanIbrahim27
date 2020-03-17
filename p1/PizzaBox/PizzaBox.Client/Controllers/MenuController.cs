using System;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Client.Controllers
{
    public class MenuController : Controller
    {
      public StoreRepository _sr = new StoreRepository();
      

       public IActionResult UserMenu()
      {
        List<Store> stores= _sr.GetAllStore();
        ViewData["stores"]=stores;
        return View();
      }

      [HttpPost]
      public IActionResult Location(string Radio)
      {
         var RadioButton = Int32.Parse(Request.Form["Radio"]);
         var store=_sr.GetStoreById(RadioButton);
         return RedirectToAction("Add","AddPizza",store);
      }


       public IActionResult StoreMenu(User user)
      {
        Store store = _sr.GetStoreByName(user.UserName);
        if(store!= null)
        {
          ViewData["location"]=store.Location;
        }
        return View();
      }
    }

}