using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HajorPay.ThriftService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adjustments2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c564a44-a040-4359-88f2-5d6fb13834e9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4a0cc4b4-288b-408b-a198-071d95ff9066"));

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("29863d6f-3158-4c0f-96e7-ee32b584f229"), null, "ADMIN", "ADMIN" },
                    { new Guid("44b5e03a-0f0a-43dd-8643-0c95fc60c050"), null, "USER", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("29863d6f-3158-4c0f-96e7-ee32b584f229"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("44b5e03a-0f0a-43dd-8643-0c95fc60c050"));

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2c564a44-a040-4359-88f2-5d6fb13834e9"), null, "ADMIN", "ADMIN" },
                    { new Guid("4a0cc4b4-288b-408b-a198-071d95ff9066"), null, "USER", "USER" }
                });
        }
    }
}
