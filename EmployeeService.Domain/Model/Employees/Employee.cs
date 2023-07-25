using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Domain.Model.Employees;

public class Employee : Entity<EmployeeId>
{
    private string _name = null!;

    private Passport _passport = null!;

    private PhoneNumber _phoneNumber = null!;

    private string _surname = null!;

    private Workplace _workplace = null!;

    public Employee(EmployeeId identity, string name, string surname, Passport passport, PhoneNumber phoneNumber,
        Workplace workplace) : base(identity)
    {
        Name = name;
        Surname = surname;
        Passport = passport;
        PhoneNumber = phoneNumber;
        Workplace = workplace;
    }

    public string Name
    {
        get => _name;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            if (!IsCapitalised(value))
                throw new ArgumentException("Name should start from capital letter.");

            if (HasSpaces(value))
                throw new ArgumentException("Name should not have any spaces.");

            _name = value;
        }
    }


    public string Surname
    {
        get => _surname;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            if (!IsCapitalised(value))
                throw new ArgumentException("Surname should start from capital letter.");

            if (HasSpaces(value))
                throw new ArgumentException("Surname should not have any spaces.");

            _surname = value;
        }
    }

    public Passport Passport
    {
        get => _passport;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _passport = value;
        }
    }

    public PhoneNumber PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _phoneNumber = value;
        }
    }

    public Workplace Workplace
    {
        get => _workplace;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _workplace = value;
        }
    }

    private static bool IsCapitalised(string properNoun)
    {
        return char.IsUpper(properNoun[0]);
    }

    private static bool HasSpaces(string word)
    {
        return word.Any(c => c == ' ');
    }
}