using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs.Order;
using Domain.Abstraction;
using MediatR;

namespace Application.Order.Commands.AddOrder
{
    public class AddOrderCommmandHandle : IRequestHandler<AddOrderCommand, AddOrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderCommmandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddOrderDto> Handle(AddOrderCommand request, CancellationToken cancellationToken)
{
    try
    {
        if (request == null || request.AddOrderDto == null)
        {
            // Handle the case where request or AddOrderDto is null
            return null;
        }

        var newOrder = new Domain.Entities.Order()
        {
            ClientId = request.AddOrderDto.ClientId,
            OrderDate = request.AddOrderDto.OrderDate,
            OrderStatus = request.AddOrderDto.OrderStatus,
            OrderTotal = request.AddOrderDto.OrderTotal,
            OrderDetails = new List<Domain.Entities.OrderDetail>()
        };

        // Add order details
        if (request.AddOrderDto.OrderDetails != null)
        {
            foreach (var item in request.AddOrderDto.OrderDetails)
            {
                if (item == null || item.ServiceChargeId == null || item.OrderId == null)
                {
                    continue;
                }
                var orderDetail = new Domain.Entities.OrderDetail()
                {
                    // Populate properties of order detail from DTO
                    ServiceChargesId = item.ServiceChargeId,
                    OrderId = item.OrderId,
                };

                // Add order detail to order
                newOrder.OrderDetails.Add(orderDetail);
            }
        }

        // Ensure _unitOfWork is not null
        if (_unitOfWork == null || _unitOfWork.Order == null)
        {
            return null;
        }

        // Add the new order to the repository
        _unitOfWork.Order.Add(newOrder);

        // Save changes
        await _unitOfWork.CompleteAsync();

        return request.AddOrderDto;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        // Log the exception here
        // You might want to log the exception and return a meaningful error message
        return null;
    }
}

    }
}
