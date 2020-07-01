using System.Linq;
using RegExLib.Core.Entities;
using RegExLib.Infrastructure.Data;

namespace RegExLib.Web.Seeds
{
  public class AuthorsSeed : ISeedData
  {
    public static readonly Author Author1 = new Author("6ef8ba23-5f6c-4ec3-abba-3b7b8c231b6e", "admin", "Admin Admin");
    private readonly AppDbContext _dbContext;

    public AuthorsSeed(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void PopulateTestData()
    {
      if (HasData())
      {
        return;
      }

      _dbContext.RemoveRange(_dbContext.Author);
      
      _dbContext.SaveChanges();
      _dbContext.Author.Add(Author1);

      _dbContext.SaveChanges();
    }

    private bool HasData()
    {
      return _dbContext.Author.Any();
    }
  }
}
