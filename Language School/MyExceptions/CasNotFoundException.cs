using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.MyExceptions
{
    class CasNotFoundException : Exception
    {
        public CasNotFoundException() : base() { }

        public CasNotFoundException(string message) : base(message) { }

        public CasNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
