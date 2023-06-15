using EmploymentApi.DTOs;
using EmploymentApi.Models;

namespace EmploymentApi.Contracts
{
    public interface IVacancyApplication
    {
        Task<AppliedVacancyDTO> GetApplicationsByVacancy(int id);
        string ApplyOnVacancy(int vacancyId, string applicantId);
    }
}
