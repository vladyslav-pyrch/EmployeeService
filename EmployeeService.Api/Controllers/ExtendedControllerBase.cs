using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetDepartmentOfEmployee;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeeService.Api.Controllers;

[Controller]
public abstract class ExtendedControllerBase : ControllerBase
{
	[NonAction]
	protected Uri DisplayUrl() => new(HttpContext.Request.GetDisplayUrl());

	[NonAction]
	protected CreatedResult Created([ActionResultObjectValue] object? value) => Created(DisplayUrl(), value);

	protected EmployeeDto GetEmployeeDto(Employee employee)
	{
		var getDepartmentOfEmployeeQuery = new GetDepartmentOfEmployeeQuery(
			employee.Identity
		);

		var getDepartmentOfEmployeeQueryHandler = HttpContext.RequestServices
			.GetRequiredService<GetDepartmentOfEmployeeQueryHandler>();

		Department department = getDepartmentOfEmployeeQueryHandler.Handle(
			getDepartmentOfEmployeeQuery
		);

		int id = employee.Identity.Deconvert();
		string name = employee.Name;
		string surname = employee.Surname;
		string phoneNumber = employee.PhoneNumber.Number;
		int companyId = employee.Workplace.Company.Deconvert();
		var passportDto = new PassportDto(
			employee.Passport.Type.Name,
			employee.Passport.Number.Number
		);
		var departmentDto = new DepartmentDto(
			department.Identity.Deconvert(),
			department.Name,
			department.PhoneNumber.Number
		);

		return new EmployeeDto(id, name, surname, phoneNumber, companyId, passportDto, departmentDto);
	}
}