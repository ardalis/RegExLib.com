using System.Collections.Generic;
using RegExLib.Core.Events;
using RegExLib.Core.Interfaces;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
    public class Author : BaseEntity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public List<Expression> Expressions { get; private set; } = new List<Expression>();

        public Author()
        {
        }

        public Author AddExpression(Expression expression)
        {
            if (expression != null)
            {
                Expressions.Add(expression);
            }

            return this;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(FullName)?string.Empty: FullName;
        }
    }
}
