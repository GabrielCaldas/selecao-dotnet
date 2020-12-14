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
    public class AlunoPGTController : ControllerBase
    {
        private readonly AlunoPGTContext _context;
        private readonly AlunoContext _context_aluno;
        private readonly AlunoCartaoContext _context_aluno_cartao;

        public AlunoPGTController(AlunoPGTContext context, AlunoContext context_aluno,AlunoCartaoContext alunoCartaoContext)
        {
            _context = context;
            _context_aluno = context_aluno;
            _context_aluno_cartao = alunoCartaoContext;
        }

        // GET: api/AlunoPGT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoPGT>>> GetAlunoPagamentos()
        {
            return await _context.AlunoPagamentos.ToListAsync();
        }

        // GET: api/AlunoPGT/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoPGT>> GetAlunoPGT(long id)
        {
            var alunoPGT = await _context.AlunoPagamentos.FindAsync(id);

            if (alunoPGT == null)
            {
                return NotFound();
            }

            return alunoPGT;
        }

        // POST: api/AlunoPGT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<String>> PostAlunoPGT(AlunoPGT alunoPGT)
        {
            if(_context_aluno.Alunos.Any(a => a.Id == alunoPGT.Id_aluno))
			{
                if(_context_aluno_cartao.AlunoCatoes.Any(c => c.Id_aluno == alunoPGT.Id_aluno && c.Id == alunoPGT.Id_cartao))
				{

                    _context.AlunoPagamentos.Add(alunoPGT);
                    await _context.SaveChangesAsync();

                    CreatedAtAction("GetAlunoPGT", new { id = alunoPGT.Id }, alunoPGT);

                    return "Pagamento realizado com sucesso";
                }
				else
				{
                    return "Cartão inválido";
				}

                
			}
			else
			{
                return "Aluno inválido";
			}
        }

    }
}
