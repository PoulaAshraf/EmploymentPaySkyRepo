using EmploymentApi.DTOs;
using EmploymentApi.Models;

namespace EmploymentApi.Contracts
{
    public interface IVacancy 
    {
        string AddVacancy(VecancyDTO vacancy);
        string EditVacancy(int vacancyId, VecancyDTO vacancy);
        string RemoveVacancy(int vacancyId);
        List<Vacancy> GetAllVacancies();
        Vacancy GetVacancyByID(int id);
        List<Vacancy> GetVacancyByName(string title);

    }
}
