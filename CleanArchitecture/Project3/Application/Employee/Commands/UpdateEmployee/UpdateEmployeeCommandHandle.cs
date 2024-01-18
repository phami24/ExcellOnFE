using Application.DTOs.Employee;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;

namespace Application.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandle : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeDto>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IDepartmentRepository _departmentRepository;
        public UpdateEmployeeCommandHandle(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<UpdateEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newEmployee = new Domain.Entities.Employee()
                {
                    FirstName = request.UpdateEmployeeDto.FirstName,
                    LastName = request.UpdateEmployeeDto.LastName,
                    Avatar = request.UpdateEmployeeDto.Avatar,
                    Email = request.UpdateEmployeeDto.Email,
                    Phone = request.UpdateEmployeeDto.Phone,
                    DepartmentId = request.UpdateEmployeeDto.DepartmentId,
                    Dob = request.UpdateEmployeeDto.Dob
                };
                Console.WriteLine(newEmployee);
                bool isCreate = await _employeeRepository.Add(newEmployee);
                if (isCreate)
                {
                    var employee = await _employeeRepository.GetByEmail(newEmployee.Email);
                    await _departmentRepository.AddEmployee(employee, employee.DepartmentId);
                    return request.UpdateEmployeeDto;
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
