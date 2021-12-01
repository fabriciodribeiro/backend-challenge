using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.UserName)
                .HasColumnName("UserName")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasColumnName("Password")
                .HasColumnType("nvarchar")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(t => t.Salt)
                .HasColumnName("Salt")
                .HasColumnType("nvarchar")
                .HasMaxLength(2000);
        }
    }
}
