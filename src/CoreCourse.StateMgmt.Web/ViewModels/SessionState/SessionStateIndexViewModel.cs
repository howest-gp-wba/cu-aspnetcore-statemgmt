using System.Collections.Generic;

namespace CoreCourse.StateMgmt.Web.ViewModels
{
    public class SessionStateIndexViewModel
    {
        //form 
        public List<SessionStateBeerViewModel> Beers { get; set; }
        public string SelectedBeerName { get; set; }

        //beers already in shopping cart
        public List<SessionStateBeerViewModel> ShoppingCart { get; set; }
    }
}

