using AutoMapper;
using EmploymentApi.Contracts;
using EmploymentApi.DTOs;
using EmploymentApi.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentApi.Services
{
    public class VacancyApplicationService : Base<VacancyApplications>, IVacancyApplication
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private static readonly object _lock = new object();
        public VacancyApplicationService(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppliedVacancyDTO> GetApplicationsByVacancy(int id)
        {
            try
            {
                var applications = FindAll(v => v.VacancyId == id, new[] { "Applicant", "Vacancy" } );
                AppliedVacancyDTO applicationDetails= new AppliedVacancyDTO();

                applicationDetails.JobDescription = applications[0].Vacancy.JobDescription;
                applicationDetails.JobTitle = applications[0].Vacancy.JobTitle;
                applicationDetails.JobType = applications[0].Vacancy.JobType;
                applicationDetails.NoOfApplied = applications[0].Vacancy.NoOfApplied;
                applicationDetails.PostedDate = applications[0].Vacancy.PostedDate;
                List<ApplicantDTO> applicants=new();
                foreach ( var application in applications) 
                {
                    var applicantUser = await _userManager.FindByIdAsync(application.ApplicantId);
                    var applicant=new ApplicantDTO()
                    { 
                        Qualification = application.Applicant.Qualification, 
                        Email = applicantUser.Email,
                        Username= applicantUser.UserName
                    };
                    applicants.Add(applicant);
                }
                applicationDetails.Applicants= applicants;
                return applicationDetails;
            }
            catch
            {
                Log.ForContext("CustomMessage", "Get All Vacancy Exception")
                .Error("Get All Vacancy Failed");
                return null;
            }
        }

        public string ApplyOnVacancy(int vacancyId, string applicantId)
        {
            try
            {
                DateTime lastAppliedDate= FindAll(app => app.ApplicantId == applicantId).OrderByDescending(app => app.AppliedDate).FirstOrDefault().AppliedDate;
                if ((DateTime.Now - lastAppliedDate).TotalHours < 24 )
                {
                    return "You can't apply before 24 hours from last applied time";
                }
                lock (_lock)
                {
                    var vacancy = _context.Vacancy.SingleOrDefault(vac => vac.VacancyId == vacancyId);
                    if (vacancy.NoOfApplied >= vacancy.MaxApplicant)
                    {
                        return "This vacancy reach max number of applications";
                    }
                    VacancyApplications vac = new VacancyApplications() { ApplicantId = applicantId, VacancyId = vacancyId, AppliedDate = DateTime.Now };
                    Insert(vac);

                    vacancy.NoOfApplied = vacancy.NoOfApplied + 1;
                    _context.Update(vacancy);
                    _context.SaveChanges();
                    return "You're applied";
                }

            }
            catch
            {
                Log.ForContext("CustomMessage", "Exception Insert")
                .Error("Application Failed");
                return "Saved failed";
            }
        }
    }
}
