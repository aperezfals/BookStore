using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Exceptions
{
    public class InvalidISBNException : Exception
    {
        public InvalidISBNException(string isbn)
            : base($"ISBN {isbn} is invalid.")
        {

        }
    }
}
