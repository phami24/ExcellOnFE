using Application.DTOs.Client;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetClientByName
{
    public class GetClientByNameQueryHandle : IRequestHandler<GetClientByNameQuery, ICollection<GetClientDto>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByNameQueryHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ICollection<GetClientDto>> Handle(GetClientByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _clientRepository.GetClientsByName(request.FirstName, request.LastName);
                var clientsDto = new List<GetClientDto>();

                foreach (Domain.Entities.Client client in clients)
                {
                    var clientDto = new GetClientDto()
                    {
                        ClientId = client.ClientId,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Email = client.Email,
                        Dob = client.Dob,
                        Phone = client.Phone,
                    };
                    clientsDto.Add(clientDto);
                }

                return clientsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query: " + ex);
                return null;
            }
        }
    }
}
