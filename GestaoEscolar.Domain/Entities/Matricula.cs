using GestaoEscolar.Exception;

namespace GestaoEscolar.Domain.Entities
{
    public class Matricula
    {
        public Guid Id { get; private set; }
        public int CodigoMatricula {  get; private set; }
        public string NomeCurso { get; private set; } = string.Empty;
        public Guid AlunoId { get; private set; } = Guid.Empty;
        public Aluno Aluno { get; private set; } = null!;

        public Matricula() { }

        //DDD (Driven-Domain Design)
        public Matricula(string nomeCurso)
        {
            if (string.IsNullOrWhiteSpace(nomeCurso))
                throw new ErrorOnValidationException(["Nome do curso é obrigatório."]);

            //if (alunoId == Guid.Empty)
            //    throw new ErrorOnValidationException(["Id do aluno é obrigatório."]);
            //
            //if (aluno is null)
            //    throw new ErrorOnValidationException(["Aluno é obrigatório."]);
            Id = Guid.NewGuid();
            NomeCurso = nomeCurso;
        }

        public void AtualizarCurso(string nomeCurso)
        {
            if (string.IsNullOrWhiteSpace(nomeCurso))
                throw new ErrorOnValidationException([ "Nome curso não informado" ]);

            NomeCurso = nomeCurso;

        }
        public void DefinirAluno(Aluno aluno, int codigoMatricula)
        {
            Aluno = aluno;
            AlunoId = aluno.Id;
            CodigoMatricula = codigoMatricula;
        }


    }
}
