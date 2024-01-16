using Shared;

namespace Application.Abstractioin.Messaging
{
    public interface IQueryHandler<in IQuery, TResponse>
      where IQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handler(TResponse response, CancellationToken cancellationToken);
    }
}
