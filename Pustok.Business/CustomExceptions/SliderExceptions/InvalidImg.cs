namespace WebApplication6.CustomExceptions.SliderExceptions
{
    public class InvalidImg : Exception
    {
        public string Propertyname { get; set; }
        public InvalidImg()
        {
        }

        public InvalidImg(string propertyname, string? message) : base(message)
        {
            Propertyname = propertyname;
        }
    }
}
