
using Application.DTOs.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Commands.UpdateService
{
    public class UpdateServiceCommand : IRequest<UpdateServiceDto>
    {
        public UpdateServiceDto UpdateServiceDto { get; set; }
    }
}
