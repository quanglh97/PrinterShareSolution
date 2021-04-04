using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterShareSolution.Utilities.Exceptions
{
    public class PrinterShareException : Exception
    {
        public PrinterShareException()
        { 
        }

        public PrinterShareException(string message)
            : base(message)
        {
        }
        public PrinterShareException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
