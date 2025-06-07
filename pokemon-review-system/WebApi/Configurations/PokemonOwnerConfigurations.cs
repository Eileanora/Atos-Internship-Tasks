using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configurations;

public class PokemonOwnerConfigurations : IEntityTypeConfiguration<PokemonOwner>
{
    public void Configure(EntityTypeBuilder<PokemonOwner> builder)
    {
        builder.HasKey(po => new { po.PokemonId, po.OwnerId });

        builder.HasOne(po => po.Pokemon)
            .WithMany(p => p.PokemonOwners)
            .HasForeignKey(po => po.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(po => po.Owner)
            .WithMany(o => o.PokemonOwners)
            .HasForeignKey(po => po.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
