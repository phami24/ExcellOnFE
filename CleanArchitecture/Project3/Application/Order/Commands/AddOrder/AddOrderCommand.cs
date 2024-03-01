using Application.DTOs.Cart;
using Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Commands.AddOrder
{
    public class AddOrderCommand : IRequest<AddOrderDto>
    {
        public AddOrderDto AddOrderDto { get; set; }
    }
}
