using Application.DTOs.Employee;
using Application.Employee.Commands.CreateEmployee;
using Application.Employee.Queries.GetAllEmployee;
using Application.Employee.Queries.GetByName;
using Application.Employee.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeQuery = new GetAllEmployeeQuery();
            var employees = await _mediator.Send(employeeQuery);
            return Ok(employees);
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery()
            {
                Id = id
            };
            var employee = await _mediator.Send(query);
            if (employee != null)
            {

                return Ok(employee);
            }
            return BadRequest($"Employee {id} not found!");

        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetEmployeeByName([FromQuery] string name)
        {
            try
            {

                string firstName = name.Split(' ')[0];
                string lastName = name.Split(' ')[1];
                var query = new GetEmployeeByNameQuery()
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                var employee = await _mediator.Send(query);

                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound($"Employee {name} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
    }
}
