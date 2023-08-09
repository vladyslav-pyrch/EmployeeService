using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Companies.AddDepartmentToCompany;

public class AddDepartmentToCompanyCommand : Command<DepartmentId>
{
	public AddDepartmentToCompanyCommand(Guid id, CompanyId companyId, string departmentName,
		PhoneNumber departmentPhoneNumber) : base(id)
	{
		CompanyId = companyId;
		DepartmentName = departmentName;
		DepartmentPhoneNumber = departmentPhoneNumber;
	}

	public CompanyId CompanyId { get; }
	
	public string DepartmentName { get; }
	
	public PhoneNumber DepartmentPhoneNumber { get; }
}