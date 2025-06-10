using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infrastructure.Configurations;

public class ReviewerConfigurations : IEntityTypeConfiguration<Reviewer>
{
    public void Configure(EntityTypeBuilder<Reviewer> builder)
    {
        builder.Property(r => r.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(r => r.Reviews)
            .WithOne(rev => rev.Reviewer)
            .HasForeignKey(rev => rev.ReviewerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
