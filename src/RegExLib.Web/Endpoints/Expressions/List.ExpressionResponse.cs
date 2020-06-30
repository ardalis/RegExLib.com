namespace RegExLib.Web.Endpoints.Expressions
{
  public class ExpressionResponse
  {
    public int Id { get; private set; }
    public int AuthorId { get; private set; }
    public string Title { get; private set; }
    public string Pattern { get; private set; }
    public string Description { get; private set; }

    public ExpressionResponse(int id, string title, string pattern, string description, int authorId)
    {
      Id = id;
      Title = title;
      Pattern = pattern;
      Description = description;
      AuthorId = authorId;
    }

  }
}
