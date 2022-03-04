using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class VendorModel
    {
        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public int VendorCategoryId { get; set; }
        public string VendorCategoryName { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public List<VendorBuyerRelModel> Buyers { get; set; }
    }
}