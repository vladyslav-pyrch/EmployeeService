using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetDepartmentOfEmployee;
using EmployeeService.Application.Employees.GetEmployeeById;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.GetEmployeeById;

[ApiController]
public class GetEmployeeByIdAction : ExtendedControllerBase
{
	private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;

	private readonly GetDepartmentOfEmployeeQueryHandler _getDepartmentOfEmployeeQueryHandler;

	public GetEmployeeByIdAction(GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler,
		GetDepartmentOfEmployeeQueryHandler getDepartmentOfEmployeeQueryHandler)
	{
		_getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
		_getDepartmentOfEmployeeQueryHandler = getDepartmentOfEmployeeQueryHandler;
	}

	[HttpGet("api/Employee/GetEmployeeById")]
	[ProducesResponseType(typeof(GetEmployeeByIdResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] GetEmployeeByIdRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var employeeId = new EmployeeId(request.EmployeeId.Value);

		Department department = _getDepartmentOfEmployeeQueryHandler.Handle(
			new GetDepartmentOfEmployeeQuery(employeeId)
		);

		Employee employee = _getEmployeeByIdQueryHandler.Handle(
			new GetEmployeeByIdQuery(employeeId)
		);

		var departmentDto = new DepartmentDto(
			department.Identity.Deconvert(), department.Name, department.PhoneNumber.Number
		);

		var passportDto = new PassportDto(
			employee.Passport.Type.Name, employee.Passport.Number.Number
		);

		var employeeDto = new EmployeeDto(
			employee.Identity.Deconvert(),
			employee.Name,
			employee.Surname,
			employee.PhoneNumber.Number,
			employee.Workplace.Company.Deconvert(),
			passportDto,
			departmentDto
		);

		return Ok(new GetEmployeeByIdResponse(employeeDto));
	}
}