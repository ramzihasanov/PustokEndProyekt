namespace WebApplication6.CustomExceptions.BookException
{
    public class InvalidTagId : Exception
    {
        public string PropertyName { get; set; }
        public InvalidTagId()
        {
        }

        public InvalidTagId(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
