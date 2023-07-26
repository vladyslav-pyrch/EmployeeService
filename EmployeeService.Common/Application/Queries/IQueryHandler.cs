namespace EmployeeService.Common.Application.Queries;

public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
{
    public TResult Handle(TQuery query);
}