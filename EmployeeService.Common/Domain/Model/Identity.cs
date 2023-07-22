namespace EmployeeService.Common.Domain.Model;

public abstract record Identity : IIdentity
{
    protected Identity(object id)
    {
        Id = id.ToString()
             ?? throw new ArgumentException("Argument id is impossible to convert to string", nameof(id));
    }

    protected Identity(string id)
    {
        Id = id;
    }

    public string Id { get; }
}