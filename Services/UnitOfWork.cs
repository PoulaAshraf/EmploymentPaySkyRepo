using AutoMapper;
using EmploymentApi.Contracts;
using EmploymentApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EmploymentApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IVacancy Vacancy { get; set; }
        public IAccountService AccountService { get; set; }
        public IVacancyApplication VacancyApplication { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UnitOfWork(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            Vacancy = new VacancyRepo(_context, mapper);
            _configuration = configuration;
            AccountService = new AccountServices(this, userManager, roleManager, _context, _configuration);
            VacancyApplication = new VacancyApplicationService(_context,mapper, userManager);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
