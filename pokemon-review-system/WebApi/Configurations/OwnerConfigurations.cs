using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configurations;

public class OwnerConfigurations : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.Property(o => o.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.Gym)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(o => o.Country)
            .WithMany(c => c.Owners)
            .HasForeignKey(o => o.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.PokemonOwners)
            .WithOne(po => po.Owner)
            .HasForeignKey(po => po.OwnerId);
    }
}
