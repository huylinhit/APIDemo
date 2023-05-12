using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDemo.Migrations
{
    public partial class CityInfoContextUpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterest_City_CityId",
                table: "PointOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointOfInterest",
                table: "PointOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.RenameTable(
                name: "PointOfInterest",
                newName: "PointOfInterests");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_PointOfInterest_CityId",
                table: "PointOfInterests",
                newName: "IX_PointOfInterests_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointOfInterests",
                table: "PointOfInterests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointOfInterests",
                table: "PointOfInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "PointOfInterests",
                newName: "PointOfInterest");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameIndex(
                name: "IX_PointOfInterests_CityId",
                table: "PointOfInterest",
                newName: "IX_PointOfInterest_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointOfInterest",
                table: "PointOfInterest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterest_City_CityId",
                table: "PointOfInterest",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
