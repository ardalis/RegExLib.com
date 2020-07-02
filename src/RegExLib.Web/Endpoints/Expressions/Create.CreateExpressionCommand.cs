using System.ComponentModel.DataAnnotations;
using RegExLib.Web.ApiModels;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionCommand
  {
    [Required]
    public ExpressionDTO ExpressionDto { get; set; } = new ExpressionDTO();

  }
}
