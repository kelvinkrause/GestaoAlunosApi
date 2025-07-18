using System.Net;

namespace GestaoEscolar.Exception
{
    public class ErrorOnValidationException : GestaoEscolarException
    {
        private readonly List<string> _errors;
        public ErrorOnValidationException(List<string> errors)
        {
            _errors = errors;
        }
        public override List<string> GetErrorMessage() => _errors;
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
