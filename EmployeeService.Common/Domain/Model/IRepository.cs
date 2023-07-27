namespace EmployeeService.Common.Domain.Model;

public interface IRepository<TEntity, TIdentity>
    where TEntity : IEntity<TIdentity>
    where TIdentity : IIdentity
{ //Todo Remove this Shit
    public TIdentity GetNewId();

    public TEntity GetById(TIdentity identity);

    public void Save();
}