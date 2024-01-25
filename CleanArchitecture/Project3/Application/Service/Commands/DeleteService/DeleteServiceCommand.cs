using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Commands.DeleteService
{
    public class DeleteServiceCommand : IRequest<bool>
    { 
        public int ServiceId { get; set; }
    }
}
