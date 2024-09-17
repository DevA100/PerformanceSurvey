using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerformanceSurvey.Migrations
{
    /// <inheritdoc />
    public partial class FixQuestionOptionTextNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_responses_question_Option_DepartmentQuestionOptionOptionId",
                table: "responses");

            migrationBuilder.DropForeignKey(
                name: "FK_responses_users_UserId",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "ScorePercentage",
                table: "responses");

            migrationBuilder.RenameColumn(
                name: "TextResponse",
                table: "responses",
                newName: "QuestionText");

            migrationBuilder.RenameColumn(
                name: "DepartmentQuestionOptionOptionId",
                table: "responses",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_responses_DepartmentQuestionOptionOptionId",
                table: "responses",
                newName: "IX_responses_DepartmentId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "responses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionId",
                table: "responses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionOptionOptionId",
                table: "responses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "responses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_responses_QuestionOptionOptionId",
                table: "responses",
                column: "QuestionOptionOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_responses_departments_DepartmentId",
                table: "responses",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_responses_question_Option_QuestionOptionOptionId",
                table: "responses",
                column: "QuestionOptionOptionId",
                principalTable: "question_Option",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_responses_users_UserId",
                table: "responses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_responses_departments_DepartmentId",
                table: "responses");

            migrationBuilder.DropForeignKey(
                name: "FK_responses_question_Option_QuestionOptionOptionId",
                table: "responses");

            migrationBuilder.DropForeignKey(
                name: "FK_responses_users_UserId",
                table: "responses");

            migrationBuilder.DropIndex(
                name: "IX_responses_QuestionOptionOptionId",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "QuestionOptionOptionId",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "responses");

            migrationBuilder.RenameColumn(
                name: "QuestionText",
                table: "responses",
                newName: "TextResponse");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "responses",
                newName: "DepartmentQuestionOptionOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_responses_DepartmentId",
                table: "responses",
                newName: "IX_responses_DepartmentQuestionOptionOptionId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "responses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<float>(
                name: "ScorePercentage",
                table: "responses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_responses_question_Option_DepartmentQuestionOptionOptionId",
                table: "responses",
                column: "DepartmentQuestionOptionOptionId",
                principalTable: "question_Option",
                principalColumn: "OptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_responses_users_UserId",
                table: "responses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId");
        }
    }
}
