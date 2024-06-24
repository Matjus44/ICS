using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PhotoUri = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CodeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Finish = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Room = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityEntities_SubjectEntities_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SubjectEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsSubjectsEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsSubjectsEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsSubjectsEntities_StudentEntities_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsSubjectsEntities_SubjectEntities_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SubjectEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingEntities_ActivityEntities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ActivityEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingEntities_StudentEntities_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntities_SubjectId",
                table: "ActivityEntities",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingEntities_ActivityId",
                table: "RatingEntities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingEntities_StudentId",
                table: "RatingEntities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSubjectsEntities_StudentId",
                table: "StudentsSubjectsEntities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSubjectsEntities_SubjectId",
                table: "StudentsSubjectsEntities",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingEntities");

            migrationBuilder.DropTable(
                name: "StudentsSubjectsEntities");

            migrationBuilder.DropTable(
                name: "ActivityEntities");

            migrationBuilder.DropTable(
                name: "StudentEntities");

            migrationBuilder.DropTable(
                name: "SubjectEntities");
        }
    }
}
