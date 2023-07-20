using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.DataAccess.Model.Configuration;

public class PassportConfiguration : IEntityTypeConfiguration<Passport>
{
    public void Configure(EntityTypeBuilder<Passport> builder)
    {
        builder.HasKey(passport => passport.Id);
        builder.Property(passport => passport.Number)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasOne<PassportType>()
            .WithMany()
            .HasForeignKey(passport => passport.PassportTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<Employee>()
            .WithOne()
            .HasForeignKey<Employee>(employee => employee.PassportId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade); // Not sure about it.
    }
}