using Application.DTOs.Client;
using Application.DTOs.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<GetServiceDto>
    {
        public int ServiceId { get; set; }
    }
}
