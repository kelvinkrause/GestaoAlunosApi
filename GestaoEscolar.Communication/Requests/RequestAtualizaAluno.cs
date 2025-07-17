namespace GestaoEscolar.Communication.Requests
{
    public class RequestAtualizaAluno
    {

        public Guid? Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public List<RequestAtualizaMatricula> Matriculas { get; set; } = new List<RequestAtualizaMatricula>() { };
    }
}
