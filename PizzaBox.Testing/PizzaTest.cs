using PizzaBox.Storage.Repositories;
using Xunit;

namespace PizzaBox.Testing.Specs
{
  public class PizzaTests
  {
    [Fact]
    public void Test_RepositoryGet()
    {
      var sut = new PizzaRepository();
      var actual = sut.GetStorePizzas(1);

      Assert.True(actual != null);
      Assert.True(actual.Count >= 0);
    }
  }
}