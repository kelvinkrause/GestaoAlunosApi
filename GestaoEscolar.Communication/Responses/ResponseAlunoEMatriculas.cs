namespace GestaoEscolar.Communication.Responses
{
    public class ResponseAlunoEMatriculas
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataInclusao { get; set; }
        public List<ResponseMatricula> Matriculas { get; set; } = new List<ResponseMatricula>() { };
    }
}
