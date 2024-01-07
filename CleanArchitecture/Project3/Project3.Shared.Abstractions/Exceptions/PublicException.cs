using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Shared.Abstractions.Exceptions
{
    public abstract class PublicException : Exception
    {
        protected PublicException(string message) : base(message)
        {

        }
    }
}
