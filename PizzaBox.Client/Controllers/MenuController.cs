using System;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using System.Collections.Generic;
using PizzaBox.Client.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PizzaBox.Client.Helpers;

namespace PizzaBox.Client.Controllers
{
  public class MenuController : Controller
  {
    public StoreRepository _sr = new StoreRepository();
    public IActionResult UserMenu()
    {
      return View("location", new LocationViewModel());
    }

    public IActionResult GoToUserMenu()
    {
      return View("UserMenu");
    }

    public IActionResult GoToStoreMenu()
    {
      return View("StoreMenu");
    }

    [HttpPost]
    public IActionResult Location(string Radio)
    {
      var RadioButton = Int32.Parse(Request.Form["Radio"]);
      var store = _sr.GetStoreById(RadioButton);
      HttpContext.Session.SetInt32("StoreId", store.StoreId);
      int userid = (int)HttpContext.Session.GetInt32("UserId");
      if (SessionHelper.GetObjectFromJson<Dictionary<string, int>>(HttpContext.Session, "cart-"+userid) == null)
      {
        Dictionary<string, int> cart = new Dictionary<string, int>();
        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart-"+userid, cart);
      }
      return View("Add", new OrderViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }


    public IActionResult StoreMenu(User user)
    {
      //Store store = _sr.GetStoreByName(user.UserName);
      // if(store!= null)
      // {
      //   ViewData["location"]=store.Location;
      // }
      return View();
    }
  }

}