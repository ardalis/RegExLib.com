using System;
using System.Collections.Generic;
using System.Text;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.ExpressionTests
{
    public class ExpressionNew
    {
        [Fact]
        public void InitializationThrowsExceptionGivenNullTitle()
        {
            Assert.Throws<ArgumentNullException>(() => new Expression(null, "userAdmin", "Admin Admin", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyTitle()
        {
            Assert.Throws<ArgumentException>(() => new Expression("", "userAdmin", "Admin Admin", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenNullPattern()
        {
            Assert.Throws<ArgumentNullException>(() => new Expression("Title", null, "Admin Admin", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyPattern()
        {
            Assert.Throws<ArgumentException>(() => new Expression("Title", "", "Admin Admin", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenNullDescription()
        {
            Assert.Throws<ArgumentNullException>(() => new Expression("Title", "userAdmin", null, 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyDescription()
        {
            Assert.Throws<ArgumentException>(() => new Expression("Title", "userAdmin", "", 1));
        }

        [Fact]
        public void InitializationSuccessWithGivenValidData()
        {
            var expression = new Expression("Title", "userAdmin", "Admin Admin", 1);
            Assert.Equal("Title", expression.Title);
        }

        [Fact]
        public void ToStringReturnTitle()
        {
            var expression = new Expression("Title", "userAdmin", "Admin Admin", 1);
            Assert.Equal("Title", expression.ToString());
        }

    }
}
