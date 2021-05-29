using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Import.Core.Exceptions
{
    public class ValidateException : Exception
    {

        public IDictionary? DataErr { get; set; }
        public ValidateException(string msg, IDictionary? data) : base(msg)
        {
            DataErr = data;
        }
    }
}
