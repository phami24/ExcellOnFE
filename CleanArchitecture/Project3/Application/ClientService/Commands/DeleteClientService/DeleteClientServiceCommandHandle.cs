using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Commands.DeleteClientService
{
    public class DeleteClientServiceCommandHandle : IRequestHandler<DeleteClientServiceCommand, bool>
    {
        private readonly IClientServiceRepository _clientServiceRepository;

        public DeleteClientServiceCommandHandle(IClientServiceRepository clientServiceRepository)
        {
            _clientServiceRepository = clientServiceRepository;
        }

        public async Task<bool> Handle(DeleteClientServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientServiceDeleted = await _clientServiceRepository.GetById(request.ClientServiceId);
                if (clientServiceDeleted != null)
                {
                    await _clientServiceRepository.Delete(clientServiceDeleted);
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
