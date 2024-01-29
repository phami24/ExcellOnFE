using Domain.Abstraction;
using MediatR;

namespace Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommandHandle : IRequestHandler<DeleteClientCommand, bool>
    {
        public readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientToDelete = await _unitOfWork.Clients.GetById(request.ClientId);
                if (clientToDelete != null)
                {
                    await _unitOfWork.Clients.Delete(clientToDelete);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
