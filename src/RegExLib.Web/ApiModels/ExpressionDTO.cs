using System.ComponentModel.DataAnnotations;
using RegExLib.Core.Entities;

namespace RegExLib.Web.ApiModels
{
  // Note: doesn't expose events or behavior
  public class ExpressionDTO
  {
    public int Id { get; set; }
#nullable disable
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Pattern { get; set; }
    [Required]
    public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
