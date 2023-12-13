using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.CustomExceptions.GenreException
{
    public class NotFoundDataException : Exception
    {
        private readonly string Propertyname;

        public NotFoundDataException()
        {
        }

        public NotFoundDataException(string propertyname,string? message) : base(message)
        {
            Propertyname = propertyname;
        }
    }
}
