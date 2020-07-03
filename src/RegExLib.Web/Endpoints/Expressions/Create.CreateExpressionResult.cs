using AutoMapper;
using RegExLib.Core.Entities;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionResult : CreateExpressionCommand
  {
    public int Id { get; set; }

    public static CreateExpressionResult FromExpression(IMapper mapper, Expression expression)
    {
      var result = new CreateExpressionResult();
      mapper.Map(expression, result);

      return result;
    }
  }
}
