using System.ComponentModel.DataAnnotations;

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

  }
}
