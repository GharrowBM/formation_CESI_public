using _03_Demenagements.Datas;
using _03_Demenagements.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _03_Demenagements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration config;

        public AccountsController(ApplicationDbContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        [HttpPost("/register")]
        public IActionResult Register(Account newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            context.Accounts.Add(newAccount);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Utilisateur enregistré avec succès !",
                    User = newAccount
                });
            }

            return BadRequest(new
            {
                Message = "Un problème a eu lieu lors de l'ajout de l'utilisateur en base de données !"
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

        [HttpPatch("/switch-role/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult SwitchAdministratorById(int id)
        {
            var found = context.Accounts.FirstOrDefault(x => x.Id == id);

            if (found != null)
            {
                found.IsAdmin = !found.IsAdmin;

                return Ok(new
                {
                    Message = "Changement effectué avec succès !"
                });
            }

            return NotFound(new
            {
                Message = "Aucun compte ne possède cet Id !"
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
