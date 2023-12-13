namespace WebApplication6.CustomExceptions.BookException
{
    public class InvalidContenttype : Exception
    {
        public string PropertyName { get; set; }
        public InvalidContenttype()
        {
        }

        public InvalidContenttype(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
