using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingSomeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetUserGuid",
                table: "StoryReports");

            migrationBuilder.DropColumn(
                name: "TargetUserGuid",
                table: "ReviewReports");

            migrationBuilder.AddColumn<int>(
                name: "TargetUserId",
                table: "StoryReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetUserId",
                table: "ReviewReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoryReports_TargetUserId",
                table: "StoryReports",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReports_TargetUserId",
                table: "ReviewReports",
                column: "TargetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewReports_Users_TargetUserId",
                table: "ReviewReports",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReports_Users_TargetUserId",
                table: "StoryReports",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewReports_Users_TargetUserId",
                table: "ReviewReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryReports_Users_TargetUserId",
                table: "StoryReports");

            migrationBuilder.DropIndex(
                name: "IX_StoryReports_TargetUserId",
                table: "StoryReports");

            migrationBuilder.DropIndex(
                name: "IX_ReviewReports_TargetUserId",
                table: "ReviewReports");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "StoryReports");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "ReviewReports");

            migrationBuilder.AddColumn<Guid>(
                name: "TargetUserGuid",
                table: "StoryReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TargetUserGuid",
                table: "ReviewReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
