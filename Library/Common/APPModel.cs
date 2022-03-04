using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.Common
{
    [Serializable]
    public class APPModel
    {

        public int ID { get; set; }

        public string Type { get; set; }

        public int? VesionApp { get; set; }

        public string UrlApp { get; set; }

        public string ReleaseNotes { get; set; }
        public string Name { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}
