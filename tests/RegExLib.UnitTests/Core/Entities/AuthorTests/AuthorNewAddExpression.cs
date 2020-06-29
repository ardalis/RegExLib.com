using System;
using RegExLib.Core.Entities;
using Xunit;

namespace RegExLib.UnitTests.Core.Entities.AuthorTests
{
    public class AuthorNewAddExpression
    {
        [Fact]
        public void AddExpressionThrowsExceptionGivenNullExpression()
        {
            var author = new Author(Guid.NewGuid().ToString(), "userAdmin", "Admin Admin");
            Assert.Throws<ArgumentNullException>(() => author.AddExpression(null!));
        }

        [Fact]
        public void AddExpressionSuccessWithGivenValidExpression()
        {
            var author = new Author(Guid.NewGuid().ToString(), "userAdmin", "Admin Admin");
            var expression = new Expression("title", "pattern", "description", 5);
            author.AddExpression(expression);
            Assert.Contains(author.Expressions, x => x == expression);
        }
    }
}
