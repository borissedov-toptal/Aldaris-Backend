using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aldaris.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOrginallySuggestedAnswerField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginallySupposedSolution",
                table: "GameSessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginallySupposedSolution",
                table: "GameSessions");
        }
    }
}
