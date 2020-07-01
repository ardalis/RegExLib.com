using System.Threading.Tasks;
using MediatR;
using System.Threading;

namespace RegExLib.UnitTests
{
  public class NoOpMediator : IMediator
  {
    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
      return Task.CompletedTask;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
      return Task.CompletedTask;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
#nullable disable
#pragma warning disable CS8604 // Possible null reference argument.
      return Task.FromResult<TResponse>(default);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public Task<object> Send(object request, CancellationToken cancellationToken = default)
    {
#nullable disable
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
      return Task.FromResult<object>(default);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
  }
}
