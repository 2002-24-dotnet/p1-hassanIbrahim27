using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.OrmData.Repositories;
using Microsoft.AspNetCore.Http;
using System;


namespace PizzaBox.Client.Models
{

  public class SalesViewModel
  {
    private PizzaRepository _pr = new PizzaRepository();
    private OrderRepository _or = new OrderRepository();
    public List<Pizza> pizzas = new List<Pizza>();
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public List<Order> storeOrders = new List<Order>();
    public int storeId;

    public SalesViewModel() { }

    public SalesViewModel(int id, DateTime DateStart, DateTime DateEnd)
    {
      pizzas = _pr.GetStorePizzas(id);
      storeOrders = _or.StoreOrdersByDate(id, DateStart, DateEnd);
      storeId = id;
    }

    public int GetQuantity(string pizzaName)
    {
      int qty = 0;
      foreach (var pizza in pizzas)
      {
        if (pizza.Name == pizzaName)
        {
          foreach (var po in pizza.PizzaOrders)
          {
            foreach (var order in storeOrders)
            {
              if (order.OrderId == po.OrderId)
              {
                qty += po.Quantity;
                break;
              }
            }
          }
          break;
        }
      }
      return qty;
    }

    public decimal GetRevenue(string pizzaName)
    {
      decimal revenue = 0;
      foreach (var pizza in pizzas)
      {
        if (pizza.Name == pizzaName)
        {
          foreach (var po in pizza.PizzaOrders)
          {
            foreach (var order in storeOrders)
            {
              if (order.OrderId == po.OrderId)
              {
                revenue += po.Quantity * po.Price;
                break;
              }
            }
          }
          break;
        }
      }
      return revenue;
    }

    public decimal GetTotalRevenue()
    {
      decimal revenue = 0;
      foreach (var pizza in pizzas)
      {
        foreach (var po in pizza.PizzaOrders)
        {
          foreach (var order in storeOrders)
          {
            if (order.OrderId == po.OrderId)
            {
              revenue += po.Quantity * po.Price;
              break;
            }
          }
        }
      }
      return revenue;
    }



  }
}