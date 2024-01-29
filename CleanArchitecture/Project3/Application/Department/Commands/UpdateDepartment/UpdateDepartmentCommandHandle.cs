using Application.DTOs.Department;
using Domain.Abstraction;
using MediatR;

namespace Application.Department.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandle : IRequestHandler<UpdateDepartmentCommand, UpdateDepartmentDto>
    {
        public readonly IUnitOfWork _unitOfWork;
        public UpdateDepartmentCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateDepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exitingEmployee = _unitOfWork.Departments.GetById(request.UpdateDepartmentDto.Id);
                if (exitingEmployee != null)
                {

                    var updateEmployee = new Domain.Entities.Department()
                    {
                        Id = request.UpdateDepartmentDto.Id,
                        DepartmentDescription = request.UpdateDepartmentDto.DepartmentDescription,
                        DepartmentName = request.UpdateDepartmentDto.DepartmentName
                    };
                    await _unitOfWork.Departments.Update(updateEmployee);
                    await _unitOfWork.CompleteAsync();
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
