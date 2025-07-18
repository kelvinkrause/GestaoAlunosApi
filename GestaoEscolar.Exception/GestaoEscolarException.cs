using System.Net;

namespace GestaoEscolar.Exception
{
    public abstract class GestaoEscolarException : System.Exception
    {
        public abstract List<string> GetErrorMessage();
        public abstract HttpStatusCode GetStatusCode();
    }
}
