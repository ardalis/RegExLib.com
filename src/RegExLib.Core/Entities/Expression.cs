using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
  public class Expression : BaseEntity
  {
    public int AuthorId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Pattern { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public Expression()
    {
    }

    public override string ToString() => Title;
  }
}
