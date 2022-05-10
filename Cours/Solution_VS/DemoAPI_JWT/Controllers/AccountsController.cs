using DemoAPI_JWT.Models;
using DemoAPI_JWT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAPI_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly FakeDbService context;
        private readonly IConfiguration config;

        public AccountsController(FakeDbService context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        [HttpPost("/register")]
        public IActionResult Register(Account newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            newAccount.Id = ++Account.Count;

            context.Accounts.Add(newAccount);

            return Ok(new
            {
                Message = "Utilisateur enregistré avec succès !",
                User = newAccount
            });
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginCredentials credentials)
        {
            var found = context.Accounts.FirstOrDefault(x => x.UserName == credentials.UserName && x.Password == credentials.Password);

            if (found == null) return NotFound(new
            {
                Message = "Login ou mot de passe introuvable !"
            });

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, found.UserName),
                    new Claim(ClaimTypes.Email, found.EmailAddress),
                    new Claim(ClaimTypes.Role, found.IsAdmin ? "Admin" : "User")
                };

            var expiresAt = DateTime.UtcNow.AddMinutes(30);

            return Ok(new
            {
                Message = "Login réalisé avec succès !",
                Token = CreateToken(claims, expiresAt),
                ExpiresAt = expiresAt
            });
        }

        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var secretKey = Encoding.ASCII.GetBytes(config["JWT:SecretKey"]);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                audience: config["JWT:ValidAudience"],
                issuer: config["JWT:ValidIssuer"],
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

    public class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
