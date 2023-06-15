using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentApi.Migrations
{
    /// <inheritdoc />
    public partial class edit_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyApplications_Applicant_ApplicantId1",
                table: "VacancyApplications");

            migrationBuilder.DropIndex(
                name: "IX_VacancyApplications_ApplicantId1",
                table: "VacancyApplications");

            migrationBuilder.DropColumn(
                name: "ApplicantId1",
                table: "VacancyApplications");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "VacancyApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyApplications_ApplicantId",
                table: "VacancyApplications",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyApplications_Applicant_ApplicantId",
                table: "VacancyApplications",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyApplications_Applicant_ApplicantId",
                table: "VacancyApplications");

            migrationBuilder.DropIndex(
                name: "IX_VacancyApplications_ApplicantId",
                table: "VacancyApplications");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "VacancyApplications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantId1",
                table: "VacancyApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyApplications_ApplicantId1",
                table: "VacancyApplications",
                column: "ApplicantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyApplications_Applicant_ApplicantId1",
                table: "VacancyApplications",
                column: "ApplicantId1",
                principalTable: "Applicant",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
