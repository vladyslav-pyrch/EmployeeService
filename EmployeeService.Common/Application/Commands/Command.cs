using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Common.Application.Commands;

public abstract class Command<TIdentity> : ICommand<TIdentity> where TIdentity : IIdentity
{
    private readonly TIdentity _identity = default!;

    protected Command(TIdentity identity)
    {
        Identity = identity;
    }

    public TIdentity Identity
    {
        get => _identity;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _identity = value;
        }
    }
}

public abstract class Command<TIdentity, TResult> : ICommand<TIdentity, TResult> where TIdentity : IIdentity
{
    private readonly TIdentity _identity = default!;

    protected Command(TIdentity identity)
    {
        Identity = identity;
    }

    public TIdentity Identity
    {
        get => _identity;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _identity = value;
        }
    }
}