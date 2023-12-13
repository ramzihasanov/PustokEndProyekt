namespace WebApplication6.CustomExceptions.BookException
{
    public class InvalidImg : Exception
    {
        public string PropertyName { get; set; }
        public InvalidImg()
        {
        }

        public InvalidImg(string propertyname,string? message) : base(message)
        {

            PropertyName = propertyname;
        }
    }
}
