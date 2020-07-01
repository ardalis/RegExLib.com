using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.ReactionTests
{
  public class ReactionNew
  {
    [Fact]
    public void InitializationSuccessWithGivenValidData()
    {
      var reaction = new Reaction(1, ReactionType.Like);
      Assert.Equal(1, reaction.ExpressionId);
      Assert.Equal(ReactionType.Like, reaction.ReactionType);
    }
  }
}
