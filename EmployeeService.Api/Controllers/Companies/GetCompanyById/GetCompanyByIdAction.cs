using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetCompanyById;
using EmployeeService.Domain.Model.Companies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.GetCompanyById;

[ApiController]
public class GetCompanyByIdAction : ExtendedControllerBase
{
	private readonly GetCompanyByIdQueryHandler _getCompanyByIdQueryHandler;

	public GetCompanyByIdAction(GetCompanyByIdQueryHandler getCompanyByIdQueryHandler)
	{
		_getCompanyByIdQueryHandler = getCompanyByIdQueryHandler;
	}

	[HttpGet("api/Company/GetCompanyById")]
	[ProducesResponseType(typeof(GetCompanyByIdResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Invoke([FromQuery] GetCompanyByIdRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		Company company = _getCompanyByIdQueryHandler.Handle(
			new GetCompanyByIdQuery(new CompanyId(request.CompanyId.Value))
		);

		var companyDto = new CompanyDto(company.Identity.Deconvert(), company.Name);

		return Ok(new GetCompanyByIdResponse(companyDto));
	}
}