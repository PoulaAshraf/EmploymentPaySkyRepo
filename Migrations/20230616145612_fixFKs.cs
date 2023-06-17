using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentApi.Migrations
{
    /// <inheritdoc />
    public partial class fixFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[VacancyApplications] DROP CONSTRAINT [FK_VacancyApplications_Vacancy_VacancyId]");
            migrationBuilder.Sql("ALTER TABLE [dbo].[VacancyApplications] ADD CONSTRAINT [FK_VacancyApplications_Vacancy_VacancyId] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancy] ([VacancyId]) ON DELETE CASCADE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[VacancyApplications] DROP CONSTRAINT [FK_VacancyApplications_Vacancy_VacancyId]");
            migrationBuilder.Sql("ALTER TABLE [dbo].[VacancyApplications] ADD CONSTRAINT [FK_VacancyApplications_Vacancy_VacancyId] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancy] ([VacancyId]) ON DELETE NO ACTION");
        }
    }
}
