using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetAllDepartmentsOfCompany;
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

	private readonly GetDepartmentOfEmployeeQueryHandler _getDepartmentOfEmployeeQueryHandler;

	public GetAllEmployeesFromCompanyAction(GetAllEmployeeOfCompanyQueryHandler getAllEmployeeOfCompanyQueryHandler,
		GetDepartmentOfEmployeeQueryHandler getDepartmentOfEmployeeQueryHandler)
	{
		_getAllEmployeeOfCompanyQueryHandler = getAllEmployeeOfCompanyQueryHandler;
		_getDepartmentOfEmployeeQueryHandler = getDepartmentOfEmployeeQueryHandler;
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

	private EmployeeDto GetEmployeeDto(Employee employee)
	{
		var getDepartmentOfEmployeeQuery = new GetDepartmentOfEmployeeQuery(
			employee.Identity
		);

		Department department = _getDepartmentOfEmployeeQueryHandler.Handle(
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
			department.Name,
			department.PhoneNumber.Number
		);

		return new EmployeeDto(id, name, surname, phoneNumber, companyId, passportDto, departmentDto);
	}
}