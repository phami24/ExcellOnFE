using Application.DTOs.Client;
using Domain.Interfaces;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommandHandle : IRequestHandler<CreateClientCommand, CreateClientDto>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public CreateClientCommandHandle(
            IClientRepository clientRepository,
            ICloudinaryService cloudinaryService)
        {
            _clientRepository = clientRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<CreateClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = request.ClientDto.Email;

                var newClient = new Domain.Entities.Client()
                {
                    FirstName = request.ClientDto.FirstName,
                    LastName = request.ClientDto.LastName,
                    Email = request.ClientDto.Email,
                    Phone = request.ClientDto.Phone,
                    Dob = request.ClientDto.Dob,
                };

                Console.WriteLine(newClient);

                bool isCreate = await _clientRepository.Add(newClient);
                if (isCreate)
                {
                    // You might want to perform additional logic here if needed
                    return request.ClientDto;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
