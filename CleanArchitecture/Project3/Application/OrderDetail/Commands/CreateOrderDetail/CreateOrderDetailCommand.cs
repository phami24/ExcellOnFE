using Application.DTOs.Cart;
using Application.DTOs.OrderDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderDetail.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommand : IRequest<AddOrderDetailDto>
    {
        public AddOrderDetailDto AddOrderDetailDto { get; set; }
    }
}
