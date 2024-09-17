using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerformanceSurvey.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTextFromResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the 'Text' column from the 'Response' table
            migrationBuilder.DropColumn(
                name: "Text",
                table: "responses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add the 'Text' column back to the 'Response' table
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "responses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }

}

