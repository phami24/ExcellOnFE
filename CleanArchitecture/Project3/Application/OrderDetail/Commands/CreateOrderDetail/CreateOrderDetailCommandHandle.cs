using Application.Cart.Commands.AddCart;
using Application.DTOs.Cart;
using Application.DTOs.OrderDetail;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderDetail.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommandHandle : IRequestHandler<CreateOrderDetailCommand, AddOrderDetailDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderDetailCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddOrderDetailDto> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newOrderDetail = new Domain.Entities.OrderDetail()
                {
                    OrderId = request.AddOrderDetailDto.OrderId,
                    ServiceChargesId = request.AddOrderDetailDto.ServiceChargesId,

                };

                bool isCreate = await _unitOfWork.OrderDetail.Add(newOrderDetail);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    // Additional logic can be added here if needed
                    return request.AddOrderDetailDto;
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
