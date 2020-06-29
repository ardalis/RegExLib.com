using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly List<Expression> _expressions = new List<Expression>();
        public IEnumerable<Expression> Expressions => new ReadOnlyCollection<Expression>(_expressions);

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
                _expressions.Add(expression);
            }
        }

        public void AddExpressions(IEnumerable<Expression> expressions)
        {
            if (expressions != null)
            {
                _expressions.AddRange(expressions);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(FullName)?string.Empty: FullName;
        }
    }
}
