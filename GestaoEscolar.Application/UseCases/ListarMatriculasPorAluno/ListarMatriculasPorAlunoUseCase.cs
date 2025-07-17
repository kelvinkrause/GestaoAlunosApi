using GestaoEscolar.Communication.Responses;
using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Exception;

namespace GestaoEscolar.Application.UseCases.ListarMatriculasPorAluno
{
    public class ListarMatriculasPorAlunoUseCase
    {
        private readonly IAlunoRepository _alunoRepository;
        public ListarMatriculasPorAlunoUseCase(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<List<ResponseMatricula>> Execute(Guid Id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(Id);

            if (aluno is null)
                throw new ErrorOnValidationException(["Aluno não encontrado."]);

            if (aluno.Matriculas is null || !aluno.Matriculas.Any())
                throw new ErrorOnValidationException(["Nenhuma matrícula encontrada para este aluno."]);

            var response = aluno.Matriculas.Select(matricula => new ResponseMatricula
            {
                Id = matricula.Id,
                CodigoMatricula = matricula.CodigoMatricula,
                NomeCurso = matricula.NomeCurso,
            }).ToList();

            return response;
        }
    }
}
