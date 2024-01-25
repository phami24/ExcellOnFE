using Application.DTOs.Client;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetClientById
{
    public class GetClientByIdQueryHandle : IRequestHandler<GetClientByIdQuery, GetClientDto>
    {
        public readonly IClientRepository _clientRepository;
        public GetClientByIdQueryHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<GetClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var client = await _clientRepository.GetById(request.Id);
                if (client == null)
                {
                    return null;
                }
                GetClientDto clientDto = new GetClientDto
                {
                    Email = client.Email,
                    Phone = client.Phone,
                    ClientId = client.ClientId,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Dob = client.Dob
                };
                return clientDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
