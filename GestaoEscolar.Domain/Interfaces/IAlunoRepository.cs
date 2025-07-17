using GestaoEscolar.Domain.Entities;

namespace GestaoEscolar.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> ObterTodosAsync();
        Task<Aluno> ObterPorIdAsync(Guid? id);
        Task AdicionarAsync(Aluno aluno);
        Task AtualizarAsync(Aluno aluno);
        Task RemoverAsync(Guid id);
    }
}
