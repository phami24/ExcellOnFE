using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Commands.DeleteService
{
    public class DeleteServiceCommandHandle : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceCommandHandle(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceDeleted = await _serviceRepository.GetById(request.ServiceId);
                if (serviceDeleted != null)
                {
                    await _serviceRepository.Delete(serviceDeleted);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
