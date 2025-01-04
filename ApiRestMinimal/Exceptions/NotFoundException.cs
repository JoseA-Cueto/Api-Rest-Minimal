using ApiRestMinimal.Exceptions.Base;

namespace MiAplicacion.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string message) : base(message, 404) { }
    }
}
