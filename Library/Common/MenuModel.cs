using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class MenuModel
    {
        public int ID { get; set; }

        [StringLength(12)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(4)]
        public string MenuCategory { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public byte? MenuLevel { get; set; }

        [StringLength(50)]
        public string PrimaryMenu { get; set; }
        public byte? MenuOrderly { get; set; }

        [StringLength(50)]
        public string SecondaryMenu { get; set; }
        public byte? MenuLevel2Orderly { get; set; }

        [StringLength(50)]
        public string TertiaryMenu { get; set; }
        public byte? MenuLevel3Orderly { get; set; }

        [StringLength(150)]
        public string Link { get; set; }

        public bool? Active { get; set; }

        [StringLength(250)]
        public string MultiLang { get; set; }
    }
}
