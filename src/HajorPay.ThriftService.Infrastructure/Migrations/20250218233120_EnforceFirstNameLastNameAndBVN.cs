using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajorPay.ThriftService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnforceFirstNameLastNameAndBVN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true);
        }
    }
}
