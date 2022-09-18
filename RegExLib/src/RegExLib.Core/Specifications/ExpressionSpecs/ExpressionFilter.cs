using RegExLib.Core.Specifications.Helpers;

namespace RegExLib.Core.Specifications;
public class ExpressionFilter : BaseFilter
{
  public int? Id { get; set; }
  public int? AuthorId { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
}

