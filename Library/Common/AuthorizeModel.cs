using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class AuthorizeModel
    {
        public int? ID { get; set; }
        public List<string> AuthorizeIDs { get; set; }
    }
}
