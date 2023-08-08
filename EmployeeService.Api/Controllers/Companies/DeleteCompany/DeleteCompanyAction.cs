using EmployeeService.Application.Companies.DeleteCompany;
using EmployeeService.Domain.Model.Companies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.DeleteCompany;

public class DeleteCompanyAction : ExtendedControllerBase
{
	private readonly DeleteCompanyCommandHandler _deleteCompanyCommandHandler;

	public DeleteCompanyAction(DeleteCompanyCommandHandler deleteCompanyCommandHandler) =>
		_deleteCompanyCommandHandler = deleteCompanyCommandHandler;

	[HttpDelete("api/Company/DeleteCompany")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] DeleteCompanyRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var deleteCompanyCommand = new DeleteCompanyCommand(
			Guid.NewGuid(), new CompanyId(request.CompanyId.Value)
		);

		_deleteCompanyCommandHandler.Handle(deleteCompanyCommand);

		return Ok();
	}
}