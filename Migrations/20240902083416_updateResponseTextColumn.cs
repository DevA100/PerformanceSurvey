using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerformanceSurvey.Migrations
{
    /// <inheritdoc />
    public partial class updateResponseTextColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "responses");

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "responses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "responses");

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "responses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
