using Ardalis.Specification;
using RegExLib.Core.Entities;

namespace RegExLib.Core.Specifications;
public sealed class ExpressionByFilterSpec : Specification<Expression>, ISingleResultSpecification
{
  public ExpressionByFilterSpec(ExpressionFilter filter)
  {
    if (!filter.IsTrackingEnabled)
    {
      Query
        .AsNoTracking();
    }

    if (filter.LoadChildren)
    {
      Query
        .Include(b => b.Author);
    }

    Query
      .Where(b => b.Id == filter.Id);
  }
}
