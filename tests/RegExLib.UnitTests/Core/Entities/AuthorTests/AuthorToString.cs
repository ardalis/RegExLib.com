using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
  public class AuthorToString
  {
    [Fact]
    public void ToStringReturnFullName()
    {
      var author = new Author(Guid.NewGuid().ToString(), "Admin", "Admin Admin");
      Assert.Equal("Admin Admin", author.ToString());
    }
  }
}
