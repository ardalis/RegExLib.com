using RegExLib.SharedKernel;
using RegExLib.SharedKernel.Interfaces;

namespace RegExLib.Core.Entities
{
  public class Expression : BaseEntity<int>, IAggregateRoot
  {
    public int? AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual Author? Author { get; set; }

    public Expression()
    {
    }

    public override string ToString() => Title;
  }
}
