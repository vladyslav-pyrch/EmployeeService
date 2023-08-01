namespace EmployeeService.Application.Employees.GetAllEmployeeOfCompany;

public record EmployeeDto(int Id, string Name, string Surname, string PhoneNumber, string PassportNumber,
    string PassportType, int DepartmentId, int CompanyId);