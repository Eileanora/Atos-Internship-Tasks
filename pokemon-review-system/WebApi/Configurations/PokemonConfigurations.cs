using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configurations;

public class PokemonConfigurations : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(p => p.PokemonCategories)
            .WithOne(pc => pc.Pokemon)
            .HasForeignKey(pc => pc.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.PokemonOwners)
            .WithOne(po => po.Pokemon)
            .HasForeignKey(po => po.PokemonId);
    }
}
