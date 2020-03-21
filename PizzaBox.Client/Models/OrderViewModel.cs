using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;

namespace PizzaBox.Client.Models
{

  public class OrderViewModel
  {

    public static PizzaRepository _pr = new PizzaRepository();
    public List<Pizza> PizzaList { get; set; }



    public string PizzaName { get; set; }
    public string Quantity { get; set; }
    //public Dictionary<string,int> cart = new Dictionary<string,int>();

    public OrderViewModel()
    {
      PizzaList = _pr.GetStorePizzas(SessionData.StoreId);

    }
    public string CartString(string name, int qty)
    {
      decimal singleprice = _pr.GetPizzaPrice(name);
      decimal totalprice = singleprice * qty;
      return name + " | " + qty + " x $" + singleprice + " = $" + totalprice;
    }

    


  }

}