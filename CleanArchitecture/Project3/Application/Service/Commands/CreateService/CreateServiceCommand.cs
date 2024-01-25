
using Application.DTOs.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Commands.CreateService
{
    public class CreateServiceCommand : IRequest<CreateServiceDto>
    {
        public CreateServiceDto ServiceDto { get; set; }
    }
}
