namespace EmployeeService.Common.Domain.Model;

public interface IBusinessRule<in TEntity> where TEntity : IEntity<IIdentity>
{
	public string Message { get; }
	
	/// <summary>
	///		Checks if an entity is valid according to the business rule.
	/// </summary>
	/// <exception cref="BusinessRuleException">
	///		Throws a <see cref="BusinessRuleException"/> if an entity fails to pass the business rule.
	/// </exception>
	/// <param name="entity">
	///		An entity to check the validity of.
	/// </param>
	public void Check(TEntity entity);
}