using GestaoEscolar.Communication.Requests;
using GestaoEscolar.Communication.Responses;
using GestaoEscolar.Domain.Entities;
using GestaoEscolar.Domain.Interfaces;

namespace GestaoEscolar.Application.UseCases.CadastrarAlunoEMatriculas
{
    public class CadastrarAlunoEMatriculasUseCase
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        public CadastrarAlunoEMatriculasUseCase(
            IAlunoRepository alunoRepository,
            IMatriculaRepository matriculaRepository)
        {
            _alunoRepository = alunoRepository;
            _matriculaRepository = matriculaRepository;
        }
        public async Task<ResponseAlunoEMatriculas> Execute(RequestCadastrarAluno request)
        {
            var matriculas = new List<Matricula>();

            foreach (var matricula in request.Matriculas)
            {
                matriculas.Add(new Matricula(
                    matricula.NomeCurso
                    ));
            }

            var aluno = new Aluno();

            aluno.CriarAlunoComMatriculas(request.Nome, request.Telefone, matriculas);

            await _alunoRepository.AdicionarAsync(aluno);

            return new ResponseAlunoEMatriculas
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Telefone = aluno.Telefone,
                DataInclusao = aluno.DataInclusao,
                Matriculas = aluno.Matriculas.Select(matricula => new ResponseMatricula
                {
                    Id = matricula.Id,
                    CodigoMatricula = matricula.CodigoMatricula,
                    NomeCurso = matricula.NomeCurso
                }).ToList()
            };

        }
    }
}
