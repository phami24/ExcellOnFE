using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Commands.DeleteClientService
{
    public class DeleteClientServiceCommand : IRequest<bool>
    {
        public int ClientServiceId { get; set; }
    }
}
