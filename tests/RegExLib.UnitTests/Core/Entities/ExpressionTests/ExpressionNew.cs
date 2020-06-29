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
