using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
  public class AuthorAddExpression
  {
    private readonly string _username;
    private readonly string _fullName;
    private readonly string _title;
    private readonly string _pattern;
    private readonly string _description;
    private readonly int _authorId;

    public AuthorAddExpression()
    {
      _username = "userAdmin";
      _fullName = "Admin Admin";
      _title = "title";
      _pattern = "pattern";
      _description = "description";
      _authorId = 1;
    }

    [Fact]
    public void AddExpressionSuccessWithGivenValidExpression()
    {
      var author = new Author(Guid.NewGuid().ToString(), _username, _fullName);
      var expression = new Expression
      {
        Title = _title,
        Pattern = _pattern,
        Description = _description,
        AuthorId = _authorId
      };
      author.AddExpression(expression);
      Assert.Contains(author.Expressions, x => x == expression);
    }
  }
}
