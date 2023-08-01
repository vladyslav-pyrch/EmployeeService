using System.Data;

namespace EmployeeService.Common.Application.Data;

public interface ISqlConnectionFactory
{
    public IDbConnection OpenConnection { get; }   
}