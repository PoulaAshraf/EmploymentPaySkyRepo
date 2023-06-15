using AutoMapper;
using EmploymentApi.Contracts;
using EmploymentApi.DTOs;
using EmploymentApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Cryptography.Xml;

namespace EmploymentApi.Services
{
    public class VacancyRepo : Base<Vacancy>,IVacancy
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public VacancyRepo(ApplicationDbContext context, IMapper mapper):base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public string AddVacancy(VecancyDTO vacancy)
        {
            try
            {
                    var vac = _mapper.Map<Vacancy>(vacancy);
                    Insert(vac);
                    return "Vacancy Added Successfuly";
                
            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Insert")
                .Error("Vacancy insertion Failed");
                return "Saved failed";
            }
        }

        public string EditVacancy(int vacancyId, VecancyDTO vacancy)
        {
            try
            {
                var vac = GetById(vacancyId);
                if (vac != null)
                {
                    vac.ExpiryDate = vacancy.ExpiryDate;
                    vac.JobDescription = vacancy.JobDescription;
                    vac.JobType = vacancy.JobType;
                    vac.JobTitle = vacancy.JobTitle;
                    vac.JobTitle = vacancy.JobTitle;
                    vac.MaxApplicant = vacancy.MaxApplicant;
                    Update(vac);
                    return "Vacancy Updated Successfuly";
                }
                else
                    return "Vacancy is not found";
            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Update")
                .Error("Vacancy edition Failed");
                return "Saved failed";
            }
        }

        public string RemoveVacancy(int vacancyId)
        {
            try
            {
                Vacancy vac = GetById(vacancyId);
                if (vac != null)
                {
                    vac.IsActive = false;
                   Update(vac);
                    return "Vacancy is inactive";
                }
                else
                    return "Vacancy is not found";
            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Update")
                .Error("Vacancy deactivation Failed");
                return "Deactivation failed";
            }
        }

        public List<Vacancy> GetAllVacancies()
        {
            try
            {
                //return GetAll().ToList();
                return FindAll(vac=>vac.IsArchived == false && vac.IsActive == true).ToList();
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }

        public Vacancy GetVacancyByID(int id)
        {
            try
            {
                return GetById(id);
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }

        public List<Vacancy> GetVacancyByName(string title)
        {
            try
            {
                return FindAll(v => v.JobTitle==title);
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }
    }
}
