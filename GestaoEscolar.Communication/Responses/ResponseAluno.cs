namespace GestaoEscolar.Communication.Responses
{
    public class ResponseAluno
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataInclusao { get; set; }
    }
}
