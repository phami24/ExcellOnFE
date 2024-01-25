using Application.DTOs.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<UpdateClientDto>
    {
      
        public UpdateClientDto UpdateClientDto { get; set; }

       
    }
}
