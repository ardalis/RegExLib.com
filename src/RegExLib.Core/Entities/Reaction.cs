using System;
using System.Collections.Generic;
using System.Text;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
  public enum ReactionType
  {
    Like,
    Dislike
  }

  public class Reaction : BaseEntity
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
