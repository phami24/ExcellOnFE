using Application.DTOs.Service;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Commands.CreateService
{
    public class CreateServiceCommandHandle : IRequestHandler<CreateServiceCommand, CreateServiceDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public CreateServiceCommandHandle(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<CreateServiceDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newService = new Domain.Entities.Service()
                {
                    ServiceName = request.ServiceDto.ServiceName,
                    Description = request.ServiceDto.Description,
                    TotalDay = request.ServiceDto.TotalDay,
                };

                bool isCreate = await _serviceRepository.Add(newService);

                if (isCreate)
                {
                    // Additional logic can be added here if needed
                    return request.ServiceDto;
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
