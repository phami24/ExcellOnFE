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
            var orders = await _unitOfWork.Order.All();

            var orderDtos = new List<GetOrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new GetOrderDto
                {
                    OrderId = order.OrderId,
                    ClientId = order.ClientId,
                    OrderDate = order.OrderDate,
                    OrderTotal = order.OrderTotal,
                    OrderDetail = new List<GetOrderDetailDto>()
                };

                // Fetch and map order details for this order
                var orderDetails = await _unitOfWork.OrderDetail.GetOrderDetailsByOrderId(order.OrderId);

                foreach (var orderDetail in orderDetails)
                {
                    var orderDetailDto = new GetOrderDetailDto
                    {
                        // Map properties from orderDetail to orderDetailDto
                        OrderDetailId = orderDetail.OrderDetailId,
                        ServiceChargeId = orderDetail.ServiceChargesId,
                        OrderId = orderDetail.OrderId,
                    };
                    orderDto.OrderDetail.Add(orderDetailDto);
                }

                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }
    }
}
