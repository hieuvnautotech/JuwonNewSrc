using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class UserManualModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string MenuCode { get; set; }
        public int MenuLevel { get; set; }
        public string LanguageCode { get; set; }
        public string FullName { get; set; }
        public bool? Active { get; set; }
    }
}
