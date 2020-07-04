using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
  public class AuthorNew
  {
    private readonly string _username = "userAdmin";
    private readonly string _fullName = "Admin Admin";

    [Fact]
    public void InitializationSuccessWithGivenValid()
    {
      var author = new Author(Guid.NewGuid().ToString(), _username, _fullName);
      Assert.Equal("userAdmin", author.Username);
    }

    [Fact]
    public void UserNameRemoveAtWhenGivenAt()
    {
      const string username = "Admin@Admin.com";
      const string expected = "AdminAdmin.com";

      var author = new Author(Guid.NewGuid().ToString(), username, _fullName);
      Assert.Equal(expected, author.Username);
    }

    [Fact]
    public void UserNameWhenNotGivenAt()
    {
      var author = new Author(Guid.NewGuid().ToString(), _username, _fullName);
      Assert.Equal(_username, author.Username);
    }
  }
}
