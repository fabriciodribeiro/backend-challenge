using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Mappings
{
    public class PortfolioMap : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("Portfolio");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.AccountId)
                .HasColumnName("AccountId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(t => t.Created)
                .HasColumnName("Created")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(t => t.Modified)
                .HasColumnName("Modified")
                .HasColumnType("datetime");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Portfolios)
                .HasConstraintName("FK_Portfolio_Account");
        }
    }
}
