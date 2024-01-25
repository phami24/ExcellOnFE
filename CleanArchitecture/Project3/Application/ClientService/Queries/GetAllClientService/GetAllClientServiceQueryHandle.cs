using Application.DTOs.ClientService;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Queries.GetAllClientService
{
    public class GetAllClientServiceQueryHandle : IRequestHandler<GetAllClientServiceQuery, ICollection<GetClientServiceDto>>
    {
        private readonly IClientServiceRepository _clientServiceRepository;

        public GetAllClientServiceQueryHandle(IClientServiceRepository clientServiceRepository)
        {
            _clientServiceRepository = clientServiceRepository;
        }

        public async Task<ICollection<GetClientServiceDto>> Handle(GetAllClientServiceQuery request, CancellationToken cancellationToken)
        {
            var clientServices = await _clientServiceRepository.All();
            var clientServicesDto = new List<GetClientServiceDto>();

            foreach (Domain.Entities.ClientService cs in clientServices)
            {
                var clientServiceDto = new GetClientServiceDto()
                {
                    Status = cs.Status,
                    ExpiredDay = cs.ExpiredDay,
                    ServiceId = cs.ServiceId,
                    ClientId = cs.ClientId,
                    StartDay = cs.StartDay
                };
                clientServicesDto.Add(clientServiceDto);
            }

            return clientServicesDto;
        }
    }
}
