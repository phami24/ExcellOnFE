using Application.DTOs.Department;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandle : IRequestHandler<CreateDepartmentCommand, CreateDepartmentDto>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public readonly IEmployeeRepository _employeeRepository;
        public CreateDepartmentCommandHandle(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateDepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newDepartment = new Domain.Entities.Department
                {
                    DepartmentName = request.CreateDepartment.DepartmentName,
                    DepartmentDescription = request.CreateDepartment.DepartmentDescription,
                };
                bool isCreate = await _departmentRepository.Add(newDepartment);
                _departmentRepository.Save();
                if (isCreate)
                {
                    return request.CreateDepartment;
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
