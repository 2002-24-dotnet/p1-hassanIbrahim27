using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Controllers
{
  public class StoreController : Controller
  {
    private StoreRepository _sr = new StoreRepository();
    private PizzaRepository _pr = new PizzaRepository();

    public IActionResult Inventory()
    {
      return View("Inventory", new InventoryViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }

    public IActionResult UpdateInventory()
    {
      return View("UpdateInv", new UpdateInvViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }

    [HttpPost]
    public IActionResult UpdateInventory(UpdateInvViewModel updateinvmodel)
    {
      Pizza pizza=_pr.GetPizzaByName(updateinvmodel.PizzaName);
      int pizzaid=pizza.PizzaId;
      ViewData["pizza"]=pizzaid;
      ViewData["Quantity"]=updateinvmodel.Quantity;
      ViewData["storeid"]=(int)HttpContext.Session.GetInt32("StoreId");
      bool suc = _sr.InventoryUpdate(pizzaid ,updateinvmodel.Quantity,(int)HttpContext.Session.GetInt32("StoreId"));
      if(suc)
      {
        ViewData["update"]="Quantity Updated";
      }
      else
      {
        ViewData["Error"]="Update Error";
      }
      
      return View("UpdateInv", new UpdateInvViewModel((int)HttpContext.Session.GetInt32("StoreId")));
    }

  }
}
