using RegExLib.Core.Entities;
using Xunit;
using Xunit.Sdk;

namespace RegExLib.UnitTests.Core.Entities.ExpressionTests
{
  public class ExpressionToString
  {
    private readonly string _title;
    private readonly string _pattern;
    private readonly string _description;
    private readonly int _authorId;

    public ExpressionToString()
    {
      _title = "title";
      _pattern = "pattern";
      _description = "description";
      _authorId = 1;
    }

    [Fact]
    public void ToStringReturnTitle()
    {
      var expression = new Expression
      {
        Title = _title,
        Pattern = _pattern,
        Description = _description,
        AuthorId = _authorId

      };
      const string expected = "title";

      Assert.Equal(expected, expression.ToString());
    }
  }
}
