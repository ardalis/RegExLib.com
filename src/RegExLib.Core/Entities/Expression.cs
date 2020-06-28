using System;
using RegExLib.Core.Events;
using RegExLib.Core.Interfaces;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
    public class Expression : BaseEntity
    {
        public int AuthorId { get; private set; }
        public string Title { get; }
        public string Pattern { get; }
        public string Description { get; }
        public Author Author { get; private set; }

        public Expression(string title, string pattern, string description)
        {
            Title = title;
            Pattern = pattern;
            Description = description;
        }

        public void SetAuthor(Author author)
        {
            Author = author;
            AuthorId = author.Id;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ?string.Empty: Title;
        }
    }
}
