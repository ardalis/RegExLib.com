using System.Collections.Generic;
using RegExLib.Core.Events;
using RegExLib.Core.Interfaces;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
    public class Author : BaseEntity
    {
        public string FullName { get; }
        public string Username { get; }
        public string UserId { get; }
        public List<Expression> Expressions { get; private set; } = new List<Expression>();

        public Author(string userId, string username, string fullName)
        {
            UserId = userId;
            Username = username.Replace("@", "");
            FullName = fullName;
        }

        public void AddExpression(Expression expression)
        {
            if (expression != null)
            {
                Expressions.Add(expression);
            }
        }

        public void AddExpressions(IEnumerable<Expression> expressions)
        {
            if (expressions != null)
            {
                Expressions.AddRange(expressions);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(FullName)?string.Empty: FullName;
        }
    }
}
