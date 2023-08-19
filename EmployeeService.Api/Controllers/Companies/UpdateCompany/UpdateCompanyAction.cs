using EmployeeService.Application.Companies.UpdateCompany;
using EmployeeService.Domain.Model.Companies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.UpdateCompany;

[ApiController]
public class UpdateCompanyAction : ExtendedControllerBase
{
	private readonly UpdateCompanyCommandHandler _updateCompanyCommandHandler;

	public UpdateCompanyAction(UpdateCompanyCommandHandler updateCompanyCommandHandler)
	{
		_updateCompanyCommandHandler = updateCompanyCommandHandler;
	}

	[HttpPatch("api/Company/UpdateCompany")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromBody] UpdateCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var updateCompanyCommand = new UpdateCompanyCommand(
			Guid.NewGuid(), new CompanyId(request.CompanyId.Value)
		)
		{
			Name = request.Name
		};
		
		_updateCompanyCommandHandler.Handle(updateCompanyCommand);

		return Ok();
	}
}