using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
  public class Expression : BaseEntity
  {
    public int AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Expression()
    {
    }

    public override string ToString() => Title;
  }
}
