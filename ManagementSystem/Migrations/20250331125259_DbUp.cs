using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class DbUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citizens_Reports_ReportId",
                table: "Citizens");

            migrationBuilder.DropIndex(
                name: "IX_Citizens_ReportId",
                table: "Citizens");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Citizens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Citizens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_ReportId",
                table: "Citizens",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citizens_Reports_ReportId",
                table: "Citizens",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "ReportId");
        }
    }
}
