using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infrastructure.Configurations;
public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(r => r.Content)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.HasOne(r => r.Pokemon)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Reviewer)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.ReviewerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
