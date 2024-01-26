using Application.DTOs.Department;
using Domain.Repositories;
using MediatR;

namespace Application.Department.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandle : IRequestHandler<UpdateDepartmentCommand, UpdateDepartmentDto>
    {
        public readonly IDepartmentRepository _departmentRepository;
        public UpdateDepartmentCommandHandle(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<UpdateDepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exitingEmployee = _departmentRepository.GetById(request.UpdateDepartmentDto.Id);
                if (exitingEmployee != null)
                {

                    var updateEmployee = new Domain.Entities.Department()
                    {
                        Id = request.UpdateDepartmentDto.Id,
                        DepartmentDescription = request.UpdateDepartmentDto.DepartmentDescription,
                        DepartmentName = request.UpdateDepartmentDto.DepartmentName
                    };
                    await _departmentRepository.Update(updateEmployee);
                    return request.UpdateDepartmentDto;
                }
                else
                {
                    throw new Exception("Employee does not exit !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
