using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Department.Queries.GetTotalDepartment
{
    public class GetTotalDepartmentQueryHandle : IRequestHandler<GetTotalDepartmentQuery, int>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public GetTotalDepartmentQueryHandle(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<int> Handle(GetTotalDepartmentQuery request, CancellationToken cancellationToken)
        {
            return _departmentRepository.Count();
        }
    }
}
