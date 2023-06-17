using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_AspNetUsers_Id",
                table: "Applicant");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Applicant",
                newName: "ApplicantId");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Vacancy",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Vacancy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "Vacancy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoOfApplied",
                table: "Vacancy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VacancyApplications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacancyId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyApplications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VacancyApplications_Applicant_ApplicantId1",
                        column: x => x.ApplicantId1,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancyApplications_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "VacancyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_Id",
                table: "Vacancy",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyApplications_ApplicantId1",
                table: "VacancyApplications",
                column: "ApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyApplications_VacancyId",
                table: "VacancyApplications",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_AspNetUsers_ApplicantId",
                table: "Applicant",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancy_Employer_Id",
                table: "Vacancy",
                column: "Id",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_AspNetUsers_ApplicantId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancy_Employer_Id",
                table: "Vacancy");

            migrationBuilder.DropTable(
                name: "VacancyApplications");

            migrationBuilder.DropIndex(
                name: "IX_Vacancy_Id",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "NoOfApplied",
                table: "Vacancy");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "Applicant",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_AspNetUsers_Id",
                table: "Applicant",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
