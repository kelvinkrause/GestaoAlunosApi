using GestaoEscolar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
