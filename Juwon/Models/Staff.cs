// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juwon.Models
{
    public partial class Staff
    {
        public int StaffId { get; set; }
        [Required]
        [StringLength(7)]
        public string StaffCode { get; set; }
        [Required]
        [StringLength(50)]
        public string StaffName { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        public bool? Active { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual UserInfo CreatedByNavigation { get; set; }
    }
}