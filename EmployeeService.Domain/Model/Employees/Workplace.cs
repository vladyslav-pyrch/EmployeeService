using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Domain.Model.Employees;

public record Workplace : ValueObject
{
	private readonly CompanyId _company = null!;

	private readonly DepartmentId _department = null!;

	public Workplace(CompanyId company, DepartmentId department)
	{
		Company = company;
		Department = department;
	}

	public CompanyId Company
	{
		get => _company;
		private init
		{
			ArgumentNullException.ThrowIfNull(value);

			_company = value;
		}
	}

	public DepartmentId Department
	{
		get => _department;
		private init
		{
			ArgumentNullException.ThrowIfNull(value);

			_department = value;
		}
	}
}