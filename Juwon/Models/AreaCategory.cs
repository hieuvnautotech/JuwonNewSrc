﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juwon.Models
{
    public partial class AreaCategory
    {
        public AreaCategory()
        {
            Area = new HashSet<Area>();
        }

        [Key]
        public int AreaCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string AreaCategoryName { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(UserInfo.AreaCategory))]
        public virtual UserInfo CreatedByNavigation { get; set; }
        [InverseProperty("AreaCategory")]
        public virtual ICollection<Area> Area { get; set; }
    }
}