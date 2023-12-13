namespace WebApplication6.CustomExceptions.BookException
{
    public class InvalidNullreferance : Exception
    {
        public string PropertyName { get; set; }
        public InvalidNullreferance()
        {
        }

        public InvalidNullreferance(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
