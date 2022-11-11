using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aldaris.API.Migrations
{
    /// <inheritdoc />
    public partial class RelationsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionAnswer_Answers_AnswerId",
                table: "GameSessionAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "GameSessionAnswer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionAnswer_Answers_AnswerId",
                table: "GameSessionAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionAnswer_Answers_AnswerId",
                table: "GameSessionAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "GameSessionAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionAnswer_Answers_AnswerId",
                table: "GameSessionAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
