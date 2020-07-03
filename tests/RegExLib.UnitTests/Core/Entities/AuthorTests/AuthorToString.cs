using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
  public class AuthorToString
  {
    private readonly string _username;
    private readonly string _fullName;

    public AuthorToString()
    {
      _username = "userAdmin";
      _fullName = "Admin Admin";
    }

    [Fact]
    public void ToStringReturnFullName()
    {
      const string expected = "Admin Admin";

      var author = new Author(Guid.NewGuid().ToString(), _username, _fullName);
      Assert.Equal(expected, author.ToString());
    }
  }
}
