using Application.DTOs.Employee;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandle : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeDto>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IDepartmentRepository _departmentRepository;

        public DeleteEmployeeCommandHandle(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<DeleteEmployeeDto> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employeeDeleted = await _employeeRepository.GetById(request.DeleteEmployeeDto.Id);
                if (employeeDeleted != null)
                {
                    await _employeeRepository.Delete(employeeDeleted);
                }
                return request.DeleteEmployeeDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
