using GestaoEscolar.Communication.Responses;
using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Exception;

namespace GestaoEscolar.Application.UseCases.ListarAlunosCadastrados
{
    public class ListarAlunosCadastradosUseCase
    {
        private readonly IAlunoRepository _alunoRepository;
        public ListarAlunosCadastradosUseCase(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }
        public async Task<List<ResponseAluno>> Execute()
        {
            var alunos = await _alunoRepository.ObterTodosAsync();

            if (!alunos.Any())
                throw new ErrorOnValidationException([ "Não encontrou registro de alunos." ]);

            var response = alunos.Select(aluno => new ResponseAluno
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Telefone = aluno.Telefone,
                DataInclusao = aluno.DataInclusao
            }).ToList();
            
            return response;
            
        }
    }
}
