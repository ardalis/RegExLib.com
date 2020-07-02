using RegExLib.Web.ApiModels;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionResult : CreateExpressionCommand
  {
    public ExpressionDTO Expression { get; }

    public CreateExpressionResult(ExpressionDTO expression)
    {
      Expression = expression;
    }
  }
}
