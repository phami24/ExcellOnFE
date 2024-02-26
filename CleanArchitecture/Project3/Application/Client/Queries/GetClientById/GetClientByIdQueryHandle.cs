using Application.DTOs.Cart;
using Application.DTOs.Client;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetClientById
{
    public class GetClientByIdQueryHandle : IRequestHandler<GetClientByIdQuery, ICollection<GetClientDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientByIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<GetClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var clients = await _unitOfWork.Clients.GetById(request.Id);
                if (clients == null)
                {
                    return null;
                }
                var clientDtos = new List<GetClientDto>();
                //foreach (var client in clients)
                //{
                    var cartDetails = await _unitOfWork.Cart.GetCartById(clients.ClientId); // Ensure to materialize the collection

                    var cartDto = new GetClientDto
                    {
                        ClientId = clients.ClientId,
                        FirstName = clients.FirstName,
                        LastName = clients.LastName,
                        Dob = clients.Dob,
                        Email = clients.Email,
                        Phone = clients.Phone,
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
                    //}

                    clientDtos.Add(cartDto);
                }
                return clientDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
