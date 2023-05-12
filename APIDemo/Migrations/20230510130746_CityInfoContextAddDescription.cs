using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDemo.Migrations
{
    public partial class CityInfoContextAddDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "a");

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "a");

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "a");

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "a");

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "a");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: null);
        }
    }
}
