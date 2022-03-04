using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juwon.Models.DTOs
{
    [Serializable]
    public class QCMasterModel
    {
        public int QCMasterId { get; set; }
        public string QCMasterName { get; set; }
        public string QCMasterDescription { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public List<int?> QCDetailIds { get; set; }
    }
}