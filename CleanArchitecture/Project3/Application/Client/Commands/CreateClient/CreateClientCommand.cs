using Application.DTOs.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<CreateClientDto>
    {
        public CreateClientDto ClientDto { get; set; }
    }
}
