namespace WebApplication6.CustomExceptions.SliderExceptions
{
    public class InvalidImgSize : Exception
    {
        public string Propertyname { get; set; }
        public InvalidImgSize()
        {
        }

        public InvalidImgSize(string propertyname,string? message) : base(message)
        {
            Propertyname = propertyname;
        }
    }
}
