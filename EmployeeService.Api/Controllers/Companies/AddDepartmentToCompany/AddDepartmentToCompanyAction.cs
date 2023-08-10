using EmployeeService.Application.Companies.AddDepartmentToCompany;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.AddDepartmentToCompany;

[ApiController]
public class AddDepartmentToCompanyAction : ExtendedControllerBase
{
	private readonly AddDepartmentToCompanyCommandHandler _addDepartmentToCompanyCommandHandler;

	public AddDepartmentToCompanyAction(AddDepartmentToCompanyCommandHandler addDepartmentToCompanyCommandHandler)
	{
		_addDepartmentToCompanyCommandHandler = addDepartmentToCompanyCommandHandler;
	}

	[HttpPost("api/Company/AddDepartmentToCompany")]
	[ProducesResponseType(typeof(AddDepartmentToCompanyResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromBody] AddDepartmentToCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var addDepartmentToCompanyCommand = new AddDepartmentToCompanyCommand(
			Guid.NewGuid(),
			new CompanyId(request.CompanyId.Value),
			request.DepartmentName,
			new PhoneNumber(request.DepartmentPhoneNumber)
		);

		DepartmentId departmentId = _addDepartmentToCompanyCommandHandler.Handle(addDepartmentToCompanyCommand);

		return Created(new AddDepartmentToCompanyResponse(departmentId.Deconvert()));
	}
}