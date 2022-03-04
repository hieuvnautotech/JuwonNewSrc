using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class BomModel
    {
        public int BomId { get; set; }
        public string BomCode { get; set; }
        public int MoldId { get; set; }
        public string MoldCode { get; set; }
        public string FileUpload { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public List<VendorBuyerRelModel> Buyers { get; set; }
    }
}