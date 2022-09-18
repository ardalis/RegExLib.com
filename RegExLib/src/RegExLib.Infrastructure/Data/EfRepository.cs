using Ardalis.Specification.EntityFrameworkCore;
using RegExLib.SharedKernel.Interfaces;

namespace RegExLib.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  private readonly AppDbContext _dbContext;
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }
}
