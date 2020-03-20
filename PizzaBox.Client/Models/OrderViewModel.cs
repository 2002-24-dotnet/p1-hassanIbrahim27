using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;

namespace PizzaBox.Client.Models
{
  
  public class OrderViewModel
  {

    public PizzaRepository _pr = new PizzaRepository();
    public List<Pizza> PizzaList{ get; set; }



    public string pizza{ get; set; }
    public int Quantity{get;set;}

    public OrderViewModel()
    {
      PizzaList =  _pr.GetPizza();
    }

  }

}