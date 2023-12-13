namespace WebApplication6.CustomExceptions.BookException
{
    public class InvalidImgSize : Exception
    {
        public string PropertyName { get; set; }
        public InvalidImgSize()
        {
        }

        public InvalidImgSize(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
