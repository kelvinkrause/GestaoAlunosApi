using GestaoEscolar.Exception;

namespace GestaoEscolar.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; } = string.Empty;
        public string Telefone {  get; private set; } = string.Empty;
        public DateTime DataInclusao { get; private set; } = DateTime.UtcNow;

        private readonly List<Matricula> _matriculas = new();
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();
        
        //DDD (Driven-Domain Design)
        public Aluno() { }

        public Aluno CriarAlunoComMatriculas(string nome, string telefone, List<Matricula> matriculas)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("Nome não informado.");

            if (string.IsNullOrWhiteSpace(telefone))
                erros.Add("Telefone não informado.");

            if (matriculas is null || matriculas.Count == 0)
                erros.Add("É necessário informar ao menos uma matrícula.");

            if (erros.Any())
                throw new ErrorOnValidationException(erros);

            this.Nome = nome;
            this.Telefone = telefone;

            var contador = 0;

            foreach (var matricula in matriculas)
            {
                matricula.DefinirAluno(this, contador++);
                _matriculas.Add(matricula);
            }

            return this;
        }
        
        public void AtualizarDadosAlunoEMatriculas(string nome, string telefone)
        {
            var erros = new List<string>();
            var matriculasNovas = new List<Matricula>();

            if (string.IsNullOrWhiteSpace(nome)) 
                erros.Add("Nome não informado.");

            if (string.IsNullOrWhiteSpace(telefone))
                erros.Add("Telefone não informado.");

            if(erros.Any())
                throw new ErrorOnValidationException(erros);

            this.Nome = nome;
            this.Telefone = telefone;

        }
    }
}
