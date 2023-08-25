using EmployeeService.Api.Contracts;
using EmployeeService.Application.Employees.GetAllEmployeesOfCompany;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromCompany;

[ApiController]
public class GetAllEmployeesFromCompanyAction : ExtendedControllerBase
{
	private readonly GetAllEmployeesOfCompanyQueryHandler _getAllEmployeesOfCompanyQueryHandler;

	public GetAllEmployeesFromCompanyAction(GetAllEmployeesOfCompanyQueryHandler getAllEmployeesOfCompanyQueryHandler) =>
		_getAllEmployeesOfCompanyQueryHandler = getAllEmployeesOfCompanyQueryHandler;

	[HttpGet("api/Employee/GetAllEmployeesFromCompany")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(GetAllEmployeesFromCompanyResponse), StatusCodes.Status200OK)]
	public IActionResult Invoke([FromQuery] GetAllEmployeesFromCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var getAllEmployeesOfCompanyQuery = new GetAllEmployeesOfCompanyQuery(
			new CompanyId(request.CompanyId!.Value)
		);

		List<Employee> employees = _getAllEmployeesOfCompanyQueryHandler.Handle(
			getAllEmployeesOfCompanyQuery
		);

		List<EmployeeDto> employeeDtos = employees.Select(GetEmployeeDto).ToList();

		return Ok(new GetAllEmployeesFromCompanyResponse(employeeDtos));
	}
}