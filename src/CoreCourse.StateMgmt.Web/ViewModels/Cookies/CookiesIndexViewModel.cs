using System.Collections.Generic;

namespace CoreCourse.StateMgmt.Web.ViewModels
{
    public class CookiesIndexViewModel
    {
        public List<CookiesBiscuitViewModel> Biscuits { get; set; }
        public string SelectedBiscuitImage { get; set; }
        public bool IsPersistent { get; set; }
    }
}

