using Application.DTOs.Department;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<CreateDepartmentDto>
    {
        public CreateDepartmentDto CreateDepartment { get; set; }
    }
}
