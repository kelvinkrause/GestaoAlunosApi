using GestaoEscolar.Communication.Requests;
using GestaoEscolar.Communication.Responses;
using GestaoEscolar.Domain.Entities;
using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Exception;

namespace GestaoEscolar.Application.UseCases.AtualizarDadosAlunoEMatriculas
{
    public class AtualizaAlunoEMatriculasUseCase
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        public AtualizaAlunoEMatriculasUseCase(
            IAlunoRepository alunoRepository,
            IMatriculaRepository matriculaRepository)
        {
            _alunoRepository = alunoRepository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task<ResponseAlunoEMatriculas> Execute(RequestAtualizaAluno request)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(request.Id);

            if (aluno is null)
                throw new ErrorOnValidationException(["Aluno não encontrado."]);

            aluno.AtualizarDadosAlunoEMatriculas(
                request.Nome,
                request.Telefone);

            foreach (var matricula in request.Matriculas)
            {
                if (matricula.Id.HasValue)
                {
                    var matriculaExiste = aluno.Matriculas.FirstOrDefault(m => m.Id == matricula.Id.Value);
                    if (matriculaExiste is not null)
                    {
                        matriculaExiste.AtualizarCurso(matricula.NomeCurso);
                    }
                }
                else
                {
                    var proximoCodigo = aluno.Matriculas
                        .OrderByDescending(desc => desc.CodigoMatricula)
                        .Select(ult => ult.CodigoMatricula)
                        .FirstOrDefault() + 1;

                    var matriculaNova = new Matricula(matricula.NomeCurso);

                    matriculaNova.DefinirAluno(aluno, proximoCodigo);

                    await _matriculaRepository.AdicionarAsync(matriculaNova);
                }
            }

            await _alunoRepository.AtualizarAsync(aluno);

            // Buscar o aluno atualizado com todas as matrículas
            var alunoAtualizado = await _alunoRepository.ObterPorIdAsync(aluno.Id);

            return new ResponseAlunoEMatriculas
            {
                Id = alunoAtualizado!.Id,
                Nome = alunoAtualizado.Nome,
                Telefone = alunoAtualizado.Telefone,
                DataInclusao = alunoAtualizado.DataInclusao,
                Matriculas = alunoAtualizado.Matriculas.Select(m => new ResponseMatricula
                {
                    Id = m.Id,
                    CodigoMatricula = m.CodigoMatricula,
                    NomeCurso = m.NomeCurso
                }).ToList()
            };
        }
    }
}

