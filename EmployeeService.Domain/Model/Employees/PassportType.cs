using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record PassportType : ValueObject
{
    private readonly string _name = null!;

    public PassportType(string name)
    {
        Name = name;
    }

    public string Name
    {
        get => _name;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _name = value;
        }
    }
}