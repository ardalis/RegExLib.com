namespace RegExLib.Web.Endpoints.Expressions
{
  public class ExpressionResponse
  {
    public int Id { get; set; }
    public int AuthorId { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Title { get; set; }
    public string Pattern { get; set; }
    public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

  }
}
