using EmploymentApi.Core.Contracts;
using EmploymentApi.Core.DTOs;
using EmploymentApi.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmploymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[AllowAnonymous]
        //[HttpPost("Register")]
        //public async Task<AuthDTO> Register(RegisterDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new AuthDTO { Message = "Not Valid" };
        //    }
        //    return await _unitOfWork.AccountService.RegisterAsync(model);
        //}

        [AllowAnonymous]
        [HttpPost("EmployerRegister")]
        public async Task<AuthDTO> EmployerRegister(EmployerDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new AuthDTO { Message = "Not Valid" };
            }
            return await _unitOfWork.AccountService.EmployerRegisterAsync(model);
        }

        [AllowAnonymous]
        [HttpPost("ApplicantRegister")]
        public async Task<AuthDTO> ApplicantRegister(ApplicantRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new AuthDTO { Message = "Not Valid" };
            }
            return await _unitOfWork.AccountService.ApplicantRegisterAsync(model);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<AuthDTO> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new AuthDTO { Message = "Not Valid" };
            }

            return await _unitOfWork.AccountService.Login(model);
        }
    }
}
