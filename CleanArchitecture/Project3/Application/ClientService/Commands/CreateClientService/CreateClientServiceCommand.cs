
using Application.DTOs.ClientService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Commands.CreateClientService
{
    public class CreateClientServiceCommand : IRequest<CreateClientServiceDto>
    {
        public CreateClientServiceDto ClientServiceDto { get; set; }
    }
}
