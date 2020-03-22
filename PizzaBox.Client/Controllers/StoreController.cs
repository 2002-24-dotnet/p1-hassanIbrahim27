using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using PizzaBox.Client.Models;
using Microsoft.AspNetCore.Http;
using System;


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
      if(updateinvmodel.Type=="A")
      {
        updateinvmodel.Quantity *= -1;
      }

      bool suc = _pr.UpdatePizzaQuantity(updateinvmodel.PizzaName,updateinvmodel.Quantity,(int)HttpContext.Session.GetInt32("StoreId"));
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

    public IActionResult Sales()
    {
      return View("Sales", new SalesViewModel());
    }

    public IActionResult ViewSales(SalesViewModel salesViewModel)
    {
      DateTime DateStart= DateTime.Parse(salesViewModel.StartDate);
      DateTime DateEnd=DateTime.Parse(salesViewModel.EndDate);
      TimeSpan ts = new TimeSpan(23, 59, 59);
      DateEnd= DateEnd.Date + ts;
      ViewData["SalesView"]="Sales Between " + salesViewModel.StartDate + " And " +salesViewModel.EndDate;
      var model = new SalesViewModel((int)HttpContext.Session.GetInt32("StoreId"),DateStart,DateEnd);
      ViewData["Revenue"]="With Total Revenue $" + model.GetTotalRevenue();
      return View("Sales", model);
    }

    public IActionResult UserStoreHistory()
    {
      return View("UserStoreHistory", new UserStoreViewModel((int)HttpContext.Session.GetInt32("StoreId"),""));
    }

    [HttpPost]
    public IActionResult UserStoreHistory(UserStoreViewModel userStoreHistory)
    {
      ViewData["UserHistory"]="Viewing Order History For " + @userStoreHistory.UserName +" between "+ userStoreHistory.StartDate + " and " + userStoreHistory.EndDate;
      return View("UserStoreHistory", new UserStoreViewModel((int)HttpContext.Session.GetInt32("StoreId"),userStoreHistory.UserName));
    }
  }
}
