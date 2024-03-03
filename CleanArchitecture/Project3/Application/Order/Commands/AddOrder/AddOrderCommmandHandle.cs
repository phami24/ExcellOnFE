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
                var newOrder = new Domain.Entities.Order()
                {
                    OrderDate = request.AddOrderDto.OrderDate,
                    OrderStatus = request.AddOrderDto.OrderStatus,
                    OrderTotal = request.AddOrderDto.OrderTotal,
                    ClientId = request.AddOrderDto.ClientId,                 
                };

                bool isCreate = await _unitOfWork.Order.Add(newOrder);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    // Additional logic can be added here if needed
                    return request.AddOrderDto;
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
