using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdisonSchoolTelegramBot.Migrations
{
    /// <inheritdoc />
    public partial class AddTelegramUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "TelegramUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "TelegramUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "TelegramUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "TelegramUsers");
        }
    }
}
