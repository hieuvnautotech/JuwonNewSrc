// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juwon.Models
{
    public partial class VendorBuyerRel
    {
        [Key]
        public long Id { get; set; }
        public int VendorId { get; set; }
        public int BuyerId { get; set; }
        [Required]
        public bool? Active { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(BuyerId))]
        [InverseProperty("VendorBuyerRel")]
        public virtual Buyer Buyer { get; set; }
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("VendorBuyerRel")]
        public virtual Vendor Vendor { get; set; }
    }
}