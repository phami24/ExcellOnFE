using Application.DTOs.ServiceCharges;
using Application.ServiceCharges.Queries.GetServiceChargesByServiceId;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetCartByClientId
{
    public class GetCartByClientIdQueyHandle : IRequestHandler<GetCartByClientIdQuery, List<GetServiceChargesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartByClientIdQueyHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetServiceChargesDto>> Handle(GetCartByClientIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartDetails = await _unitOfWork.Cart.GetCartById(request.Id);
                List<GetServiceChargesDto> serviceChargesDto = new List<GetServiceChargesDto>();

                foreach (var cartDetail in cartDetails)
                {
                    var serviceCharge = await _unitOfWork.ServicesCharges.GetById(cartDetail.ServiceChargeId);

                    if (serviceCharge != null)
                    {
                        var serviceChargeDto = new GetServiceChargesDto()
                        {
                            ServiceChargesId = serviceCharge.ServiceChargesId,
                            ServiceChargesName = serviceCharge.ServiceChargesName,
                            Price = serviceCharge.Price,
                            ServiceChargesDescription = serviceCharge.ServiceChargesDescription,
                            ServiceId = serviceCharge.ServiceId
                        };
                        serviceChargesDto.Add(serviceChargeDto);
                    }
                }
                return serviceChargesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query: " + ex);
                return null;
            }
        }
    }
}
