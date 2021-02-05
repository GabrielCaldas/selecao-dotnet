using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;
using API_de_cursos.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace API_de_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoCartaoController : ControllerBase
    {
        private readonly AlunoCartaoContext _context;
        private readonly AlunoContext _context_aluno;
        private readonly SessionTokenContext _sessionTokenContext;

        public AlunoCartaoController(AlunoCartaoContext context, AlunoContext context_aluno, SessionTokenContext sessionTokenContext)
        {
            _sessionTokenContext = sessionTokenContext;
            _context = context;
            _context_aluno = context_aluno;
        }

        // GET: api/AlunoCartaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoCartao>>> GetAlunoCatoes()
        {
            return await _context.AlunoCatoes.ToListAsync();
        }

        // GET: api/AlunoCartaos/5
        [Authorize]
        [HttpGet("{id_aluno}")]
        public async Task<ActionResult<IEnumerable<AlunoCartao>>> GetAlunoCartao(long id_aluno)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                _sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == id_aluno)
            )
            {

                var alunoCartao = await _context.AlunoCatoes.Where(c => c.Id_aluno == id_aluno).ToListAsync();

                return Ok(alunoCartao);
            
            }
			else
			{
                return BadRequest("Usuário inválido.");
			}
        }

        // PUT: api/AlunoCartaos/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlunoCartao(long id, AlunoCartao alunoCartao)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                !_sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == id) ||
                id != alunoCartao.Id
            ){
                return BadRequest("Usuário inválido.");
            }

            _context.Entry(alunoCartao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoCartaoExists(id))
                {
                    return NotFound("Não foi possível identificar o cartão.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Atualizado com sucesso."));
        }

        // POST: api/AlunoCartaos
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<String>> PostAlunoCartao(AlunoCartao alunoCartao)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                _sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == alunoCartao.Id_aluno) &&
                _context_aluno.Alunos.Any(e => e.Id == alunoCartao.Id_aluno)
             ){
                // Exite aluno

                if(_context.AlunoCatoes.Any(e => e.NUCartao == alunoCartao.NUCartao && e.Id_aluno == alunoCartao.Id_aluno))
				{
                    return BadRequest("Cartão já cadastrado");
				}
				else
				{

                    _context.AlunoCatoes.Add(alunoCartao);
                    await _context.SaveChangesAsync();

                    CreatedAtAction("GetAlunoCartao", new { id = alunoCartao.Id }, alunoCartao);

                    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Cartão cadastrado com sucesso"));
                }

			}
			else
			{
                return BadRequest("Usuário inválido.");
			}
        }

        // DELETE: api/AlunoCartaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlunoCartao(long id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var alunoCartao = await _context.AlunoCatoes.FindAsync(id);
            if (alunoCartao == null)
            {
                return NotFound("Cartão não encontrado.");
            }

            if (
                _sessionTokenContext.Tokens.Any(e => e.Token == accessToken && e.Id_User == alunoCartao.Id_aluno)
            )
            {
   

                _context.AlunoCatoes.Remove(alunoCartao);
                await _context.SaveChangesAsync();

                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Cartão excluido com sucesso."));
            }
			else
			{
                return BadRequest("Usuário inválido.");
			}
        }

        private bool AlunoCartaoExists(long id)
        {
            return _context.AlunoCatoes.Any(e => e.Id == id);
        }
    }
}
