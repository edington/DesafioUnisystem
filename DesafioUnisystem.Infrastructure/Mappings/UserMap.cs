using DesafioUnisystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioUnisystem.Infrastructure.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(a => a.Name)
               .HasMaxLength(100);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                 .HasColumnName("Email")
                 .IsRequired();
        });

        builder.OwnsOne(u => u.Password, email =>
        {
            email.Property(e => e.Hash)
                 .HasColumnName("Password")
                 .IsRequired();
        });
    }
}
