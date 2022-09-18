using System.ComponentModel.DataAnnotations;

namespace RegExLib.BlazorShared.Dtos;
public class ExpressionDto
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

