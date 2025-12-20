using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsReadedToWarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReaded",
                table: "Warnings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReaded",
                table: "Warnings");
        }
    }
}
