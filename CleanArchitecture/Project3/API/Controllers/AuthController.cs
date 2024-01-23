using Application.DTOs.Auth;
using Application.DTOs.Employee;
using Application.DTOs.Response.AuthResponse;
using Application.Employee.Commands.CreateEmployee;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(IMediator mediator, UserManager<IdentityUser> userManager, IJwtService jwtService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("EmployeeRegister")]
        public async Task<IActionResult> EmployeeRegister([FromBody] CreateEmployeeDto request)
        {
            if (ModelState.IsValid)
            {
                var userExiting = await _userManager.FindByEmailAsync(request.Email);
                if (userExiting != null)
                {
                    return BadRequest("Email is already exits !");
                }

                var newUser = new IdentityUser()
                {
                    Email = request.Email,
                    UserName = request.Email
                };

                var isCreate = await _userManager.CreateAsync(newUser, request.Password);

                if (isCreate.Succeeded)
                {
                    var createEmployeeCommand = new CreateEmployeeCommand
                    {
                        EmployeeDto = request
                    };
                    var isAddRole = await _userManager.AddToRoleAsync(newUser, "Employee");
                    if (isAddRole.Succeeded)
                    {
                        await _mediator.Send(createEmployeeCommand);
                        var token = await _jwtService.GenerateJwtTokenAsync(newUser);
                        return Ok(
                            new EmployeeRegisterResponse()
                            {
                                Result = true,
                                Token = token.ToString()
                            }
                        );
                    }
                    return BadRequest(isAddRole.Errors.Select(x => x.Description).ToList());
                }
                return BadRequest(isCreate.Errors.Select(x => x.Description).ToList());
            }
            return BadRequest("Invalid request payload");
        }


        [HttpPost]
        [Route("Emp-Login")]
        public async Task<IActionResult> EmpLogin([FromBody] LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser == null)
                {
                    return BadRequest("Invalid authentication");
                }
                var roles = await _userManager.GetRolesAsync(existingUser);
                if (!roles.Contains("Admin") && !roles.Contains("Employee"))
                {
                    return BadRequest("You don't have the required roles (Admin or Employee) to perform this action.");
                }
                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);
                if (isPasswordValid)
                {
                    var token = await _jwtService.GenerateJwtTokenAsync(existingUser);
                    return Ok(new LoginResponseDto()
                    {
                        Result = true,
                        Token = token.ToString()
                    }); ;
                }
            }
            return BadRequest("Invalid request payload !");
        }
    }
}
