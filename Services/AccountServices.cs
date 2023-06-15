using EmploymentApi.Contracts;
using EmploymentApi.DTOs;
using EmploymentApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmploymentApi.Services
{
    public class AccountServices: IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountServices(IUnitOfWork UnitOfWork, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext context,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _UnitOfWork = UnitOfWork;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthDTO> Login(LoginDTO model)
        {
            var authModel = new AuthDTO();

            var user = await _userManager.FindByEmailAsync(model.Email);
            var IsValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user is null || !IsValidPassword)
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            authModel.Token = await GenerateToken(user);
            //var jwtSecurityToken = await _UnitOfWork.JWTService.CreateJwtToken(user);
            var role = await _userManager.GetRolesAsync(user);
            authModel.IsAuthenticated = true;
            //authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            //authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = role.FirstOrDefault();

            return authModel;
        }

        public async Task<AuthDTO> EmployerRegisterAsync(EmployerDTO employer)
        {
            var auth = await RegisterAsync(employer.Register);
            if (auth is not null) 
            {
                Employer employer1 = new Employer() { Id = auth.Id, Company = employer.Company};
                employer1.ApplicationUser = (ApplicationUser) await _userManager.FindByIdAsync(auth.Id);
                _context.Employer.Add(employer1);
                _context.SaveChanges();
            }
            return auth;
        }

        public async Task<AuthDTO> ApplicantRegisterAsync(ApplicantRegisterDTO applicant)
        {
            var auth = await RegisterAsync(applicant.Register);
            if (auth is not null)
            {
                Applicant applicant1 = new Applicant() { ApplicantId = auth.Id, Qualification = applicant.Qualification };
                applicant1.ApplicationUser = (ApplicationUser)await _userManager.FindByIdAsync(auth.Id);
                _context.Applicant.Add(applicant1);
                _context.SaveChanges();
            }
            return auth;
        }

        public async Task<AuthDTO> RegisterAsync(RegisterDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthDTO { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthDTO { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthDTO { Message = errors };
            }

            if (model.IsEmployer)
            {
                await _userManager.AddToRoleAsync(user, "Employer");

            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Applicant");
            }


            await _userManager.UpdateAsync(user); 

            return new AuthDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = model.IsEmployer? "Employer" : "Applicant",
                Username = user.UserName
            };
        }

        private async Task<string> GenerateToken(IdentityUser user)
        {
            //// [1] Get The Key from config --> and it's a symetric key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JwtSettings:Key"]));


            //// [2] Generate hashed credentials
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            //// [3] Get information about the user to be in the token
            // --[3.1] Get user roles
            var roles = await _userManager.GetRolesAsync(user);
           

            // do a little operation aganist the roles
            // claim is a special datatype that could be assignrd to generate token
            // so here i need to convert user roles to claim so i can include these information into token
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            // --[3.2] user defined claims in database, in our case we don't but just for info
            var userClaims = await _userManager.GetClaimsAsync(user);


            // --[3.3] combine all claims for token
            var tokenClaims = new List<Claim>
            {
                // one of the predefined and it just a string called "sub" and the value will be on the right side
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // if you want to include a custome key for example user id
                // and for [best practices] it's prefered to have a static class which contains all custome claims you want
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            //// [4] Generate token
            var token = new JwtSecurityToken(
                // assign token parameter which is descriped in JWT configurations file
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
