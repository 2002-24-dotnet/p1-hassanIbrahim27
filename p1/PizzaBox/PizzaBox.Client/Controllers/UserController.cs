using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;

namespace PizzaBox.Client.Controllers
{
    public class UserController: Controller
    {
      public UserRepository _ur = new UserRepository();
       public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(User user)
        {
          
          User validUser=_ur.GetUserByName(user.UserName,user.Password);
          if(validUser != null)
          {
            if(validUser.Type!=user.Type)
            {
            ViewData["error"]="Invalid Type";
            return View();

            }

            if(user.Type=="S")
             return RedirectToAction("StoreMenu","Menu",user);
            else
              return RedirectToAction("UserMenu","Menu",user);
          }
          
          else
          {
            ViewData["error"]="Wrong Username or Password";
            return View();

          }
          
          
        }


    }
}