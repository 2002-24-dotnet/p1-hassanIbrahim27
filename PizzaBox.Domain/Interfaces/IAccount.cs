namespace PizzaBox.Domain.Interfaces
{
    public interface IAccount
    {
      bool Login(string username, string password);
    }
}