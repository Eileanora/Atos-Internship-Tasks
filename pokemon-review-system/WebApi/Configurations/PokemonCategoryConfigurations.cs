using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configurations;

public class PokemonCategoryConfigurations : IEntityTypeConfiguration<PokemonCategory>
{
    public void Configure(EntityTypeBuilder<PokemonCategory> builder)
    {
        builder.HasKey(pc => new { pc.PokemonId, pc.CategoryId });

        builder.HasOne(pc => pc.Pokemon)
            .WithMany(p => p.PokemonCategories)
            .HasForeignKey(pc => pc.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pc => pc.Category)
            .WithMany(c => c.PokemonCategories)
            .HasForeignKey(pc => pc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
