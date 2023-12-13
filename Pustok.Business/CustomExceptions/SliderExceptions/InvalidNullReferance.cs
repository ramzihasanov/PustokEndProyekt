namespace WebApplication6.CustomExceptions.SliderExceptions
{
    public class InvalidNullReferance : Exception
    {
        public string Propertyname  { get; set; }
        public InvalidNullReferance()
        {
        }

        public InvalidNullReferance(string propertyname,string? message) : base(message)
        {

            Propertyname = propertyname;
        }
    }
}
