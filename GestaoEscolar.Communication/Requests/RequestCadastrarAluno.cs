namespace GestaoEscolar.Communication.Requests
{
    public class RequestCadastrarAluno
    {
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public List<RequestCadastrarMatricula> Matriculas { get; set; } = new List<RequestCadastrarMatricula>() { };
    }
}
