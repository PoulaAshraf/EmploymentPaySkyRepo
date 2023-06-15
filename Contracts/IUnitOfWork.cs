using EmploymentApi.Services;

namespace EmploymentApi.Contracts
{
    public interface IUnitOfWork
    {
        IVacancy Vacancy { get; set; }
        IAccountService AccountService { get; set; }
        IVacancyApplication VacancyApplication { get; set; }
        



        int Complete();
    }
}
