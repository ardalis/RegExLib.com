using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.ExpressionTests
{
  public class ExpressionNew
  {
    [Fact]
    public void InitializationSuccessWithGivenValidData()
    {
      var expression = new Expression
      { 
        Title = "Title",
        Pattern = "pattern",
        Description = "description",
        AuthorId = 1

      };
      Assert.Equal("Title", expression.Title);
    }
  }
}
