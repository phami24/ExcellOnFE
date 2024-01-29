using Application.DTOs.Employee;
using Domain.Abstraction;
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
    public class DeleteEmployeeCommandHandle : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employeeDeleted = await _unitOfWork.Employees.GetById(request.EmployeeId);
                if (employeeDeleted != null)
                {
                    await _unitOfWork.Employees.Delete(employeeDeleted);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
