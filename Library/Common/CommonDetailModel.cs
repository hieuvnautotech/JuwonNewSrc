using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class CommonDetailModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(4)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(4)]
        public string MasterCode { get; set; }

        [StringLength(50)]
        public string MasterName { get; set; }

        public bool? Active { get; set; }
    }
}
