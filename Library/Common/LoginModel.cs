using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    [Serializable]
    public class LoginModel
    {
        [Required]
        [Display(Name = "DbTbl_UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "DbTbl_Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        //[Display(Name = "CheckBox_RememberMe", ResourceType = typeof(Resource))]
        //public bool RememberMe { get; set; }
    }
}
