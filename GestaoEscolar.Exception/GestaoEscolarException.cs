using System.Net;

namespace GestaoAlunos.Exception
{
    public abstract class GestaoEscolarException : System.Exception
    {
        public abstract List<string> GetErrorMessage();
        public abstract HttpStatusCode GetStatusCode();
    }
}
