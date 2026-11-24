using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aquarium_Data.Migrations
{
    /// <inheritdoc />
    public partial class NewConfigChanges_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TanksExhibits",
                table: "TanksExhibits");

            migrationBuilder.DropIndex(
                name: "IX_TanksExhibits_ExhibitId",
                table: "TanksExhibits");

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 1);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TanksExhibits",
                table: "TanksExhibits",
                columns: new[] { "ExhibitId", "TankId" });

            migrationBuilder.UpdateData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "Name", "Species", "TankId" },
                values: new object[] { 2, "Clownfish", "Amphiprioninae", 1 });

            migrationBuilder.UpdateData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "Name", "Species", "TankId" },
                values: new object[] { 1, "Guppy", "Poecilia reticulata", 2 });

            migrationBuilder.InsertData(
                table: "Fish",
                columns: new[] { "Id", "Age", "Name", "Species", "TankId" },
                values: new object[] { 4, 3, "Angelfish", "Pterophyllum scalare", 1 });

            migrationBuilder.UpdateData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Volume" },
                values: new object[] { "Reef Tank", 120.0 });

            migrationBuilder.UpdateData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Location", "Name", "Volume" },
                values: new object[] { "Main Hall", "Tropical Tank", 100.0 });

            migrationBuilder.UpdateData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Location", "Name", "Volume" },
                values: new object[] { "Freshwater Section", "Freshwater Tank", 150.0 });

            migrationBuilder.InsertData(
                table: "Tanks",
                columns: new[] { "Id", "IsDeleted", "Location", "Name", "Volume" },
                values: new object[] { 4, false, "Deep Sea Section", "Deep Sea Tank", 200.0 });

            migrationBuilder.InsertData(
                table: "TanksExhibits",
                columns: new[] { "ExhibitId", "TankId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TanksExhibits_TankId",
                table: "TanksExhibits",
                column: "TankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TanksExhibits",
                table: "TanksExhibits");

            migrationBuilder.DropIndex(
                name: "IX_TanksExhibits_TankId",
                table: "TanksExhibits");

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 4);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TanksExhibits",
                table: "TanksExhibits",
                columns: new[] { "TankId", "ExhibitId" });

            migrationBuilder.UpdateData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "Name", "Species", "TankId" },
                values: new object[] { 1, "Guppy", "Poecilia reticulata", 2 });

            migrationBuilder.UpdateData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Age", "Name", "Species", "TankId" },
                values: new object[] { 3, "Angelfish", "Pterophyllum scalare", 1 });

            migrationBuilder.InsertData(
                table: "Fish",
                columns: new[] { "Id", "Age", "Name", "Species", "TankId" },
                values: new object[] { 1, 2, "Clownfish", "Amphiprioninae", 1 });

            migrationBuilder.UpdateData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Volume" },
                values: new object[] { "Tropical Tank", 100.0 });

            migrationBuilder.UpdateData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Location", "Name", "Volume" },
                values: new object[] { "Freshwater Section", "Freshwater Tank", 150.0 });

            migrationBuilder.UpdateData(
                table: "Tanks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Location", "Name", "Volume" },
                values: new object[] { "Deep Sea Section", "Deep Sea Tank", 200.0 });

            migrationBuilder.InsertData(
                table: "TanksExhibits",
                columns: new[] { "ExhibitId", "TankId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TanksExhibits_ExhibitId",
                table: "TanksExhibits",
                column: "ExhibitId");
        }
    }
}
