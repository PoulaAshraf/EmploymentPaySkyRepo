using AutoMapper;
using EmploymentApi.Core.Contracts;
using EmploymentApi.Core.DTOs;
using EmploymentApi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Serilog;
using System;

namespace EmploymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public EmployerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[HttpPost("AddVacancy")]
        //public string AddVacancy(VecancyDTO vacancy)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            return _unitOfWork.Vacancy.AddVacancy(vacancy);
        //        }
        //        return "Vacancy must be not null";
        //    }
        //    catch
        //    {
        //        Log.ForContext("CustomMessage", "Exception Insert")
        //        .Error("Vacancy insertion Failed");
        //        return "Saved failed";
        //    }
        //}

        //[HttpPut("EditVacancy")]
        //public string EditVacancy(VecancyDTO vacancy, int vacancyId)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _unitOfWork.Vacancy.EditVacancy(vacancy, vacancyId);
        //        }
        //        return "Vacancy is not valid";
        //    }
        //    catch
        //    {
        //        Log.ForContext("CustomMessage", "Exception Update")
        //        .Error("Vacancy edition Failed");
        //        return "Saved failed";
        //    }
        //}

        //[HttpPut("RemoveVacancy")]
        //public string RemoveVacancy(int vacancyId)
        //{
        //    try
        //    {
        //        return _unitOfWork.Vacancy.RemoveVacancy(vacancyId);
                
        //    }
        //    catch
        //    {
        //        Log.ForContext("CustomMessage", "Exception Update")
        //        .Error("Vacancy deactivation Failed");
        //        return "Deactivation failed";
        //    }
        //}

        //[HttpGet("GetAllVacancies")]
        //public List<Vacancy> GetAllVacancies()
        //{
        //    try
        //    {
        //        return _unitOfWork.Vacancy.GetAllVacancies();
        //    }
        //    catch
        //    {
        //        Log.ForContext("CustomMessage", "Get All Vacancy Exception")
        //        .Error("Get All Vacancy Failed");
        //        return null;
        //    }
        //}

        //[HttpGet("GetVacancyByID")]
        //public Vacancy GetVacancyByID(int id)
        //{
        //    try
        //    {
        //        return _unitOfWork.Vacancy.GetVacancyByID(id);
        //    }
        //    catch
        //    {
        //        Log.ForContext("CustomMessage", "Get All Vacancy Exception")
        //        .Error("Get All Vacancy Failed");
        //        return null;
        //    }
        //}

        //[HttpGet("GetVacancyByTitle")]
        //public List<Vacancy> GetVacancyByName(string title)
        //{
        //    try
        //    {
        //        return _unitOfWork.Vacancy.GetVacancyByName(title);
        //    }
        //    catch
        //    {
        //        Log.ForContext("CustomMessage", "Get All Vacancy Exception")
        //        .Error("Job title Vacancy Failed");
        //        return null;
        //    }
        //}
    }
}
