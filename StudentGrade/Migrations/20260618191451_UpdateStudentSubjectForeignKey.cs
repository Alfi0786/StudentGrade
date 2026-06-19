using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGrade.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentSubjectForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectKey",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectKey",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
