using EmployeeService.Application.Companies.CreateCompany;
using EmployeeService.Domain.Model.Companies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.CreateCompany;

[ApiController]
public class CreateCompanyAction : ExtendedControllerBase
{
	private readonly CreateCompanyCommandHandler _createCompanyCommandHandler;

	public CreateCompanyAction(CreateCompanyCommandHandler createCompanyCommandHandler) =>
		_createCompanyCommandHandler = createCompanyCommandHandler;

	[HttpPost("api/Company/CreateCompany")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CreateCompanyResponse), StatusCodes.Status201Created)]
	public IActionResult Invoke([FromBody] CreateCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var createCompanyCommand = new CreateCompanyCommand(
			Guid.NewGuid(), request.Name, request.PhoneNumber
		);

		CompanyId companyId = _createCompanyCommandHandler.Handle(createCompanyCommand);

		return Created(new CreateCompanyResponse(companyId.Deconvert()));
	}
}