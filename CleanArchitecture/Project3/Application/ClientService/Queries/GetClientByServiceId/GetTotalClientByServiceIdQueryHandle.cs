using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Queries.GetClientByServiceId
{
    internal class GetTotalClientByServiceIdQueryHandle : IRequestHandler<GetTotalClientByServiceIdQuery, int>
    {
        public IClientServiceRepository _clientServiceRepository { get; set; }

        public GetTotalClientByServiceIdQueryHandle(IClientServiceRepository clientServiceRepository)
        {
            _clientServiceRepository = clientServiceRepository;
        }
        public Task<int> Handle(GetTotalClientByServiceIdQuery request, CancellationToken cancellationToken)
        {
            return _clientServiceRepository.GetTotalClientsByServiceId(request.ServiceId);
        }
    }
}
