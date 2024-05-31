using AgriEnergyConnect.Domain.Shared;
using AgriEnergyConnect.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriEnergyConnect.Infrastructure.EFConfigurations;

public sealed class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(user => user.Id); 
        
        builder.Property(user => user.FirstName)
            .HasMaxLength(200)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));

        builder.Property(user => user.LastName)
            .HasMaxLength(200)
            .HasConversion(firstName => firstName.Value, value => new LastName(value));

        builder.Property(user => user.Photo) 
            .HasConversion(photo => photo.Value, value => new Photo(value)); 
        
        builder.Property(user => user.Email)
            .HasMaxLength(400) 
            .HasConversion(email => email.Value, value => new Domain.Users.Email(value));
         
        builder.HasMany(user => user.Products)
            .WithOne(product => product.Farmer)
            .HasForeignKey(product => product.FarmerId);

        builder.HasIndex(user => user.Email).IsUnique();

        builder.HasIndex(user => user.IdentityId).IsUnique();
         
    }
}