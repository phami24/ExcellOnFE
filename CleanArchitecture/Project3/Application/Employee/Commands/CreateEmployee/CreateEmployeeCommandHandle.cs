using Application.DTOs.Employee;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Services;
using MediatR;

namespace Application.Employee.Commands.CreateEmployee
{
    internal class CreateEmployeeCommandHandle : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IDepartmentRepository _departmentRepository;
        public readonly ICloudinaryService _cloudinaryService;
        public CreateEmployeeCommandHandle(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            ICloudinaryService cloudinaryService)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = request.EmployeeDto.Email;
                string filePath = "";
                var result = _cloudinaryService.Upload(request.EmployeeDto.Avatar, fileName);
                if (result != null)
                {
                    filePath = result.Url.ToString();
                }
                var newEmployee = new Domain.Entities.Employee()
                {
                    FirstName = request.EmployeeDto.FirstName,
                    LastName = request.EmployeeDto.LastName,
                    Avatar = filePath,
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
                    _departmentRepository.Save();
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

