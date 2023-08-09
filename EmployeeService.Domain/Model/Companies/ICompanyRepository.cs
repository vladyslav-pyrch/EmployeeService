using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Domain.Model.Companies;

public interface ICompanyRepository : IRepository
{
	public void CreateCompany(Company company);

	public void DeleteCompanyById(CompanyId companyId);
	
	public void DeleteDepartmentById(DepartmentId departmentId);
	
	public void UpdateCompany(Company company);
	
	void AddDepartment(Department department);
}