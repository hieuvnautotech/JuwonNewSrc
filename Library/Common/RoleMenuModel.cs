using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class RoleMenuModel
    {
        public int? RoleID { get; set; }
        public List<string> MenuIDs { get; set; }
        public List<string> PermissionIDs { get; set; }
    }
}
