using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
  public class AuthorAddExpression
  {
    [Fact]
    public void AddExpressionSuccessWithGivenValidExpression()
    {
      var author = new Author(Guid.NewGuid().ToString(), "userAdmin", "Admin Admin");
      var expression = new Expression
      {
        Title = "Title",
        Pattern = "pattern",
        Description = "description",
        AuthorId = 1
      };
      author.AddExpression(expression);
      Assert.Contains(author.Expressions, x => x == expression);
    }
  }
}
