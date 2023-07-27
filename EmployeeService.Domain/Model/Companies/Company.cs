using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Domain.Model.Companies;

public class Company : Entity<CompanyId>
{
    private List<Department> _departments = null!;

    private string _name = null!;

    public Company(CompanyId identity, string name, List<Department>? departments = null) : base(identity)
    {
        Name = name;
        Departments = departments ?? new List<Department>();
    }
    
    public string Name
    {
        get => _name;
        private set
        {
            ArgumentNullException.ThrowIfNull(value);

            _name = value;
        }
    }

    public List<Department> Departments
    {
        get => _departments.ToList();
        private set
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.Any(department => department.CompanyId != Identity))
                throw new ArgumentException("Some Departments' CompanyIds are not equal to Identity");

            _departments = value;
        }
    }

    public Department AddDepartment(DepartmentId departmentId , string name, PhoneNumber phoneNumber)
    {
        Department department = new(departmentId, name, phoneNumber, Identity);
        
        _departments.Add(department);
        
        AddDomainEvent(new DepartmentAdded(Source));

        return department;
    }

    public void RemoveDepartment(DepartmentId departmentId)
    {
        Department departmentToRemove = GetDepartmentById(departmentId);

        _departments.Remove(departmentToRemove);
        
        AddDomainEvent(new DepartmentRemoved(Source));
    }


    public void ChangeName(string name)
    {
        Name = name;
        
        AddDomainEvent(new CompanysNameChanged(Source));
    }

    public Department GetDepartmentById(DepartmentId departmentId)
    {
        return _departments.FirstOrDefault(
            department => department.Identity == departmentId
        ) ?? throw new InvalidOperationException("There is no such a department in the Company");
    }
}