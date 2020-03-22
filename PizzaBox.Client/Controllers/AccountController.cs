using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Controllers
{
  public class AccountController : Controller
  {
    public UserRepository _ur = new UserRepository();
    private StoreRepository _sr = new StoreRepository();
    public IActionResult Login()
    {
      if (HttpContext.Session.GetInt32("UserId") != null && (int)HttpContext.Session.GetInt32("UserId")!=0 )
      {
        if (HttpContext.Session.GetString("UserType")== "S")
          return View("StoreMenu");
        else
        {
          return View("UserMenu");
        }
      }
      else
      {
        return View();
      }

    }





    [HttpPost]
    public IActionResult Login(User user)
    {

      User validUser = _ur.GetUserByName(user.UserName, user.Password);
      if (validUser != null)
      {
        if (validUser.Type != user.Type)
        {
          ViewData["error"] = "Invalid Type";
          return View();

        }

        HttpContext.Session.SetInt32("UserId", validUser.UserId);
        HttpContext.Session.SetString("UserName", user.UserName);
        HttpContext.Session.SetString("UserType", user.Type);
        if (user.Type == "S")
        {
          var store = _sr.GetStoreByName(user.UserName);
          HttpContext.Session.SetInt32("StoreId", store.StoreId);
          return View("StoreMenu");
        }
          
        else
        {
          return View("UserMenu");
        }

      }

      else
      {
        ViewData["error"] = "Wrong Username or Password";
        return View();

      }


    }


    public IActionResult Logout()
    {
      HttpContext.Session.SetInt32("UserId", 0);
      HttpContext.Session.SetInt32("StoreId", 0);
      HttpContext.Session.SetString("UserName", "");
      HttpContext.Session.SetString("UserType", "");
      return View("login");
    }


  }
}