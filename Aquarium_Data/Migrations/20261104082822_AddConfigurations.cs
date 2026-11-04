using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aquarium_Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exhibits",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "A vibrant display of coral reefs and tropical fish.", false, "Coral Reef Exhibit" },
                    { 2, "A showcase of freshwater species from rivers and lakes.", false, "Freshwater Exhibit" },
                    { 3, "An exploration of the mysterious deep sea creatures.", false, "Deep Sea Exhibit" }
                });

            migrationBuilder.InsertData(
                table: "Tanks",
                columns: new[] { "Id", "IsDeleted", "Location", "Name", "Volume" },
                values: new object[,]
                {
                    { 1, false, "Main Hall", "Tropical Tank", 100.0 },
                    { 2, false, "Freshwater Section", "Freshwater Tank", 150.0 },
                    { 3, false, "Deep Sea Section", "Deep Sea Tank", 200.0 }
                });

            migrationBuilder.InsertData(
                table: "Fish",
                columns: new[] { "Id", "Age", "Name", "Species", "TankId" },
                values: new object[,]
                {
                    { 1, 2, "Clownfish", "Amphiprioninae", 1 },
                    { 2, 1, "Guppy", "Poecilia reticulata", 2 },
                    { 3, 3, "Angelfish", "Pterophyllum scalare", 1 }
                });

            migrationBuilder.InsertData(
                table: "TanksExhibits",
                columns: new[] { "ExhibitId", "TankId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TanksExhibits",
                keyColumns: new[] { "ExhibitId", "TankId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TanksExhibits",
                keyColumns: new[] { "ExhibitId", "TankId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "TanksExhibits",
                keyColumns: new[] { "ExhibitId", "TankId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Exhibits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exhibits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exhibits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
