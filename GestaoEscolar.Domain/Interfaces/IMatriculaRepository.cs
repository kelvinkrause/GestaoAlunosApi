using GestaoEscolar.Domain.Entities;

namespace GestaoEscolar.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<List<Matricula>> ObterTodosAsync();
        Task<Matricula?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Matricula aluno);
        Task AtualizarAsync(Matricula aluno);
        Task RemoverAsync(Guid id);
        Task<int> ObterUltimoCodigoMatricula(Guid alunoId);
    }
}
