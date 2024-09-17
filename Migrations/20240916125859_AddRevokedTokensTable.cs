using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PerformanceSurvey.Migrations
{
    /// <inheritdoc />
    public partial class AddRevokedTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_responses_question_Option_QuestionOptionOptionId",
                table: "responses");

            migrationBuilder.DropIndex(
                name: "IX_responses_QuestionOptionOptionId",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "QuestionOptionOptionId",
                table: "responses");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "responses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthToken = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    IssuedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_responses_OptionId",
                table: "responses",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_responses_question_Option_OptionId",
                table: "responses",
                column: "OptionId",
                principalTable: "question_Option",
                principalColumn: "OptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_responses_question_Option_OptionId",
                table: "responses");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_responses_OptionId",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "responses");

            migrationBuilder.AddColumn<int>(
                name: "QuestionOptionOptionId",
                table: "responses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_responses_QuestionOptionOptionId",
                table: "responses",
                column: "QuestionOptionOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_responses_question_Option_QuestionOptionOptionId",
                table: "responses",
                column: "QuestionOptionOptionId",
                principalTable: "question_Option",
                principalColumn: "OptionId");
        }
    }
}
