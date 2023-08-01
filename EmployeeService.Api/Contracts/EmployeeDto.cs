namespace EmployeeService.Api.Contracts;

public record EmployeeDto(int Id, string Name, string Surname, string Phone, int CompanyId, PassportDto Passport,
	DepartmentDto Department);