using RegExLib.SharedKernel;
using RegExLib.SharedKernel.Interfaces;

namespace RegExLib.Core.Entities
{
  public class Reaction : BaseEntity<int>, IAggregateRoot
  {
    public int ExpressionId { get; private set; }
    public ReactionType ReactionType { get; private set; }
    public DateTime DateLastUpdated { get; private set; } = DateTime.UtcNow;

    public Reaction(int expressionId, ReactionType reactionType)
    {
      ReactionType = reactionType;
      ExpressionId = expressionId;
    }

    public override string ToString() => Enum.GetName(typeof(ReactionType), ReactionType);
  }
}
