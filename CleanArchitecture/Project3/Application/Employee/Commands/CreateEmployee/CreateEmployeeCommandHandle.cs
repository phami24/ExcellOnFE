using Application.DTOs.Employee;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;

namespace Application.Employee.Commands.CreateEmployee
{
    internal class CreateEmployeeCommandHandle : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IDepartmentRepository _departmentRepository;
        public CreateEmployeeCommandHandle(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newEmployee = new Domain.Entities.Employee()
                {
                    FirstName = request.EmployeeDto.FirstName,
                    LastName = request.EmployeeDto.LastName,
                    Avatar = request.EmployeeDto.Avatar,
                    Email = request.EmployeeDto.Email,
                    Phone = request.EmployeeDto.Phone,
                    DepartmentId = request.EmployeeDto.DepartmentId,
                    Dob = request.EmployeeDto.Dob
                };
                if (request.EmployeeDto.Avatar == null)
                {
                    newEmployee.Avatar = "defautAvatar.png";
                }
                Console.WriteLine(newEmployee);
                bool isCreate = await _employeeRepository.Add(newEmployee);
                if (isCreate)
                {
                    var employee = await _employeeRepository.GetByEmail(newEmployee.Email);
                    await _departmentRepository.AddEmployee(employee, employee.DepartmentId);
                    return request.EmployeeDto;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

