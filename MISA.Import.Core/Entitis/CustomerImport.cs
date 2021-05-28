using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Import.Core.Entitis
{
    public class CustomerImport
    {
        public Customer Data { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
