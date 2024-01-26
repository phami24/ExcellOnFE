using Application.DTOs.ClientService;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ClientService.Queries.GetClientServiceById
{
    public class GetClientServiceByIdQueryHandler : IRequestHandler<GetClientServiceByIdQuery, GetClientServiceDto>
    {
        private readonly IClientServiceRepository _clientServiceRepository;

        public GetClientServiceByIdQueryHandler(IClientServiceRepository clientServiceRepository)
        {
            _clientServiceRepository = clientServiceRepository;
        }

        public async Task<GetClientServiceDto> Handle(GetClientServiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientService = await _clientServiceRepository.GetById(request.ClientServiceId);

                if (clientService == null)
                {
                    // Consider returning a specific result, like NotFound or an empty DTO
                    return null; // Return null for non-existing client service
                }

                var clientServiceDto = new GetClientServiceDto
                {
                    Status = clientService.Status,
                    ExpiredDay = clientService.ExpiredDay,
                    ServiceId = clientService.ServiceId,
                    ClientId = clientService.ClientId,
                    StartDay = clientService.StartDay
                    // Add any additional properties specific to your GetClientServiceDto
                };

                return clientServiceDto;
            }
            catch (Exception ex)
            {
                // Consider logging the exception instead of writing to the console
                Console.WriteLine("Query : " + ex);
                // Rethrow the exception or handle it appropriately based on your application's needs
                throw;
            }
        }
    }
}
