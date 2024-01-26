using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
