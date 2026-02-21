using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class AppException : Exception
    {
        protected AppException(string message) : base(message) { }
    }
}
