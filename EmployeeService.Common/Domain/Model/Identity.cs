namespace EmployeeService.Common.Domain.Model;

public abstract record Identity<T> : ValueObject, IIdentity where T : notnull
{
	protected Identity(T id) =>
		Id = id.ToString()
		     ?? throw new ArgumentException("Argument id is impossible to convert to string", nameof(id));

	protected Identity(string id) => Id = id;

	public string Id { get; }

	public abstract T Deconvert();
}