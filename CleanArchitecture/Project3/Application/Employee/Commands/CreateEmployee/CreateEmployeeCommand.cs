using Application.DTOs.Employee;
using MediatR;

namespace Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeDto>
    {
        public CreateEmployeeDto EmployeeDto { get; set; }
    }
}
