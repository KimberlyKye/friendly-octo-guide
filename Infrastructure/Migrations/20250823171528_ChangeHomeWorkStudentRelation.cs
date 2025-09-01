using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHomeWorkStudentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_StudentCourses_StudentId",
                table: "HomeWorks");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Users_StudentId",
                table: "HomeWorks",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Users_StudentId",
                table: "HomeWorks");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_StudentCourses_StudentId",
                table: "HomeWorks",
                column: "StudentId",
                principalTable: "StudentCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
