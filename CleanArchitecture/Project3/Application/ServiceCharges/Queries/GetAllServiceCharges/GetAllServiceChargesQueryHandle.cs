using Application.DTOs.ServiceCharges;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceCharges.Queries.GetAllServiceCharges
{
    public class GetAllServiceChargesQueryHandle : IRequestHandler<GetAllServiceChargesQuery, ICollection<GetServiceChargesDto>>
    {
        private readonly IServiceChargesRepository _serviceChargeRepository;

        public GetAllServiceChargesQueryHandle(IServiceChargesRepository serviceChargeRepository)
        {
            _serviceChargeRepository = serviceChargeRepository;
        }

        public async Task<ICollection<GetServiceChargesDto>> Handle(GetAllServiceChargesQuery request, CancellationToken cancellationToken)
        {
            var serviceCharges = await _serviceChargeRepository.All();
            var serviceChargesDto = new List<GetServiceChargesDto>();

            foreach (Domain.Entities.ServiceCharges sc in serviceCharges)
            {
                var serviceChargeDto = new GetServiceChargesDto()
                {
                    ServiceChargesId = sc.ServiceChargesId,
                    ServiceChargesName = sc.ServiceChargesName,
                    Price = sc.Price,
                    ServiceChargesDescription = sc.ServiceChargesDescription,
                    ServiceId = sc.ServiceId,
                   
    };
                serviceChargesDto.Add(serviceChargeDto);
            }

            return serviceChargesDto;
        }

        
    }
}
