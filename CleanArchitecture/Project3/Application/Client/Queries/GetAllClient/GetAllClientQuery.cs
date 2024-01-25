using Application.DTOs.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetAllClient
{
    public class GetAllClientQuery : IRequest<ICollection<GetClientDto>>
    {
    }
}
