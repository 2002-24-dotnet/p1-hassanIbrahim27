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
        
        ViewData["UserID"]=SessionData.UserId;
        return View(new LocationViewModel());
      }

      [HttpPost]
      public IActionResult Location(string Radio)
      {
         var RadioButton = Int32.Parse(Request.Form["Radio"]);
         var store=_sr.GetStoreById(RadioButton);
         SessionData.StoreId=store.StoreId;
         return RedirectToAction("Add","Pizza",store);
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