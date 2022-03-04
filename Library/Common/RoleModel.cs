using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class RoleModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(4)]
        public string RoleCategory { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public bool? Active { get; set; }
    }
}
