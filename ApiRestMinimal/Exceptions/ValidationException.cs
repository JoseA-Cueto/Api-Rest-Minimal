using ApiRestMinimal.Exceptions.Base;

namespace MiAplicacion.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public ValidationException(string message) : base(message, 400) { }
    }
}
