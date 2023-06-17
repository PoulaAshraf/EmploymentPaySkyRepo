using AutoMapper;
using EmploymentApi.Core.Contracts;
using EmploymentApi.Core.DTOs;
using Data.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmploymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class VacancyController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public VacancyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ArchiveExpiryVacancies")]
        public String ArchiveExpiryVacancies()
        {
            //Recurring Job - this job is executed many times on the specified cron schedule
            RecurringJob.AddOrUpdate(() => _unitOfWork.Vacancy.ArchivingExpiredVacancies(), Cron.Daily);

            return "offer sent!";
        }


        [HttpPost("AddVacancy")]
        [Authorize(Roles = "Employer")]
        public string AddVacancy(VecancyDTO vacancy)
        {
            string userId = HttpContext.Items["UserId"] as string;
            vacancy.Id = userId;
            try
            {
                if (ModelState.IsValid)
                {
                    return _unitOfWork.Vacancy.AddVacancy(vacancy);
                }
                return "Vacancy must be not null";
            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Insert")
                .Error("Vacancy insertion Failed");
                return "Saved failed";
            }
        }

        [HttpPut("EditVacancy")]
        [Authorize(Roles = "Employer")]
        public string EditVacancy(int vacancyId, VecancyDTO vacancy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return _unitOfWork.Vacancy.EditVacancy(vacancyId, vacancy);
                }
                return "Vacancy is not valid";
            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Update")
                .Error("Vacancy edition Failed");
                return "Saved failed";
            }
        }

        [HttpPut("DeactivateVacancy")]
        [Authorize(Roles = "Employer")]
        public string RemoveVacancy(int vacancyId)
        {
            try
            {
                return _unitOfWork.Vacancy.RemoveVacancy(vacancyId);

            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Update")
                .Error("Vacancy deactivation Failed");
                return "Deactivation failed";
            }
        }

        [HttpGet("GetAllVacancies")]
        [Authorize(Roles = "Applicant, Employer")]
        public List<Vacancy> GetAllVacancies()
        {
            try
            {
                return _unitOfWork.Vacancy.GetAllVacancies();
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }

        [HttpGet("GetVacancyByID")]
        [Authorize(Roles = "Applicant, Employer")]
        public Vacancy GetVacancyByID(int id)
        {
            try
            {
                return _unitOfWork.Vacancy.GetVacancyByID(id);
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }

        [HttpGet("GetVacancyByTitle")]
        [Authorize(Roles = "Applicant, Employer")]
        public List<Vacancy> GetVacancyByName(string title)
        {
            try
            {
                return _unitOfWork.Vacancy.GetVacancyByName(title);
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Job title Vacancy Failed");
                return null;
            }
        }

        
    }
}
