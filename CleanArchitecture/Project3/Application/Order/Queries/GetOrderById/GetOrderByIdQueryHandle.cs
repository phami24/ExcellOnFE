using Application.DTOs.Order;
using Application.DTOs.OrderDetail;
using Domain.Abstraction;
using MediatR;

namespace Application.Order.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandle : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.Order.GetById(request.OrderId);

                if (order == null)
                {
                    return null;
                }
                var orderDetails = order.OrderDetails.Select(detail => new GetOrderDetailDto
                {
                   
                    OrderDetailId = detail.OrderDetailId,
                    ServiceChargeId = detail.ServiceChargesId,
                }).ToList();


                GetOrderDto orderDto = new GetOrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    OrderTotal = order.OrderTotal,
                    OrderDetail = orderDetails,
                    ClientId = order.ClientId
                };

                return orderDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
