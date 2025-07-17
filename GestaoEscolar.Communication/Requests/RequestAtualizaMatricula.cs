namespace GestaoEscolar.Communication.Requests
{
    public class RequestAtualizaMatricula
    {
        public Guid? Id { get; set; }
        public string NomeCurso { get; set; } = string.Empty;
    }
}
