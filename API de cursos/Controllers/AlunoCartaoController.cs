using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoCartaoController : ControllerBase
    {
        private readonly AlunoCartaoContext _context;
        private readonly AlunoContext _context_aluno;

        public AlunoCartaoController(AlunoCartaoContext context, AlunoContext context_aluno)
        {
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
        [HttpGet("{id_aluno}")]
        public async Task<List<AlunoCartao>> GetAlunoCartao(long id_aluno)
        {
            var alunoCartao = await _context.AlunoCatoes.Where(c => c.Id_aluno == id_aluno).ToListAsync();

            return alunoCartao;
        }

        // PUT: api/AlunoCartaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlunoCartao(long id, AlunoCartao alunoCartao)
        {
            if (id != alunoCartao.Id)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AlunoCartaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<String>> PostAlunoCartao(AlunoCartao alunoCartao)
        {
            if(_context_aluno.Alunos.Any(e => e.Id == alunoCartao.Id_aluno)){
                // Exite aluno

                if(_context.AlunoCatoes.Any(e => e.NUCartao == alunoCartao.NUCartao && e.Id_aluno == alunoCartao.Id_aluno))
				{
                    return "Cartão já cadastrado";
				}
				else
				{

                    _context.AlunoCatoes.Add(alunoCartao);
                    await _context.SaveChangesAsync();

                    CreatedAtAction("GetAlunoCartao", new { id = alunoCartao.Id }, alunoCartao);

                    return "Cartão cadastrado com sucesso";
                }

			}
			else
			{
                return "Aluno inválido";
			}
        }

        // DELETE: api/AlunoCartaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlunoCartao(long id)
        {
            var alunoCartao = await _context.AlunoCatoes.FindAsync(id);
            if (alunoCartao == null)
            {
                return NotFound();
            }

            _context.AlunoCatoes.Remove(alunoCartao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoCartaoExists(long id)
        {
            return _context.AlunoCatoes.Any(e => e.Id == id);
        }
    }
}
