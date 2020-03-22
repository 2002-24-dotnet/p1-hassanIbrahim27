using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Models
{

  public class InventoryViewModel
  {

    public static PizzaRepository _pr = new PizzaRepository();
    public List<Pizza> PizzaList { get; set; }

    public int StoreId;
    public InventoryViewModel(int storeid)
    {
      PizzaList = _pr.GetStorePizzas(storeid);
      StoreId=storeid;
      
    }

    public int GetQuantity(string name)
    {
      foreach (var item in PizzaList)
      {
        if(item.Name==name)
        {
          foreach (var piz in item.PizzaStores)
          {
              if(piz.StoreId==StoreId)
              {
                return piz.Quantity;
              }
          }
        }
          
      }
      return -1;
    }
   
  }

}