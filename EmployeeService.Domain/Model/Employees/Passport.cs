using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record Passport : ValueObject
{
	private readonly PassportNumber _number = null!;

	private readonly PassportType _type = null!;

	public Passport(PassportNumber number, PassportType type)
	{
		Number = number;
		Type = type;
	}

	public PassportNumber Number
	{
		get => _number;
		private init
		{
			ArgumentNullException.ThrowIfNull(value);

			_number = value;
		}
	}

	public PassportType Type
	{
		get => _type;
		private init
		{
			ArgumentNullException.ThrowIfNull(value);

			_type = value;
		}
	}
}