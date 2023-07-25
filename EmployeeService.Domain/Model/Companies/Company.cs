using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Domain.Model.Companies;

public class Company : IEntity<CompanyId>
{
    private readonly CompanyId _identity = null!;

    private List<Department> _departments = null!;

    private string _name = null!;

    public Company(CompanyId identity, string name, List<Department>? departments = null)
    {
        Identity = identity;
        Name = name;
        Departments = departments ?? new List<Department>();
    }

    public CompanyId Identity
    {
        get => _identity;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _identity = value;
        }
    }
    
    public string Name
    {
        get => _name;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _name = value;
        }
    }

    public List<Department> Departments
    {
        get => _departments.ToList();
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.Any(department => department.CompanyId != Identity))
                throw new ArgumentException("Some Departments' CompanyIds are not equal to Identity");

            _departments = value;
        }
    }
}