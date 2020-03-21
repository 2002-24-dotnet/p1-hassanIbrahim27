using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Controllers
{
    public class AccountController: Controller
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
            
            HttpContext.Session.SetInt32("UserId",validUser.UserId);
            HttpContext.Session.SetString("UserName",user.UserName);
            if(user.Type=="S")
             return View("Store");
            else
              {
                return View("UserMenu");
              }
              
          }
          
          else
          {
            ViewData["error"]="Wrong Username or Password";
            return View();

          }
          
          
        }


        public IActionResult Logout()
        {
          return View("login");
        }


    }
}