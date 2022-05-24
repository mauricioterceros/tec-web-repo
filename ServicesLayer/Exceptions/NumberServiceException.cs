using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class NumberServiceException : Exception
    {
        public NumberServiceException(String message) : base(message) { }
    }
}
