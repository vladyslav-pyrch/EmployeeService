using EmployeeService.Api.Contracts;
using EmployeeService.Application.Companies.GetAllCompanies;
using EmployeeService.Domain.Model.Companies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers.Companies.GetAllCompanies;

[ApiController]
public class GetAllCompaniesAction : ExtendedControllerBase
{
	private readonly GetAllCompaniesQueryHandler _getAllCompaniesQueryHandler;

	public GetAllCompaniesAction(GetAllCompaniesQueryHandler getAllCompaniesQueryHandler)
	{
		_getAllCompaniesQueryHandler = getAllCompaniesQueryHandler;
	}

	[HttpGet("api/Company/GetAllCompanies")]
	[ProducesResponseType(typeof(GetAllCompaniesResponse),StatusCodes.Status200OK)]
	public IActionResult Invoke()
	{
		List<Company> companies = _getAllCompaniesQueryHandler.Handle(
			new GetAllCompaniesQuery()
		);

		List<CompanyDto> companyDtos = companies.Select(
			company => new CompanyDto(company.Identity.Deconvert(), company.Name)
		).ToList();

		return Ok(new GetAllCompaniesResponse(companyDtos));
	}
}