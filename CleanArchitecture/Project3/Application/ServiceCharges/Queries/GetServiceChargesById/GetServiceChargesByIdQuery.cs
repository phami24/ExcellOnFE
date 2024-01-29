
using Application.DTOs.ServiceCharges;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceCharges.Queries.GetServiceChargesById
{
    public class GetServiceChargesByIdQuery : IRequest<GetServiceChargesDto>
    {
        public int ServiceChargesId { get; set; }
    }
}
