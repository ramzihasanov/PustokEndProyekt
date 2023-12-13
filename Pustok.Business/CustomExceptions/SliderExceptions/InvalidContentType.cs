namespace WebApplication6.CustomExceptions.SliderExceptions
{
    public class InvalidContentType : Exception
    {
        public string Propertyname { get; set; }
        public InvalidContentType()
        {
        }

        public InvalidContentType(string propertyname, string? message) : base(message)
        {
            Propertyname = propertyname;
        }
    }
}
