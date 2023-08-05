using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Companies;

internal record DepartmentDto
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string PhoneNumber { get; set; }

	public int CompanyId { get; set; }

	public static explicit operator Department(DepartmentDto departmentDto)
	{
		var id = new DepartmentId(departmentDto.Id);
		string name = departmentDto.Name;
		var phoneNumber = new PhoneNumber(departmentDto.PhoneNumber);
		var companyId = new CompanyId(departmentDto.CompanyId);

		return new Department(id, name, phoneNumber, companyId);
	}
}