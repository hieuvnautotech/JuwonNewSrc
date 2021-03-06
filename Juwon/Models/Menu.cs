// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juwon.Models
{
    public partial class Menu
    {
        public Menu()
        {
            RoleMenu = new HashSet<RoleMenu>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(12)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(150)]
        public string FullName { get; set; }
        [StringLength(4)]
        public string MenuCategory { get; set; }
        public byte? MenuLevel { get; set; }
        public byte? MenuOrderly { get; set; }
        public byte? MenuLevel2Orderly { get; set; }
        public byte? MenuLevel3Orderly { get; set; }
        [StringLength(150)]
        public string Link { get; set; }
        public bool? Active { get; set; }

        [InverseProperty("Menu")]
        public virtual ICollection<RoleMenu> RoleMenu { get; set; }
    }
}