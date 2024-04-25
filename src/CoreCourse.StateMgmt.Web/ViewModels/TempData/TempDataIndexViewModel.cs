using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCourse.StateMgmt.Web.ViewModels
{
    public class TempDataIndexViewModel
    {
        [Required]
        [Display(Name = "Enter message")]
        public string MessageToEcho { get; set; }


        [Display(Name = "Keep Until Removed")]
        public bool KeepUntilRemoved { get; set; }
    }
}
