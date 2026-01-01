using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdisonSchoolTelegramBot.Migrations
{
    /// <inheritdoc />
    public partial class AddTextQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectTextAnswer",
                table: "TestQuestions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionType",
                table: "TestQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "TestAttemptAnswers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "TestAttemptAnswers",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "TextAnswer",
                table: "TestAttemptAnswers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectTextAnswer",
                table: "TestQuestions");

            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "TestQuestions");

            migrationBuilder.DropColumn(
                name: "TextAnswer",
                table: "TestAttemptAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "TestAttemptAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "TestAttemptAnswers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
