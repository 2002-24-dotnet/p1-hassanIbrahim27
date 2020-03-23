using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Models
{

  public class OrderViewModel
  {

    public static PizzaRepository _pr = new PizzaRepository();
    public List<Pizza> PizzaList { get; set; }

    public int StoreId{get;set;}

    public string PizzaName { get; set; }
    public string Quantity { get; set; }
    public decimal cartTotal;

    public OrderViewModel(int storeid)
    {
      StoreId=storeid;
      PizzaList = _pr.GetStorePizzas(StoreId);

    }
    public OrderViewModel(){}
    public string CartString(string name, int qty)
    {
      decimal singleprice = _pr.GetPizzaPrice(name);
      decimal totalprice = singleprice * qty;
      cartTotal+=totalprice;
      return name + " | " + qty + " x $" + singleprice + " = $" + totalprice;
    }

    


  }

}