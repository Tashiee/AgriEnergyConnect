using AgriEnergyConnect.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriEnergyConnect.Infrastructure.EFConfigurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        
        builder.HasKey(role => role.Id);
        
        builder.Property(role => role.Id)
            .ValueGeneratedNever();
        
        builder.HasMany(role=> role.Users)
            .WithOne(u=> u.Role)
            .HasForeignKey(u=> u.RoleId);

        builder.HasData(Role.Employee, Role.Farmer); 
    }
}