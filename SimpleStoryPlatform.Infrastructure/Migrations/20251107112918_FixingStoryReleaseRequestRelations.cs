using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoryPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingStoryReleaseRequestRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryReportId",
                table: "StoryReleaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoryReleaseRequests_StoryReportId",
                table: "StoryReleaseRequests",
                column: "StoryReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryReleaseRequests_StoryReports_StoryReportId",
                table: "StoryReleaseRequests",
                column: "StoryReportId",
                principalTable: "StoryReports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryReleaseRequests_StoryReports_StoryReportId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_StoryReleaseRequests_StoryReportId",
                table: "StoryReleaseRequests");

            migrationBuilder.DropColumn(
                name: "StoryReportId",
                table: "StoryReleaseRequests");
        }
    }
}
