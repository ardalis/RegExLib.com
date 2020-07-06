using System.ComponentModel.DataAnnotations;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionCommand
  {
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Pattern { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
  }
}
