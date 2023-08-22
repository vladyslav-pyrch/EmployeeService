using System.Text.Json.Serialization;
using EmployeeService.Api.Contracts;

namespace EmployeeService.Api.Controllers.Companies.GetAllCompanies;

public record GetAllCompaniesResponse(List<CompanyDto> Companies);