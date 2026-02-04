namespace MyCookBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : MyCookBookException
    {
        // Lista de mensagens de erro de validação
        // IList é uma interface que representa uma coleção genérica de objetos
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
