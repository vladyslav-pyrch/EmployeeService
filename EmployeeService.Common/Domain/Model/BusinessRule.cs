namespace EmployeeService.Common.Domain.Model;

public abstract class BusinessRule<TEntity> : IBusinessRule<TEntity> where TEntity : IEntity<IIdentity>
{
	public abstract string Message { get; }

	public abstract void Check(TEntity entity);

	protected void Throw() => throw new BusinessRuleException(GetType().Name, Message);
}