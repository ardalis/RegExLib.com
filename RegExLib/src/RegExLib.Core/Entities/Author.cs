﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using RegExLib.SharedKernel;
using RegExLib.SharedKernel.Interfaces;

namespace RegExLib.Core.Entities
{
  public class Author : BaseEntity<int>, IAggregateRoot
  {
    public string FullName { get; private set; }
    public string Username { get; private set; }
    public string UserId { get; private set; }

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
      _expressions.Add(expression);
    }

    public override string ToString() => FullName;
  }
}
