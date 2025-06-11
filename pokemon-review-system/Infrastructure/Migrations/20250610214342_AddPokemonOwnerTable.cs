using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonOwnerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwner_Owners_OwnerId",
                table: "PokemonOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwner_Pokemons_PokemonId",
                table: "PokemonOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonOwner",
                table: "PokemonOwner");

            migrationBuilder.RenameTable(
                name: "PokemonOwner",
                newName: "PokemonOwners");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonOwner_OwnerId",
                table: "PokemonOwners",
                newName: "IX_PokemonOwners_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners",
                columns: new[] { "PokemonId", "OwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners");

            migrationBuilder.RenameTable(
                name: "PokemonOwners",
                newName: "PokemonOwner");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonOwners_OwnerId",
                table: "PokemonOwner",
                newName: "IX_PokemonOwner_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonOwner",
                table: "PokemonOwner",
                columns: new[] { "PokemonId", "OwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwner_Owners_OwnerId",
                table: "PokemonOwner",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwner_Pokemons_PokemonId",
                table: "PokemonOwner",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
