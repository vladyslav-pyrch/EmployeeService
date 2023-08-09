using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetAllDepartmentsFromCompany;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.GetAllDepartmentsFromCompany;

public class GetAllDepartmentsFromCompanyAction : ExtendedControllerBase
{
	private readonly GetAllDepartmentsFromCompanyQueryHandler _getAllDepartmentsFromCompanyQueryHandler;

	public GetAllDepartmentsFromCompanyAction(
		GetAllDepartmentsFromCompanyQueryHandler getAllDepartmentsFromCompanyQueryHandler)
	{
		_getAllDepartmentsFromCompanyQueryHandler = getAllDepartmentsFromCompanyQueryHandler;
	}

	[HttpGet("api/Company/GetAllDepartmentsFromCompany")]
	[ProducesResponseType(typeof(GetAllDepartmentsFromCompanyResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] GetAllDepartmentsFromCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		List<DepartmentDto> departmentDtos = _getAllDepartmentsFromCompanyQueryHandler.Handle(
			new GetAllDepartmentsFromCompanyQuery(new CompanyId(request.CompanyId.Value))
		).Select(department => new DepartmentDto(
				department.Identity.Deconvert(),
				department.Name,
				department.PhoneNumber.Number
			)
		).ToList();
		
		return Ok(new GetAllDepartmentsFromCompanyResponse(departmentDtos));
	}
}