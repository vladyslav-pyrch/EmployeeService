using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record PassportNumber : ValueObject
{
    private readonly string _number = null!;

    public PassportNumber(string number)
    {
        Number = number;
    }

    public string Number
    {
        get => _number;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _number = value;
        }
    }
}