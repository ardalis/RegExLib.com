using System;
using System.Collections.Generic;
using System.Text;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
    public class AuthorNew
    {
        [Fact]
        public void InitializationThrowsExceptionGivenNullUserId()
        {
            Assert.Throws<ArgumentNullException>(() => new Author(null, "userAdmin", "Admin Admin"));
        }

        [Fact]
        public void InitializationThrowsNullExceptionGivenNullUserName()
        {
            Assert.Throws<ArgumentNullException>(() => new Author(Guid.NewGuid().ToString(), null, "Admin Admin"));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyUserName()
        {
            Assert.Throws<ArgumentException>(() => new Author(Guid.NewGuid().ToString(), "", "Admin Admin"));
        }

        [Fact]
        public void InitializationThrowsNullExceptionGivenNullFullName()
        {
            Assert.Throws<ArgumentNullException>(() => new Author(Guid.NewGuid().ToString(), "userAdmin", null));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyFullName()
        {
            Assert.Throws<ArgumentException>(() => new Author(Guid.NewGuid().ToString(), "userAdmin", ""));
        }

        [Fact]
        public void InitializationSuccessWithGivenValid()
        {
            var author = new Author(Guid.NewGuid().ToString(), "userAdmin", "Admin Admin");
            Assert.Equal("userAdmin", author.Username);
        }

        [Fact]
        public void ToStringReturnFullName()
        {
            var author = new Author(Guid.NewGuid().ToString(), "userAdmin", "Admin Admin");
            Assert.Equal("Admin Admin", author.ToString());
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
