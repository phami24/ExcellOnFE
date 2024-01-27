using Domain.Interfaces;
using MediatR;

namespace Application.Employee.Queries.GetTotalEmployee
{
    public class GetTotalEmployeeQueryHandle : IRequestHandler<GetTotalEmployeeQuery, int>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public GetTotalEmployeeQueryHandle(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<int> Handle(GetTotalEmployeeQuery request, CancellationToken cancellationToken)
        {
            return _employeeRepository.Count();
        }
    }
}
