using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class BomDetailModel
    {
        public int BomDetailId { get; set; }
        public string BomDetailCode { get; set; }
        public int BomId { get; set; }
        public string BomCode { get; set; }
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public decimal? Weight { get; set; }
        public short Amount { get; set; }
        public string Image { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}