using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Domain.Model.Companies.Departments;

public class Department : Entity<DepartmentId>
{
    private readonly CompanyId _companyId = null!;

    private List<EmployeeId> _employeeIds = null!;

    private string _name = null!;

    private PhoneNumber _phoneNumber = null!;

    public Department(DepartmentId identity, string name, PhoneNumber phoneNumber, CompanyId companyId,
        List<EmployeeId>? employeeIds = null) : base(identity)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        CompanyId = companyId;
        EmployeeIds = employeeIds ?? new List<EmployeeId>();
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

    public PhoneNumber PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _phoneNumber = value;
        }
    }

    public CompanyId CompanyId
    {
        get => _companyId;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _companyId = value;
        }
    }

    public List<EmployeeId> EmployeeIds
    {
        get => _employeeIds.ToList();
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _employeeIds = value;
        }
    }
}