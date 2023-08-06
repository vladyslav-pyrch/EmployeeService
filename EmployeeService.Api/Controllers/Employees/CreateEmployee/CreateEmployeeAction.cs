using EmployeeService.Application.Employees.CreateEmployee;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.CreateEmployee;

[ApiController]
public class CreateEmployeeAction : ExtendedControllerBase
{
	private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;

	public CreateEmployeeAction(CreateEmployeeCommandHandler createEmployeeCommandHandler) =>
		_createEmployeeCommandHandler = createEmployeeCommandHandler;

	[HttpPost("api/Employee/CreateEmployee")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CreateEmployeeResponse), StatusCodes.Status201Created)]
	public IActionResult Invoke([FromBody] CreateEmployeeRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		EmployeeId employeeId = _createEmployeeCommandHandler.Handle(
			MadeCreateEmployeeCommand(request)
		); //todo Handle BusinessRuleExceptions!!! 

		return Created(
			new CreateEmployeeResponse(employeeId.Deconvert())
		);
	}

	private static CreateEmployeeCommand MadeCreateEmployeeCommand(CreateEmployeeRequest request)
	{
		var id = Guid.NewGuid();
		var passport = new Passport(
			new PassportNumber(request.PassportNumber),
			new PassportType(request.PassportType)
		);
		var phoneNumber = new PhoneNumber(request.PhoneNumber);
		var workplace = new Workplace(
			new CompanyId(request.CompanyId.Value),
			new DepartmentId(request.DepartmentId.Value)
		);

		return new CreateEmployeeCommand(
			id, request.Name, request.Surname, passport, phoneNumber, workplace
		);
	}
}