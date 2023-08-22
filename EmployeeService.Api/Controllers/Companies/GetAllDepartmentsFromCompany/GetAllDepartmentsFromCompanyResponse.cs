using EmployeeService.Api.Contracts;

namespace EmployeeService.Api.Controllers.Companies.GetAllDepartmentsFromCompany;

public record GetAllDepartmentsFromCompanyResponse(List<DepartmentDto> Departments);