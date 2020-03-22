using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Controllers
{
  public class UserController : Controller
  {
    
    public IActionResult History()
        {
            string name = HttpContext.Session.GetString("UserName");
            return View(new HistoryViewModel(name));
        }
  }
}
