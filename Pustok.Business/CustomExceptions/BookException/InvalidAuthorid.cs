namespace WebApplication6.CustomExceptions.BookException
{
    public class InvalidAuthorid : Exception
    {
        public string PropertyName { get; set; }
        public InvalidAuthorid()
        {
        }

        public InvalidAuthorid(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
