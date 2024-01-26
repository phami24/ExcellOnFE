using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int ClientId { get; set; }
    }
}
