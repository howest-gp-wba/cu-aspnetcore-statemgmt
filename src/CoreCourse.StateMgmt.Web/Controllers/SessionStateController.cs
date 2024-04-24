using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using CoreCourse.StateMgmt.Web.ViewModels.SessionState;

namespace CoreCourse.StateMgmt.Web.Controllers
{
    public class SessionStateController : Controller
    {
        const string STATEKEY = "SessionBeers";

        List<BeerViewModel> AllBeers = new List<BeerViewModel> {
            new BeerViewModel { Name = "Brugse Zot", ImageName = "brugsezot" },
            new BeerViewModel { Name = "Duvel", ImageName = "duvel" },
            new BeerViewModel { Name = "Grimbergen", ImageName = "grimbergen" },
            new BeerViewModel { Name = "La Chouffe", ImageName = "lachouffe" },
            new BeerViewModel { Name = "Leffe", ImageName = "leffe" }
        };

        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            vm.Beers = AllBeers;
            vm.ShoppingCart = new List<BeerViewModel>();

            string serializedBeers = HttpContext.Session.GetString(STATEKEY);
            if (serializedBeers != null)
            {
                vm.ShoppingCart = JsonConvert.DeserializeObject<List<BeerViewModel>>(serializedBeers);
            }
            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult AddBeer(IndexViewModel model)
        {
            //get the selected beer from form
            BeerViewModel selectedBeer = AllBeers.FirstOrDefault(b => b.Name == model.SelectedBeerName);
            if(selectedBeer == null)
            {
                return RedirectToAction("Index");
            }

            //retrieve serialized collection from session
            string serializedBeers = HttpContext.Session.GetString(STATEKEY);
            List<BeerViewModel> beersInShoppingList = new List<BeerViewModel>();
            if(serializedBeers != null)
            {
                //deserialize JSON string to it's original form
                beersInShoppingList = JsonConvert.DeserializeObject<List<BeerViewModel>>(serializedBeers);
            }
            //add beer to the List
            beersInShoppingList.Add(selectedBeer);
            //seriaize List to JSON string
            serializedBeers = JsonConvert.SerializeObject(beersInShoppingList);
            //store JSON string in Session State
            HttpContext.Session.SetString(STATEKEY, serializedBeers);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(STATEKEY);
            return RedirectToAction("Index");
        }
    }
}