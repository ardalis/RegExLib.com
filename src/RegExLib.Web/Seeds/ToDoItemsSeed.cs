using System.Linq;
using RegExLib.Core.Entities;
using RegExLib.Infrastructure.Data;

namespace RegExLib.Web.Seeds
{
  public class ToDoItemsSeed : ISeedData
  {
    public static readonly ToDoItem ToDoItem1 = new ToDoItem
    {
      Title = "Get Sample Working",
      Description = "Try to get the sample to build."
    };
    public static readonly ToDoItem ToDoItem2 = new ToDoItem
    {
      Title = "Review Solution",
      Description = "Review the different projects in the solution and how they relate to one another."
    };
    public static readonly ToDoItem ToDoItem3 = new ToDoItem
    {
      Title = "Run and Review Tests",
      Description = "Make sure all the tests run and review what they are doing."
    };
    private readonly AppDbContext _dbContext;

    public ToDoItemsSeed(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void PopulateTestData()
    {
      if (HasData())
      {
        return;
      }

      _dbContext.RemoveRange(_dbContext.ToDoItems);
      
      _dbContext.SaveChanges();
      _dbContext.ToDoItems.Add(ToDoItem1);
      _dbContext.ToDoItems.Add(ToDoItem2);
      _dbContext.ToDoItems.Add(ToDoItem3);

      _dbContext.SaveChanges();
    }

    private bool HasData()
    {
      return _dbContext.ToDoItems.Any();
    }

  }
}
