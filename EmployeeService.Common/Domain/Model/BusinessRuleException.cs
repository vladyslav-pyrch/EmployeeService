namespace EmployeeService.Common.Domain.Model;

public class BusinessRuleException : Exception
{
	public BusinessRuleException(string businessRuleName, string details): base(details)
	{
		BusinessRuleName = businessRuleName;
	}

	private string BusinessRuleName { get; }

	public override string ToString() => $"{BusinessRuleName}: {Message}";
}