using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Exception;

namespace GestaoEscolar.Application.UseCases.RemoverAluno
{
    public class RemoverAlunoEMatriculasUseCase
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMatriculaRepository _matriculaRepository;

        public RemoverAlunoEMatriculasUseCase(
            IAlunoRepository alunoRepository,
            IMatriculaRepository matriculaRepository)
        {
            _alunoRepository = alunoRepository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task Execute(Guid id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);

            if (aluno is null)
                throw new ErrorOnValidationException([ "Aluno não encontrado." ]);

            await _alunoRepository.RemoverAsync(aluno.Id);
        }

    }
}
