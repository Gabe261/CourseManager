using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Instructor = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Instructor", "Name", "RoomNumber", "StartDate" },
                values: new object[,]
                {
                    { 1, "David", "See Sharp", "4G15", new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Ryan", "Sequel", "2C09", new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Owen", "GitHub", "1G15", new DateTime(2023, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Liam", "Web Dynamics", "4G18", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Eddy", "Game Programming", "3B11", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "CourseId", "Status", "StudentEmail", "StudentName" },
                values: new object[,]
                {
                    { 1, 1, 0, "gabed.siewert@hotmail.com", "John" },
                    { 2, 1, 0, "gsiewert2384@conestogac.on.ca", "Greg" },
                    { 3, 2, 0, "gabesiewert@hotmail.com", "Simon" },
                    { 4, 2, 0, "gabed.siewert@hotmail.com", "Thomas" },
                    { 5, 3, 0, "gabed.siewert@hotmail.com", "Jason" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
