using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetupStudentLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentFavoriteCourses_CourseId",
                table: "StudentFavoriteCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFavoriteCourses_StudentId",
                table: "StudentFavoriteCourses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonScores_LessonId",
                table: "LessonScores",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonScores_StudentId",
                table: "LessonScores",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonScores_Lessons_LessonId",
                table: "LessonScores",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonScores_Users_StudentId",
                table: "LessonScores",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFavoriteCourses_Courses_CourseId",
                table: "StudentFavoriteCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFavoriteCourses_Users_StudentId",
                table: "StudentFavoriteCourses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonScores_Lessons_LessonId",
                table: "LessonScores");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonScores_Users_StudentId",
                table: "LessonScores");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFavoriteCourses_Courses_CourseId",
                table: "StudentFavoriteCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFavoriteCourses_Users_StudentId",
                table: "StudentFavoriteCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentFavoriteCourses_CourseId",
                table: "StudentFavoriteCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentFavoriteCourses_StudentId",
                table: "StudentFavoriteCourses");

            migrationBuilder.DropIndex(
                name: "IX_LessonScores_LessonId",
                table: "LessonScores");

            migrationBuilder.DropIndex(
                name: "IX_LessonScores_StudentId",
                table: "LessonScores");
        }
    }
}
