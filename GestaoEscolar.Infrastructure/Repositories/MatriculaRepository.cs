using GestaoEscolar.Domain.Entities;
using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoEscolar.Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AppDbContext _context;
        public MatriculaRepository(AppDbContext context) => _context = context;
        public async Task AdicionarAsync(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task<Matricula?> ObterPorIdAsync(Guid id) => await _context.Matriculas.FindAsync(id);

        public async Task<List<Matricula>> ObterTodosAsync() => await _context.Matriculas.ToListAsync();

        public async Task RemoverAsync(Guid id)
        {
            var matricula = await ObterPorIdAsync(id);
            if (matricula is not null)
            {
                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> ObterUltimoCodigoMatricula(Guid alunoId)
        {
            var aluno = await _context.Matriculas
                .Where(aluno => aluno.AlunoId == alunoId)
                .OrderByDescending(desc => desc.CodigoMatricula)
                .Select(ult => ult.CodigoMatricula)
                .FirstOrDefaultAsync();

            return aluno;
        }
    }
}
