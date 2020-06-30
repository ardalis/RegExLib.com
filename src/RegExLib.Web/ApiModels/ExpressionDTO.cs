using System.ComponentModel.DataAnnotations;
using RegExLib.Core.Entities;

namespace RegExLib.Web.ApiModels
{
  // Note: doesn't expose events or behavior
  public class ExpressionDTO
  {
    public int Id { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Title { get; private set; }
    [Required]
    public string Pattern { get; private set; }
    [Required]
    public string Description { get; private set; }

    public ExpressionDTO(int id, int authorId, string title, string pattern, string description)
    {
      Id = id;
      AuthorId = authorId;
      Title = title;
      Pattern = pattern;
      Description = description;
    }

    public static ExpressionDTO FromExpression(Expression expression)
    {
      return new ExpressionDTO(expression.Id, expression.AuthorId, expression.Title, expression.Pattern, expression.Description);
    }
  }
}
