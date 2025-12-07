using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewReportEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequests_Stories_StoryId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequests_StoryReports_StoryReportId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequests_Users_TargetUserId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryReviews_Users_UserId",
                table: "StoryReviews");

            migrationBuilder.DropIndex(
                name: "IX_StoryReviews_UserId",
                table: "StoryReviews");

            migrationBuilder.DropIndex(
                name: "IX_StoryReleaseRequests_StoryReportId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_StoryReleaseRequests_TargetUserId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StoryReviews");

            migrationBuilder.DropColumn(
                name: "ReportText",
                table: "StoryReleaseRequests");

            migrationBuilder.DropColumn(
                name: "StoryReportId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "StoryReleaseRequests");

            migrationBuilder.AddColumn<string>(
                name: "RequestMessage",
                table: "StoryReleaseRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StoryReleaseRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoryReleaseRequests_UserId",
                table: "StoryReleaseRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_Stories_StoryId",
                table: "StoryReleaseRequests",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_Users_UserId",
                table: "StoryReleaseRequests",
                column: "UserId",
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
                name: "FK_StoryReleaseRequests_Users_UserId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_StoryReleaseRequests_UserId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropColumn(
                name: "RequestMessage",
                table: "StoryReleaseRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StoryReleaseRequests");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StoryReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportText",
                table: "StoryReleaseRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoryReportId",
                table: "StoryReleaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetUserId",
                table: "StoryReleaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoryReviews_UserId",
                table: "StoryReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReleaseRequests_StoryReportId",
                table: "StoryReleaseRequests",
                column: "StoryReportId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReleaseRequests_TargetUserId",
                table: "StoryReleaseRequests",
                column: "TargetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_Stories_StoryId",
                table: "StoryReleaseRequests",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_StoryReports_StoryReportId",
                table: "StoryReleaseRequests",
                column: "StoryReportId",
                principalTable: "StoryReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_Users_TargetUserId",
                table: "StoryReleaseRequests",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReviews_Users_UserId",
                table: "StoryReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
