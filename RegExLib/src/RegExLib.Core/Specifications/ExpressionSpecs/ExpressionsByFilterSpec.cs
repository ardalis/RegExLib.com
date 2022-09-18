using Ardalis.Specification;
using RegExLib.Core.Entities;
using RegExLib.Core.Specifications.Helpers;

namespace RegExLib.Core.Specifications;
public sealed class ExpressionsByFilterSpec : Specification<Expression>
{
  public ExpressionsByFilterSpec(ExpressionFilter filter)
  {
    if (!filter.IsTrackingEnabled)
    {
      Query
        .AsNoTracking();
    }

    if (!string.IsNullOrEmpty(filter.Title))
    {
      Query
        .Search(b => b.Title, $"% {filter.Title} %");
    }

    if (!string.IsNullOrEmpty(filter.Description))
    {
      Query
        .Search(b => b.Description, $"% {filter.Description} %");
    }

    if (filter.AuthorId != null)
    {
      Query
        .Where(b => b.AuthorId == filter.AuthorId);
    }

    if (filter.LoadChildren)
    {
      Query
        .Include(b => b.Author);
    }

    if (filter.IsPagingEnabled)
    {
      Query
        .Skip(PaginationHelper.CalculateSkip(filter))
        .Take(PaginationHelper.CalculateTake(filter));
    }

    Query
      .OrderBy(b => b.Title);
  }
}
