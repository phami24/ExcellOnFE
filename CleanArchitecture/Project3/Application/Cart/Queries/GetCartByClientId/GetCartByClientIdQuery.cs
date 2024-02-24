using Application.DTOs.ServiceCharges;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetCartByClientId
{
    public class GetCartByClientIdQuery : IRequest<List<GetServiceChargesDto>>
    {
        public int Id { get; set; }
    }
}
