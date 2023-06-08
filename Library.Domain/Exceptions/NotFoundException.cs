namespace Library.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public string message { get; set; }

        public NotFoundException(string Message) : base(Message)
        {
            message = Message;
        }
    }
}