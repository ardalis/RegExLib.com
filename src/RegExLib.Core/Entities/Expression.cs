using System;
using RegExLib.Core.Events;
using RegExLib.Core.Interfaces;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
    public class Expression : BaseEntity
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Pattern { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }

        public Expression()
        {
            Author = new Author();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ?string.Empty: Title;
        }
    }
}
