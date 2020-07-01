using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.ReactionTests
{
  public class ReactionToString
  {

    [Fact]
    public void ToStringReturnTitle()
    {
      var reaction = new Reaction(1, ReactionType.Like);
      Assert.Equal("Like", reaction.ToString());
    }
  }
}
