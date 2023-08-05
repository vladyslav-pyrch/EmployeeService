using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetDepartmentOfEmployee;
using EmployeeService.Application.Employees.GetAllEmployeeOfCompany;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromCompany;

[ApiController]
public class GetAllEmployeesFromCompanyAction : ExtendedControllerBase
{
	private readonly GetAllEmployeeOfCompanyQueryHandler _getAllEmployeeOfCompanyQueryHandler;

	public GetAllEmployeesFromCompanyAction(GetAllEmployeeOfCompanyQueryHandler getAllEmployeeOfCompanyQueryHandler)
	{
		_getAllEmployeeOfCompanyQueryHandler = getAllEmployeeOfCompanyQueryHandler;
	}

	[HttpGet("api/Employee/GetAllEmployeesFromCompany")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(GetAllEmployeesFromCompanyResponse), StatusCodes.Status200OK)]
	public IActionResult Invoke([FromQuery] GetAllEmployeesFromCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var getAllEmployeesOfCompanyQuery = new GetAllEmployeeOfCompanyQuery(
			new CompanyId(request.CompanyId!.Value)
		);

		List<Employee> employees = _getAllEmployeeOfCompanyQueryHandler.Handle(
			getAllEmployeesOfCompanyQuery
		);

		List<EmployeeDto> employeeDtos = employees.Select(GetEmployeeDto).ToList();

		return Ok(new GetAllEmployeesFromCompanyResponse(employeeDtos));
	}
}