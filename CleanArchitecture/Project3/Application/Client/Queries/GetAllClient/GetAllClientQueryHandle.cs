using Application.DTOs.Cart;
using Application.DTOs.Client;
using Application.DTOs.Service;
using Application.DTOs.ServiceCharges;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Client.Queries.GetAllClient
{
    public class GetAllClientQueryHandle : IRequestHandler<GetAllClientQuery, ICollection<GetClientDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllClientQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetClientDto>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
        {
            var clients = await _unitOfWork.Clients.All(); // Ensure to materialize the collection
            var clientDtos = new List<GetClientDto>();

            foreach (var client in clients)
            {
                var cartDetails = await _unitOfWork.Cart.GetCartById(client.ClientId); // Ensure to materialize the collection

                var cartDto = new GetClientDto
                {
                    ClientId = client.ClientId,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Dob = client.Dob,
                    Email = client.Email,
                    Phone = client.Phone,
                    CartDetail = new List<GetCartServiceChargeDto>() // Initialize the list
                };

                foreach (var cartDetail in cartDetails)
                {
                    var serviceCharge = await _unitOfWork.ServicesCharges.GetById(cartDetail.ServiceChargeId);

                    if (serviceCharge != null)
                    {
                        var cartServiceChargeDto = new GetCartServiceChargeDto
                        {
                            ClientId = cartDetail.ClientId,
                            ServiceChargeId = cartDetail.ServiceChargeId,
                            ServiceChargesName = serviceCharge.ServiceChargesName,
                            ServiceChargesDescription = serviceCharge.ServiceChargesDescription,
                            Price = serviceCharge.Price
                        };

                        cartDto.CartDetail.Add(cartServiceChargeDto);
                    }
                }

                clientDtos.Add(cartDto);
            }

            return clientDtos;
        }
    }
}
