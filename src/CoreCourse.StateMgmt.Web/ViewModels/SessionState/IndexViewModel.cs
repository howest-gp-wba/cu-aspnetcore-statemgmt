using System.Collections.Generic;

namespace CoreCourse.StateMgmt.Web.ViewModels.SessionState
{
    public class IndexViewModel
    {
        //form 
        public List<BeerViewModel> Beers { get; set; }
        public string SelectedBeerName { get; set; }

        //beers already in shopping cart
        public List<BeerViewModel> ShoppingCart { get; set; }
    }
}

