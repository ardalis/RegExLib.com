using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
    public class AuthorNew
    {

        [Fact]
        public void InitializationSuccessWithGivenValid()
        {
            var author = new Author(Guid.NewGuid().ToString(), "userAdmin", "Admin Admin");
            Assert.Equal("userAdmin", author.Username);
        }

        [Fact]
        public void UserNameRemoveAtWhenGivenAt()
        {
            var author = new Author(Guid.NewGuid().ToString(), "Admin@Admin.com", "Admin Admin");
            Assert.Equal("AdminAdmin.com", author.Username);
        }

        [Fact]
        public void UserNameWhenNotGivenAt()
        {
            var author = new Author(Guid.NewGuid().ToString(), "Admin", "Admin Admin");
            Assert.Equal("Admin", author.Username);
        }
    }
}
