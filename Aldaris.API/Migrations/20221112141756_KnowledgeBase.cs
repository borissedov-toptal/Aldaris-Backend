using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aldaris.API.Migrations
{
    /// <inheritdoc />
    public partial class KnowledgeBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VariableName",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClauseType",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClauseValue",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Facts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsequentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_Facts_ConsequentId",
                        column: x => x.ConsequentId,
                        principalTable: "Facts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeBaseRuleAntecedent",
                columns: table => new
                {
                    RuleId = table.Column<int>(type: "int", nullable: false),
                    AntecedentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBaseRuleAntecedent", x => new { x.RuleId, x.AntecedentId });
                    table.ForeignKey(
                        name: "FK_KnowledgeBaseRuleAntecedent_Facts_AntecedentId",
                        column: x => x.AntecedentId,
                        principalTable: "Facts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KnowledgeBaseRuleAntecedent_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBaseRuleAntecedent_AntecedentId",
                table: "KnowledgeBaseRuleAntecedent",
                column: "AntecedentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_ConsequentId",
                table: "Rules",
                column: "ConsequentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnowledgeBaseRuleAntecedent");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Facts");

            migrationBuilder.DropColumn(
                name: "VariableName",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ClauseType",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ClauseValue",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
