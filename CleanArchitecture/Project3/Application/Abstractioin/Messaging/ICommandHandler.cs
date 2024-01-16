using Shared;
namespace Application.Abstractioin.Messaging
{
    public interface ICommandHandler<in TCommand>
    {
        Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
    }
    public interface ICommandHandler<in ICommand, TResponse>
        where ICommand : ICommand<TResponse>
    {
        Task<Result<TResponse>> Handle(ICommand command, CancellationToken cancellationToken);
    }
}
