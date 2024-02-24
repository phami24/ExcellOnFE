using Application.DTOs.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetAllCart
{
    public class GetAllCartQuery : IRequest<ICollection<GetCartServiceChargeDto>>
    {
    }
}
