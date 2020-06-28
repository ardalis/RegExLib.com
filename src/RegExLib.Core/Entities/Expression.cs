using System;
using RegExLib.Core.Events;
using RegExLib.Core.Interfaces;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
    public class Expression : BaseEntity
    {
        public readonly int AuthorId;
        public readonly string Title;
        public readonly string Pattern;
        public readonly string Description;
        public readonly Author Author;

        public Expression(string title, string pattern, string description, Author author)
        {
            Title = title;
            Pattern = pattern;
            Description = description;
            Author = author;
            AuthorId = author.Id;
        }


        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ?string.Empty: Title;
        }
    }
}
