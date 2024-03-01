using Application.Cart.Queries.GetCartId;
using Application.DTOs.Cart;
using Application.DTOs.Order;
using Application.DTOs.OrderDetail;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Queries.GetOrder
{
    public class GetOrderQueryHandle : IRequestHandler<GetOrderQuery, ICollection<GetOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<GetOrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Order.All();
            var orderDto = new List<GetOrderDto>();

            foreach (Domain.Entities.Order c in order)
            {
                var orderDtos = new GetOrderDto()
                {
                    OrderId = c.OrderId,
                    ClientId = c.ClientId,
                    OrderDate = c.OrderDate,
                   
                };
                orderDto.Add(orderDtos);
            }

            return orderDto;
        }
    }
}
