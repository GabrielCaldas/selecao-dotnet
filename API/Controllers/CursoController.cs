using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;
using API_de_cursos.Contexts;

namespace API_de_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoContext _context;

        public CursoController(CursoContext context)
        {
            _context = context;

            // Cadastro de base de cursos para teste

            if (_context.Cursos.Count() == 0)
            {
                Curso A = new Curso();
                A.NMCurso = "Filosofia";
                A.DETCurso = "O curso de filosofia visa o ensinamento e capacitação para o pensamento crítico.";

                Curso B = new Curso();
                B.NMCurso = "Matemática";
                B.DETCurso = "Com este curso você irá ser capaz de manipular os números e realizar contas como ninguem!.";

                Curso C = new Curso();
                C.NMCurso = "Português";
                C.DETCurso = "O conhecimento de português é fundamental para avançar nos demais cursos. Assine já!";

                PostCurso(A);
                PostCurso(B);
                PostCurso(C);
            }
        }

        // GET: api/Cursoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Cursos.ToListAsync();
        }

        // GET: api/Cursoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(long id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound("Curso não encontrado.");
            }

            return Ok(curso);
        }

        // PUT: api/Cursoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<String>> PutCurso(long id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest("Não foi possível atualizar o curso.");
            }

            _context.Entry(curso).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Atualização realizada com sucesso."));
        }

        // POST: api/Cursoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<String>> PostCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            CreatedAtAction("GetCurso", new { id = curso.Id }, curso);

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Cadastro realizado com sucesso."));
        }

        // DELETE: api/Cursoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(long id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound("Falha ao excluir curso.");
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Curso removido com sucesso."));
        }

        private bool CursoExists(long id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
