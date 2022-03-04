using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class MaterialModel
    {
        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public int MaterialTypeId { get; set; }
        public string MaterialTypeName { get; set; }
        public int MaterialInOutId { get; set; }
        public string MaterialInOutName { get; set; }
        public int MaterialSectionId { get; set; }
        public string MaterialSectionName { get; set; }
        public int MaterialUnitId { get; set; }
        public string MaterialUnitName { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int MoldId { get; set; } = 0;
        public string MoldCode { get; set; }
        public string MaterialRemark { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}