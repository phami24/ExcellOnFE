using Application.DTOs.Employee;
using Application.Employee.Commands.CreateEmployee;
using Application.Employee.Queries.GetAllEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var getCommand = new GetAllEmployeeQuery();
            var employees = await _mediator.Send(getCommand);
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto request)
        {
            var getCommand = new CreateEmployeeCommand()
            {
                EmployeeDto = request
            };
            var employee = await _mediator.Send(getCommand);
            return Ok(employee);
        }

        //[HttpPut]
        //[Route("Update")]
        //public async Task<IActionResult> UpdateEmp(UpdateEmpRequestDto request)
        //{
        //}
        //[HttpDelete]
        //[Route("Delete")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //}
    }
}
