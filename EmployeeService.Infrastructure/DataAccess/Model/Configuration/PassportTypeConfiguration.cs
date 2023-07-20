using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.DataAccess.Model.Configuration;

public class PassportTypeConfiguration : IEntityTypeConfiguration<PassportType>
{
    public void Configure(EntityTypeBuilder<PassportType> builder)
    {
        builder.HasKey(type => type.Id);
        builder.Property(type => type.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasMany<Passport>()
            .WithOne()
            .HasForeignKey(passport => passport.PassportTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}