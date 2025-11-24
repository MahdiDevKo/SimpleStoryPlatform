using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbSetForStoryReleaseRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequest_Stories_StoryId",
                table: "StoryReleaseRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequest_Users_TargetUserId",
                table: "StoryReleaseRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryReleaseRequest",
                table: "StoryReleaseRequest");

            migrationBuilder.RenameTable(
                name: "StoryReleaseRequest",
                newName: "StoryReleaseRequests");

            migrationBuilder.RenameIndex(
                name: "IX_StoryReleaseRequest_TargetUserId",
                table: "StoryReleaseRequests",
                newName: "IX_StoryReleaseRequests_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryReleaseRequest_StoryId",
                table: "StoryReleaseRequests",
                newName: "IX_StoryReleaseRequests_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryReleaseRequests",
                table: "StoryReleaseRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_Stories_StoryId",
                table: "StoryReleaseRequests",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_Users_TargetUserId",
                table: "StoryReleaseRequests",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequests_Stories_StoryId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequests_Users_TargetUserId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryReleaseRequests",
                table: "StoryReleaseRequests");

            migrationBuilder.RenameTable(
                name: "StoryReleaseRequests",
                newName: "StoryReleaseRequest");

            migrationBuilder.RenameIndex(
                name: "IX_StoryReleaseRequests_TargetUserId",
                table: "StoryReleaseRequest",
                newName: "IX_StoryReleaseRequest_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryReleaseRequests_StoryId",
                table: "StoryReleaseRequest",
                newName: "IX_StoryReleaseRequest_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryReleaseRequest",
                table: "StoryReleaseRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequest_Stories_StoryId",
                table: "StoryReleaseRequest",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequest_Users_TargetUserId",
                table: "StoryReleaseRequest",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
