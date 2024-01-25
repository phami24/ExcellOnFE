using Application.DTOs.Service;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandle : IRequestHandler<GetServiceByIdQuery, GetServiceDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdQueryHandle(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<GetServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await _serviceRepository.GetById(request.ServiceId);

                if (service == null)
                {
                    return null;
                }

                GetServiceDto serviceDto = new GetServiceDto
                {
                   ServiceName = service.ServiceName,
                   Description = service.Description,
                   TotalDay = service.TotalDay,
                };

                return serviceDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
