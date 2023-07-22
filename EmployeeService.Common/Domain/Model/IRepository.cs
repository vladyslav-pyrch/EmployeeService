namespace EmployeeService.Common.Domain.Model;

public interface IRepository<TEntity> where TEntity : IEntity<IIdentity>
{ }