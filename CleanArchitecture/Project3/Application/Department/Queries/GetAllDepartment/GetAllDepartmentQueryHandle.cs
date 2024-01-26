using Application.DTOs.Department;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;

namespace Application.Department.Queries.GetAllDepartment
{
    public class GetAllDepartmentQueryHandle : IRequestHandler<GetAllDepartmentQuery, List<GetDepartmentDto>>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public readonly IEmployeeRepository _employeeRepository;
        public GetAllDepartmentQueryHandle(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<List<GetDepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.All();
            if (departments == null)
            {
                return null;
            }
            List<GetDepartmentDto> departmentDtos = new List<GetDepartmentDto>();
            foreach (var department in departments)
            {
                if (department != null)
                {
                    departmentDtos.Add(new GetDepartmentDto()
                    {
                        Id = department.Id,
                        DepartmentDescription = department.DepartmentDescription,
                        DepartmentName = department.DepartmentName
                    });
                }
            }
            return departmentDtos;
        }
    }
}
