using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddressApi.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PhoneNumber");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "PhoneNumber");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "PhoneNumber");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PhoneNumber",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "PhoneNumber",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "PhoneNumber",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Emails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Address",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1398ff0d-2062-4594-33d4-08dac5f97924"),
                column: "UpdatedOn",
                value: null);
        }
    }
}
