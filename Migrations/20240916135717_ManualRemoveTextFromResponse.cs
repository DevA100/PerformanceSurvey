using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerformanceSurvey.Migrations
{
    /// <inheritdoc />
    public partial class ManualRemoveTextFromResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "responses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "responses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
