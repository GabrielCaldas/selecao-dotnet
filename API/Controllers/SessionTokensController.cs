using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;
using API_de_cursos.Contexts;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace API_de_cursos.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SessionTokensController : ControllerBase
    {
        private readonly SessionTokenContext _context;
        private readonly AlunoContext _context_aluno;
        private readonly IConfiguration _configuration;

        public SessionTokensController(SessionTokenContext context, AlunoContext context_aluno, IConfiguration configuration)
        {
            _context = context;
            _context_aluno = context_aluno;
            _configuration = configuration;
        }

        // Post: api/login
        [HttpPost("/api/login")]
        public async Task<ActionResult<String>> Login(LoginInfo login)
        {
            if(!_context_aluno.Alunos.Any(e => e.Email == login.Email))
            {
                return BadRequest("Usuário ou senha incorretos");
            }

            var selectedAluno = await _context_aluno.Alunos.Where(e => e.Email == login.Email).ToListAsync();
            if (selectedAluno == null)
            {
                return BadRequest("Usuário ou senha incorretos");
            }

            Aluno alunoItem = selectedAluno[0];

            if(alunoItem.Password != login.Password)
            {
                return BadRequest("Usuário ou senha incorretos");
            }

            SessionToken sessionToken = BuildToken(alunoItem);

            _context.Tokens.Add(sessionToken);
            await _context.SaveChangesAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(sessionToken));
        }

        private SessionToken BuildToken(Aluno aluno)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, aluno.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new SessionToken
            {
                Id_User = aluno.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        // GET: api/logout
        [Authorize]
        [HttpGet("/api/logout")]
        public async Task<IActionResult> Logout()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _context.Tokens.Any(e => e.Token == accessToken);
            var sessionToken = await _context.Tokens.Where(e => e.Token == accessToken).ToListAsync();
            if (sessionToken == null)
            {
                return BadRequest("Token não válido");
            }

            _context.Tokens.Remove(sessionToken[0]);
            await _context.SaveChangesAsync();

            return Ok("Usuário foi deslogado");
        }
    }
}
