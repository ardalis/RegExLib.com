using RegExLib.SharedKernel.Interfaces;
using RegExLib.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegExLib.Infrastructure.Data
{
  public class EfRepository : IRepository
  {
    private readonly AppDbContext _dbContext;
    private const int PerPage = 25;

    public EfRepository(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public T GetById<T>(int id) where T : BaseEntity
    {
      return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
    }

    public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
    {
      return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<List<T>> ListAsync<T>() where T : BaseEntity
    {
      return _dbContext.Set<T>().ToListAsync();
    }

    public Task<List<T>> ListAsync<T>(int page) where T : BaseEntity
    {
      return _dbContext.Set<T>().Skip(PerPage * (page)).Take(PerPage).ToListAsync();
    }

    public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
    {
      await _dbContext.Set<T>().AddAsync(entity);
      await _dbContext.SaveChangesAsync();

      return entity;
    }

    public async Task UpdateAsync<T>(T entity) where T : BaseEntity
    {
      _dbContext.Entry(entity).State = EntityState.Modified;
      await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(T entity) where T : BaseEntity
    {
      _dbContext.Set<T>().Remove(entity);
      await _dbContext.SaveChangesAsync();
    }
  }
}
