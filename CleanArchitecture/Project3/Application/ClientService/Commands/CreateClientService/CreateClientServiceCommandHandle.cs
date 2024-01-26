
using Application.DTOs.ClientService;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Commands.CreateClientService
{
    public class CreateClientServiceCommandHandle : IRequestHandler<CreateClientServiceCommand, CreateClientServiceDto>
    {
        private readonly IClientServiceRepository _clientServiceRepository;

        public CreateClientServiceCommandHandle(IClientServiceRepository clientServiceRepository)
        {
            _clientServiceRepository = clientServiceRepository;
        }

        public async Task<CreateClientServiceDto> Handle(CreateClientServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newClientService = new Domain.Entities.ClientService()
                {
                    Status = request.ClientServiceDto.Status,
                    ExpiredDay = request.ClientServiceDto.ExpiredDay,
                    ServiceId = request.ClientServiceDto.ServiceId,
                    ClientId = request.ClientServiceDto.ClientId,
                    StartDay = request.ClientServiceDto.StartDay
                };

                bool isCreate = await _clientServiceRepository.Add(newClientService);

                if (isCreate)
                {
                    // Additional logic can be added here if needed
                    return request.ClientServiceDto;
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
