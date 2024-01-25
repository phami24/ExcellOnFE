using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommandHandle : IRequestHandler<DeleteClientCommand, bool>
    {
        public readonly IClientRepository _clientRepository; 

        public DeleteClientCommandHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientToDelete = await _clientRepository.GetById(request.ClientId);
                if (clientToDelete != null)
                {
                    await _clientRepository.Delete(clientToDelete);
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
