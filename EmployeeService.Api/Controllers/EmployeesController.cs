using EmployeeService.Api.Contracts.Requests;
using EmployeeService.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeesController : ExtendedControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(AddEmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddEmployee(AddEmployeeRequest request)
    {
        if (!ModelState.IsValid)
            return new BadRequestResult();
        
        //todo...

        return Created(new AddEmployeeResponse { EmployeeId = 111 });
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteEmployee(DeleteEmployeeRequest request)
    {
        return Ok();
    }


    [HttpGet]
    [ProducesResponseType(typeof(GetEmployeesFromDepartmentResponse),StatusCodes.Status200OK)]
    public IActionResult GetEmployeesFromDepartment(GetEmployeesFromDepartmentRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateEmployee(UpdateEmployeeRequest request)
    {
        throw new NotImplementedException();
    }

}