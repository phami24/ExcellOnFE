using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Queries.GetClientByServiceId
{
    public class GetTotalClientByServiceIdQuery : IRequest<int>
    {
        public int ServiceId { get; set; }
    }
}
