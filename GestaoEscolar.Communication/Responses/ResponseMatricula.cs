namespace GestaoEscolar.Communication.Responses
{
    public class ResponseMatricula
    {
        public Guid Id { get; set; }
        public int CodigoMatricula { get; set; }
        public string NomeCurso { get; set; } = string.Empty;
    }
}
