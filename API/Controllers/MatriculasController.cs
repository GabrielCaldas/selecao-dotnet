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
    public class MatriculasController : ControllerBase
    {
        private readonly MatriculaContext _context;
        private readonly CursoContext _context_curso;
        private readonly AlunoPGTContext _context_aluno_pgt;
        private readonly SessionTokenContext _sessionTokenContext;

        public MatriculasController(MatriculaContext context, AlunoPGTContext alunoPGTContext, SessionTokenContext sessionTokenContext,CursoContext cursoContext)
        {
            _sessionTokenContext = sessionTokenContext;
            _context = context;
            _context_aluno_pgt = alunoPGTContext;
            _context_curso = cursoContext;

        }

        // GET: api/Matriculas
        [HttpGet]
        public async Task<ActionResult<String>> GetMatriculas()
        {
            List<Matricula> matriculas = await _context.Matriculas.ToListAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(matriculas));
        }

        // GET: api/Matriculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<String>> GetMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            if (matricula == null)
            {
                return NotFound("Matrícula não encontrada.");
            }

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(matricula));
        }

        // GET: api/Matriculas/Aluno/5
        [Authorize]
        [HttpGet("Aluno/{aluno_id}")]
        public async Task<ActionResult<List<Matricula>>> GetMatriculaByAluno(long aluno_id)
        {

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (
                _sessionTokenContext.Tokens.Any(e => e.Token == accessToken)
            )
            {
                return await _context.Matriculas.Where(m => m.Id_aluno == aluno_id).ToListAsync(); ;

            }
            else
            {
                return BadRequest("Usuário inválido.");
            }
        }


        // PUT: api/Matriculas/5
        [HttpPut("{id}")]
        public async Task<ActionResult<String>> PutMatricula(long id, Matricula matricula)
        {
            if (id != matricula.Id)
            {
                return BadRequest("Não foi possível atualizar a matrícula.");
            }

            _context.Entry(matricula).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Matrícula atualizada com sucesso."));
        }

        // POST: api/Matriculas
        [HttpPost]
        public async Task<ActionResult<String>> PostMatricula(Matricula matricula)
        {
            if(_context_aluno_pgt.AlunoPagamentos.Any(p => p.Id_aluno == matricula.Id_aluno))
			{

                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetMatricula", new { id = matricula.Id }, matricula);

                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Matrícula realizada com sucesso."));
            }
			else
			{
                return BadRequest("Nenhum pagamento identificado para este aluno. Realize o pagamento antes de matricular-se.");
			}
        }

        // DELETE: api/Matriculas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeleteMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound("Falha ao remover a matrícula.");
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Matrícula removida com sucesso."));
        }

        private bool MatriculaExists(long id)
        {
            return _context.Matriculas.Any(e => e.Id == id);
        }
    }
}
