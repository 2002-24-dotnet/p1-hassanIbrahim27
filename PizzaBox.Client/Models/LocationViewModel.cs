using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;

namespace PizzaBox.Client.Models
{
  public class LocationViewModel
  {
    private static StoreRepository _sr = new StoreRepository();
    public List<Store> Stores {get;set;}

    public string StoreId {get;set;}
    


    public LocationViewModel()
    {
      Stores = _sr.GetAllStore();
    }
  }
}