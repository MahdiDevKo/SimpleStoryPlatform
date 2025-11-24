using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingNewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BanReason", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsBan", "IsDeleted", "LastName", "Library", "Password", "PublicId", "Role", "UnBanDate", "Username" },
                values: new object[] { 2, null, new DateTime(2646, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "myOskoolAdmin@gmail.com", "Ali", false, false, "BaBaHaji", null, "12341234", new Guid("22222222-2222-2222-2222-222222222222"), "admin", null, "AdminAli" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
