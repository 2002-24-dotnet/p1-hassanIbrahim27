using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;


namespace PizzaBox.Client.Controllers
{
  public class UserController : Controller
  {
    public IActionResult History()
        {
            return View();
        }
  }
}
