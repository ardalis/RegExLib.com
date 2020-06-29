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
            Assert.Throws<ArgumentNullException>(() => new Expression(null, "pattern", "description", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyTitle()
        {
            Assert.Throws<ArgumentException>(() => new Expression("", "pattern", "description", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenNullPattern()
        {
            Assert.Throws<ArgumentNullException>(() => new Expression("Title", null, "description", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyPattern()
        {
            Assert.Throws<ArgumentException>(() => new Expression("Title", "", "description", 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenNullDescription()
        {
            Assert.Throws<ArgumentNullException>(() => new Expression("Title", "pattern", null, 1));
        }

        [Fact]
        public void InitializationThrowsExceptionGivenEmptyDescription()
        {
            Assert.Throws<ArgumentException>(() => new Expression("Title", "pattern", "", 1));
        }

        [Fact]
        public void InitializationSuccessWithGivenValidData()
        {
            var expression = new Expression("Title", "pattern", "description", 1);
            Assert.Equal("Title", expression.Title);
        }

        [Fact]
        public void ToStringReturnTitle()
        {
            var expression = new Expression("Title", "pattern", "description", 1);
            Assert.Equal("Title", expression.ToString());
        }

    }
}
