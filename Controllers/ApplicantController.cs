using AutoMapper;
using EmploymentApi.Core.Contracts;
using EmploymentApi.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmploymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public ApplicantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllApplications")]
        [Authorize(Roles = "Employer")] 
        public Task<AppliedVacancyDTO> GetApplicationsByVacancy(int id)
        {
            try
            {
                return _unitOfWork.VacancyApplication.GetApplicationsByVacancy(id);
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }

        [HttpPost("AddApplication")]
        [Authorize(Roles = "Applicant")]
        public string ApplyOnVacancy(int vacancy)
        {
            string userId = HttpContext.Items["UserId"] as string;
            
            try
            {
                if (ModelState.IsValid)
                {
                    return _unitOfWork.VacancyApplication.ApplyOnVacancy(vacancy, userId);
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
    }
}
