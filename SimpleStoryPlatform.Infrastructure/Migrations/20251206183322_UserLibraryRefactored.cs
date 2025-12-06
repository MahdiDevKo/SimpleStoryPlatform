using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserLibraryRefactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Library",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "StoryUser",
                columns: table => new
                {
                    InLibraryOfId = table.Column<int>(type: "int", nullable: false),
                    LibraryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryUser", x => new { x.InLibraryOfId, x.LibraryId });
                    table.ForeignKey(
                        name: "FK_StoryUser_Stories_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Stories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoryUser_Users_InLibraryOfId",
                        column: x => x.InLibraryOfId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryUser_LibraryId",
                table: "StoryUser",
                column: "LibraryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryUser");

            migrationBuilder.AddColumn<string>(
                name: "Library",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Library",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Library",
                value: "[]");
        }
    }
}
