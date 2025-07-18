using GestaoEscolar.Domain.Entities;
using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoEscolar.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;
        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Aluno aluno)
        {
            //_context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<Aluno?> ObterPorIdAsync(Guid? id)
        {
            return await _context.Alunos
                .Include(m => m.Matriculas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos
                .Include(m => m.Matriculas)
                .ToListAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var aluno = await ObterPorIdAsync(id);
            if (aluno is not null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
        }
    }
}
