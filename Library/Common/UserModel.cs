using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class UserModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public bool Active { get; set; }
        public int? WarehouseID { get; set; }
        public int? LocationID { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
        public List<MenuModel> Menus { get; set; }
    }
}
