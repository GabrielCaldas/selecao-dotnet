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
    public class AlunoController : ControllerBase
    {
        private readonly AlunoContext _context;

        public AlunoController(AlunoContext context)
        {
            _context = context;
        }

        // GET: api/Alunoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        // GET: api/Alunoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(long id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        // PUT: api/Alunoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(long id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
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

        // POST: api/Alunoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<String>> PostAluno(Aluno aluno)
        {
            if(_context.Alunos.Any(e => e.Id== aluno.Id))
			{
                return "ID já cadastrado";
			}
            else if (_context.Alunos.Any(e => e.Email == aluno.Email))
            {
                return "Email já cadastrado";
            }
			else
			{

                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);

                return "Cadastro realizado com sucesso";
            }
        }

        // DELETE: api/Alunoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(long id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(long id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
