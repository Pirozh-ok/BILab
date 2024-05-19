using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BILab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateOfDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("378b85d2-f5cb-4607-9393-79d3d7d4f775"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("57c73785-7c3e-434f-8eb9-aa5e2e8419ad"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("77bfaa3f-9b74-4708-aeb6-b684975e4b24"));

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1058dab4-f5da-43f3-b072-a8ed6c8607b2"), null, "Employee", "EMPLOYEE" },
                    { new Guid("db291ef2-45a6-4ac4-841f-84a84f7adf11"), null, "Admin", "ADMIN" },
                    { new Guid("e09317f8-87ab-4024-9048-5779bd03677a"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1058dab4-f5da-43f3-b072-a8ed6c8607b2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("db291ef2-45a6-4ac4-841f-84a84f7adf11"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e09317f8-87ab-4024-9048-5779bd03677a"));

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Schedules");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("378b85d2-f5cb-4607-9393-79d3d7d4f775"), null, "Admin", "ADMIN" },
                    { new Guid("57c73785-7c3e-434f-8eb9-aa5e2e8419ad"), null, "Employee", "EMPLOYEE" },
                    { new Guid("77bfaa3f-9b74-4708-aeb6-b684975e4b24"), null, "User", "USER" }
                });
        }
    }
}
