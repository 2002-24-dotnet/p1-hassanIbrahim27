using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Client.Controllers
{
  public class AddPizza : Controller
  {
    public PizzaRepository _pr = new PizzaRepository();
    public IActionResult Add(Store store)
    {
      List<Pizza> lp = new List<Pizza>();
      lp=_pr.GetStorePizzas(store.StoreId);
      ViewData["pizzas"]=lp;
      ViewData["storeid"]=store.StoreId;
      return View();
    }
  }
}