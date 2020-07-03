using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.ExpressionTests
{
  public class ExpressionNew
  {
    private readonly string _title;
    private readonly string _pattern;
    private readonly string _description;
    private readonly int _authorId;

    public ExpressionNew()
    {
      _title = "title";
      _pattern = "pattern";
      _description = "description";
      _authorId = 1;
    }

    [Fact]
    public void InitializationSuccessWithGivenValidData()
    {
      var expression = new Expression
      { 
        Title = _title,
        Pattern = _pattern,
        Description = _description,
        AuthorId = _authorId

      };
      const string expected = "title";

      Assert.Equal(expected, expression.Title);
    }
  }
}
