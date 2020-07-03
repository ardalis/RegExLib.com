using System.ComponentModel.DataAnnotations;
using RegExLib.Core.Entities;

namespace RegExLib.Web.Endpoints.Expressions
{
  // Note: doesn't expose events or behavior
  public class ExpressionDTO
  {
    public int Id { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Pattern { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;


    public static ExpressionDTO FromExpression(Expression expression)
    {
      return new ExpressionDTO()
      {
        Id = expression.Id,
        AuthorId = expression.AuthorId,
        Title = expression.Title,
        Pattern = expression.Pattern,
        Description = expression.Description
      };
    }
  }
}
