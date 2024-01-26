using Application.DTOs.ClientService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Queries.GetAllClientService
{
    public class GetAllClientServiceQuery : IRequest<ICollection<GetClientServiceDto>>
    {
    }
}
