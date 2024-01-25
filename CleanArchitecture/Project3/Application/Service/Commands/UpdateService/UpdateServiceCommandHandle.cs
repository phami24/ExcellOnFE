using Application.DTOs.Service;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Commands.UpdateService
{
    public class UpdateServiceCommandHandle : IRequestHandler<UpdateServiceCommand, UpdateServiceDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceCommandHandle(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<UpdateServiceDto> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingService = await _serviceRepository.GetById(request.UpdateServiceDto.ServiceId);

                if (existingService == null)
                {
                    // Handle the case where the service to be updated is not found
                    return null;
                }

                // Update the existingService with the new values from the request
                existingService.ServiceName = request.UpdateServiceDto.ServiceName;
                existingService.Description = request.UpdateServiceDto.Description;
                existingService.TotalDay = request.UpdateServiceDto.TotalDay;

                // Update the service in the repository
                bool isUpdate = await _serviceRepository.Update(existingService);

                if (isUpdate)
                {
                    // If the update is successful, you may return the updated DTO or any relevant information
                    return request.UpdateServiceDto;
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
