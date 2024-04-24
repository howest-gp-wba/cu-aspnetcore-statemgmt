using System.Collections.Generic;

namespace CoreCourse.StateMgmt.Web.ViewModels.Cookies
{
    public class IndexViewModel
    {
        public List<BiscuitViewModel> Biscuits { get; set; }
        public string SelectedBiscuitImage { get; set; }
        public bool IsPersistent { get; set; }
    }
}

