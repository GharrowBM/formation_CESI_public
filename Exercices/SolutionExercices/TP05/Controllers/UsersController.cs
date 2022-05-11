using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP05.Datas;
using TP05.Models;
using TP05.Services;

namespace TP05.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("allConnections")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepository<User> _usersRepository;
        private PasswordService _passwordService;
        private readonly IConfiguration _config;

        public UsersController(IRepository<User> usersRepository, PasswordService passwordService, IConfiguration config)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
            _config = config;
        }

        [HttpPost("/login")]
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            password = _passwordService.EncryptPassword(password);

            User userToFind = _usersRepository.GetAll().Find(u => u.Email == email && u.Password == password);

            if (userToFind != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userToFind.Email),
                    new Claim(ClaimTypes.Role, userToFind.IsAdmin ? "admin" : "user"),
                };

                //Objet pour signer le token
                SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"])), SecurityAlgorithms.HmacSha256);


                //Créer notre jwt
                JwtSecurityToken jwt = new JwtSecurityToken(issuer: _config["JWT:ValidIssuer"], audience: _config["JWT:ValidAudience"], claims: claims, signingCredentials: signingCredentials, expires: DateTime.Now.AddDays(7));
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(jwt) });
            }

            return NotFound(new { Message = "Impossible de se connecter ! Utilisateur introuvable..." });
        }

        [HttpPost("/register")]
        public IActionResult Register([FromForm] string firstname, [FromForm] string lastname, [FromForm] string email, [FromForm] string password, [FromForm] string phone, [FromForm] bool isAdmin)
        {
            User newUser = new User()
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = _passwordService.EncryptPassword(password),
                Phone = phone,
                IsAdmin = isAdmin
            };

            User userAdded = _usersRepository.Add(newUser);

            if (userAdded != null)
            {
                return Ok(new { Message = $"L'utilisateur {userAdded.Firstname} {userAdded.Lastname} a été ajouté avec succès !"});
            }

            return NotFound(new { Message = "Erreur ! Impossible d'enregistrer l'utilisateur..." });
        }

        //[HttpPost("/test-token")]
        //public IActionResult TesTToken([FromBody] string jwt)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        tokenHandler.ValidateToken(jwt, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidIssuer = "aston",
        //            ValidAudience = "aston",
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Clé de cryptage à changer car trop simple à trouver..."))
        //        }, out SecurityToken validatedToken);
        //    }
        //    catch
        //    {
        //        return NotFound(new { Message = "Erreur ! Token invalide !", testResult = false });
        //    }

        //    return Ok(new { Message = "Le token est valide !", testResult = true });
        //}

    }
}
