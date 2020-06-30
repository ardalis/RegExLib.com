using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.ExpressionTests
{
  public class ExpressionToString
  {

    [Fact]
    public void ToStringReturnTitle()
    {
      var expression = new Expression("Title", "pattern", "description", 1);
      Assert.Equal("Title", expression.ToString());
    }
  }
}
