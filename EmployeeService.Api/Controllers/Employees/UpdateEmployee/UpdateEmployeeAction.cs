using EmployeeService.Application.Employees.GetPassportByEmployeeId;
using EmployeeService.Application.Employees.GetWorkplaceByEmployeeId;
using EmployeeService.Application.Employees.UpdateEmployee;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.UpdateEmployee;

[ApiController]
public class UpdateEmployeeAction : ExtendedControllerBase
{
	private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;

	private readonly GetPassportByEmployeeIdQueryHandler _getPassportByEmployeeIdQueryHandler;

	private readonly GetWorkplaceByEmployeeIdQueryHandler _getWorkplaceByEmployeeIdQueryHandler;

	public UpdateEmployeeAction(UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
		GetPassportByEmployeeIdQueryHandler getPassportByEmployeeIdQueryHandler,
		GetWorkplaceByEmployeeIdQueryHandler getWorkplaceByEmployeeIdQueryHandler)
	{
		_updateEmployeeCommandHandler = updateEmployeeCommandHandler;
		_getPassportByEmployeeIdQueryHandler = getPassportByEmployeeIdQueryHandler;
		_getWorkplaceByEmployeeIdQueryHandler = getWorkplaceByEmployeeIdQueryHandler;
	}

	[HttpPatch("api/Employee/UpdateEmployee")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromBody] UpdateEmployeeRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		UpdateEmployeeCommand updateEmployeeCommand = CreateUpdateEmployeeCommand(request);
		
		_updateEmployeeCommandHandler.Handle(updateEmployeeCommand);

		return Ok();
	}

	private UpdateEmployeeCommand CreateUpdateEmployeeCommand(UpdateEmployeeRequest request)
	{
		var id = Guid.NewGuid();
		var employeeId = new EmployeeId(request.Id.Value);
		string? name = request.Name;
		string? surname = request.Surname;
		PhoneNumber? phoneNumber = null;
		Passport? passport = null;
		Workplace? workplace = null;

		if (request.PhoneNumber != null)
			phoneNumber = new PhoneNumber(request.PhoneNumber);
		
		if (request.PassportType != null || request.PassportNumber != null)
			passport = MakeUpdatedPassport(request, employeeId);
		
		if (request.CompanyId != null || request.DepartmentId != null)
			workplace = MakeUpdatedWorkplace(request, employeeId);

		return new UpdateEmployeeCommand(id, employeeId)
		{
			Name = name, Surname = surname, PhoneNumber = phoneNumber, Passport = passport, Workplace = workplace
		};
	}

	private Passport GetOldPassport(EmployeeId employeeId)
	{
		var getPassportByEmployeeId = new GetPassportByEmployeeIdQuery(employeeId);
			
		return _getPassportByEmployeeIdQueryHandler.Handle(getPassportByEmployeeId);
	}

	private Workplace GetOldWorkplace(EmployeeId employeeId)
	{
		var getWorkplaceByEmployeeId = new GetWorkplaceByEmployeeIdQuery(employeeId);
			
		return _getWorkplaceByEmployeeIdQueryHandler.Handle(getWorkplaceByEmployeeId);
		
	}

	private Passport MakeUpdatedPassport(UpdateEmployeeRequest request, EmployeeId employeeId)
	{
		Passport oldPassport = GetOldPassport(employeeId);

		return new Passport(
			request.PassportNumber != null ? new PassportNumber(request.PassportNumber) : oldPassport.Number,
			request.PassportType != null ? new PassportType(request.PassportType) : oldPassport.Type
		);
	}

	private Workplace MakeUpdatedWorkplace(UpdateEmployeeRequest request, EmployeeId employeeId)
	{
		Workplace oldWorkplace = GetOldWorkplace(employeeId);

		return new Workplace(
			request.CompanyId != null ? new CompanyId(request.CompanyId.Value) : oldWorkplace.Company,
			request.DepartmentId != null ? new DepartmentId(request.DepartmentId.Value) : oldWorkplace.Department
		);
	}
}