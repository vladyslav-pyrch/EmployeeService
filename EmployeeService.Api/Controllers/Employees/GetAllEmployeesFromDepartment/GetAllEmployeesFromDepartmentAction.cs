using EmployeeService.Api.Contracts;
using EmployeeService.Application.Employees.GetAllEmployeesFromDepartment;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromDepartment;

[ApiController]
public class GetAllEmployeesFromDepartmentAction : ExtendedControllerBase
{
	private readonly GetAllEmployeesFromDepartmentQueryHandler _getAllEmployeesFromDepartmentQueryHandler;

	public GetAllEmployeesFromDepartmentAction(
		GetAllEmployeesFromDepartmentQueryHandler getAllEmployeesFromDepartmentQueryHandler) =>
		_getAllEmployeesFromDepartmentQueryHandler = getAllEmployeesFromDepartmentQueryHandler;

	[HttpGet("api/Employee/GetAllEmployeesFromDepartment")]
	[ProducesResponseType(typeof(GetAllEmployeesFromDepartmentResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] GetAllEmployeesFromDepartmentRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var getAllEmployeeFromDepartmentQuery = new GetAllEmployeesFromDepartmentQuery(
			new DepartmentId(request.DepartmentId.Value)
		);

		List<Employee> employees = _getAllEmployeesFromDepartmentQueryHandler.Handle(
			getAllEmployeeFromDepartmentQuery
		);

		List<EmployeeDto> employeeDtos = employees.Select(GetEmployeeDto).ToList();

		return Ok(new GetAllEmployeesFromDepartmentResponse(employeeDtos));
	}
}