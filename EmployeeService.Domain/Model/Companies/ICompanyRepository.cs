using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Domain.Model.Companies;

public interface ICompanyRepository : IRepository<Company, CompanyId>, IRepository<Department, DepartmentId>
{
}