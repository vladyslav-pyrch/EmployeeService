using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies;

public interface ICompanyRepository : IRepository
{
	public void CreateCompany(Company company);

	public void DeleteCompanyById(CompanyId companyId);
}