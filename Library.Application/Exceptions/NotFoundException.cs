namespace Library.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string message { get; set; }

        public NotFoundException(string Message)
            : base(Message)
        {
            message = Message;
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}