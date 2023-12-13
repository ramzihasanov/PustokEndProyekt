namespace WebApplication6.CustomExceptions.BookException
{
    public class invalidGenreId : Exception
    {
        public string PropertyName { get; set; }
        public invalidGenreId()
        {
        }

        public invalidGenreId(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
