using Application.DTOs.Client;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Commands.UpdateClient
{
    public class UpdateClientCommandHandle : IRequestHandler<UpdateClientCommand, UpdateClientDto>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandle(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<UpdateClientDto> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingClient = await _clientRepository.GetById(request.UpdateClientDto.ClientId);

                if (existingClient == null)
                {
                    
                    return null;
                }

                // Update the existingClient with the new values from the request
                existingClient.FirstName = request.UpdateClientDto.FirstName;
                existingClient.LastName = request.UpdateClientDto.LastName;
                existingClient.Email = request.UpdateClientDto.Email;
                existingClient.Phone = request.UpdateClientDto.Phone;
                existingClient.Dob = request.UpdateClientDto.Dob;

                // Update the client in the repository
                bool isUpdate = await _clientRepository.Update(existingClient); 

                if (isUpdate)
                {
                    // If the update is successful, you may return the updated DTO or any relevant information
                    return request.UpdateClientDto;
                }

                // If the update fails, you might want to handle it accordingly
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Handle the exception as needed, log, and return null or throw a custom exception
                return null;
            }
        }

    }
}
