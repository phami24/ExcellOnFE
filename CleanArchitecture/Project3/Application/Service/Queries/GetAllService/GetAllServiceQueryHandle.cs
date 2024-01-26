using Application.DTOs.Service;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Queries.GetAllService
{
    public class GetAllServiceQueryHandle : IRequestHandler<GetAllServiceQuery, ICollection<GetServiceDto>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServiceQueryHandle(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ICollection<GetServiceDto>> Handle(GetAllServiceQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.All();
            var servicesDto = new List<GetServiceDto>();

            foreach (Domain.Entities.Service s in services)
            {
                var serviceDto = new GetServiceDto()
                {
                    ServiceName = s.ServiceName,
                    Description = s.Description,
                    TotalDay = s.TotalDay,
                    
                };
                servicesDto.Add(serviceDto);
            }

            return servicesDto;
        }
    }
}
