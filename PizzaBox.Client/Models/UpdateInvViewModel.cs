using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using Microsoft.AspNetCore.Http;


namespace PizzaBox.Client.Models
{

  public class UpdateInvViewModel
  {

    public static PizzaRepository _pr = new PizzaRepository();
    public List<Pizza> PizzaList { get; set; }

    public int Quantity{get;set;}
    public string PizzaName{get;set;}

    public int StoreId;
    public UpdateInvViewModel(int storeid)
    {
      PizzaList = _pr.GetStorePizzas(storeid);
      StoreId=storeid;
      
    }

    public UpdateInvViewModel(){}

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