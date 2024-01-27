using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetTotalClient
{
    public class GetTotalClientQueryHandle : IRequestHandler<GetTotalClientQuery, int>
    {
        public readonly IClientRepository _clientRepository;
        public GetTotalClientQueryHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public Task<int> Handle(GetTotalClientQuery request, CancellationToken cancellationToken)
        {
            return _clientRepository.Count();
        }
    }
}

