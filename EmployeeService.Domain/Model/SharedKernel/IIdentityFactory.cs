using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.SharedKernel;

public interface IIdentityFactory<out TIdentity> where TIdentity : IIdentity
{
	public TIdentity GenerateId();
}