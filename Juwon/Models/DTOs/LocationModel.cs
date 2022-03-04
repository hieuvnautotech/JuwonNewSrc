using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class LocationModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public int LocationCategoryId { get; set; }
        public string LocationCategoryName { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}