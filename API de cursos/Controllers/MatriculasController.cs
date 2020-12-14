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
    public class MatriculasController : ControllerBase
    {
        private readonly MatriculaContext _context;
        private readonly AlunoPGTContext _context_aluno_pgt;

        public MatriculasController(MatriculaContext context, AlunoPGTContext alunoPGTContext)
        {
            _context = context;
            _context_aluno_pgt = alunoPGTContext;

        }

        // GET: api/Matriculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetMatriculas()
        {
            return await _context.Matriculas.ToListAsync();
        }

        // GET: api/Matriculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            if (matricula == null)
            {
                return NotFound();
            }

            return matricula;
        }


        // PUT: api/Matriculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula(long id, Matricula matricula)
        {
            if (id != matricula.Id)
            {
                return BadRequest();
            }

            _context.Entry(matricula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
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

        // POST: api/Matriculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<String>> PostMatricula(Matricula matricula)
        {
            if(_context_aluno_pgt.AlunoPagamentos.Any(p => p.Id_aluno == matricula.Id_aluno))
			{

                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetMatricula", new { id = matricula.Id }, matricula);

                return "Matricula realizada com sucesso";
            }
			else
			{
                return "Nenhum pagamento identificado para este aluno. Realize o pagamento antes de matricular-se";
			}
        }

        // DELETE: api/Matriculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(long id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatriculaExists(long id)
        {
            return _context.Matriculas.Any(e => e.Id == id);
        }
    }
}
