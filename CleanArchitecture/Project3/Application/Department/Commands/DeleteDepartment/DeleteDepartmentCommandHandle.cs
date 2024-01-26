using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandle : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentCommandHandle(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var departmentDeleted = await _departmentRepository.GetById(request.Id);
                if (departmentDeleted != null)
                {
                    await _departmentRepository.Delete(departmentDeleted);
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
