

namespace Pustok.Business.CustomExceptions.GenreException
{
    public class InvalidNameException:Exception
    {private  string PropertyName { get; set; }
        public InvalidNameException()
        {

        }
        public InvalidNameException(string propertyName,string? message):base(message)
        {
            PropertyName = propertyName;
        }
    }
}
