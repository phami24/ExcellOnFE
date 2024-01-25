using Application.DTOs.Client;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetAllClient
{
    public class GetAllClientQueryHandle : IRequestHandler<GetAllClientQuery, ICollection<GetClientDto>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientQueryHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ICollection<GetClientDto>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.All();
            var clientsDto = new List<GetClientDto>();

            foreach (Domain.Entities.Client c in clients)
            {
                var clientDto = new GetClientDto()
                {
                    ClientId = c.ClientId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Dob = c.Dob,
                };
                clientsDto.Add(clientDto);
            }

            return clientsDto;
        }
    }
}
