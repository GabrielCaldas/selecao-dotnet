using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;
using API_de_cursos.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace API_de_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoContext _context;
        private readonly SessionTokenContext _sessionTokenContext;

        public AlunoController(AlunoContext context, SessionTokenContext sessionTokenContext)
        {
            _sessionTokenContext = sessionTokenContext;
            _context = context;
        }

        // GET: api/Alunoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        // GET: api/Alunoes/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(long id)
        {

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                _sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == id)
            )
            {
                var aluno = await _context.Alunos.FindAsync(id);

                if (aluno == null)
                {
                    return NotFound("Aluno inválido.");
                }

                return Ok(aluno);
            }
			else
			{
                return BadRequest("Usuário inválido.");
			}
        }

        // PUT: api/Alunoes/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(long id, Aluno aluno)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                !_sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == id) ||
                id != aluno.Id
            ){
                return BadRequest("Usuário inválido.");
            }

            _context.Entry(aluno).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Aluno atualizado com sucesso."));
        }

        // POST: api/Alunoes
        [HttpPost]
        public async Task<ActionResult<String>> PostAluno(Aluno aluno)
        {
            if(_context.Alunos.Any(e => e.Id== aluno.Id))
			{
                return BadRequest("ID já cadastrado");
			}
            else if (_context.Alunos.Any(e => e.Email == aluno.Email))
            {
                return BadRequest("Email já cadastrado");
            }
			else
			{

                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);

                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Cadastro realizado com sucesso"));
            }
        }

        // DELETE: api/Alunoes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(long id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                _sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == id)
            )
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null)
                {
                    return NotFound("Aluno inválido.");
                }

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Aluno excluido com sucesso."));
            }
			else
			{
                return BadRequest("Usuário não autenticado.");
			}
        }

        private bool AlunoExists(long id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
