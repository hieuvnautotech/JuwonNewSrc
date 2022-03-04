using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class AreaModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int AreaCategoryId { get; set; }
        public string AreaCategoryName { get; set; }
        public string AreaDescription { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}