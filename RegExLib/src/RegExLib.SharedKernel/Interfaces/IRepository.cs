using Ardalis.Specification;

namespace RegExLib.SharedKernel.Interfaces;
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
