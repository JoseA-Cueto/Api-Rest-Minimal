using ApiRestMinimal.Exceptions.Base;

namespace MiAplicacion.Exceptions
{
    public class ValidationException : ExceptionBase
    {
      
        public ValidationException(string field, string message)
            : base($"Validation error in field '{field}': {message}", 400) { }
    }
}

