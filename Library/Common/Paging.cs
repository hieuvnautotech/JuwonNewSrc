using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class Paging
    {
        public string Sidx { get; set; }
        public string Sord { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public bool Search { get; set; }
    }
}
