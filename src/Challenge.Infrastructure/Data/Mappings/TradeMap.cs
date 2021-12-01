using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Mappings
{
    public class TradeMap : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.ToTable("Trade");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Executor)
                .HasColumnName("Executor")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(t => t.Created)
                .HasColumnName("Created")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(t => t.Modified)
                .HasColumnName("Modified")
                .HasColumnType("datetime");

            builder.Property(t => t.PortfolioId)
                .HasColumnName("PortfolioId")
                .HasColumnType("uniqueidentifier");

            builder.HasOne(x => x.Portfolio)
                .WithMany(x => x.Trades)
                .HasConstraintName("FK_Trade_Portfolio");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Trades)
                .HasConstraintName("FK_Trade_Account");
        }
    }
}
