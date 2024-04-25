using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using CoreCourse.StateMgmt.Web.ViewModels;

namespace CoreCourse.StateMgmt.Web.Controllers
{
    public class SessionStateController : Controller
    {
        const string STATEKEY = "SessionBeers";

        List<SessionStateBeerViewModel> AllBeers = new List<SessionStateBeerViewModel> {
            new SessionStateBeerViewModel { Name = "Brugse Zot", ImageName = "brugsezot" },
            new SessionStateBeerViewModel { Name = "Duvel", ImageName = "duvel" },
            new SessionStateBeerViewModel { Name = "Grimbergen", ImageName = "grimbergen" },
            new SessionStateBeerViewModel { Name = "La Chouffe", ImageName = "lachouffe" },
            new SessionStateBeerViewModel { Name = "Leffe", ImageName = "leffe" }
        };

        public IActionResult Index()
        {
            var vm = new SessionStateIndexViewModel();
            vm.Beers = AllBeers;
            vm.ShoppingCart = new List<SessionStateBeerViewModel>();

            string serializedBeers = HttpContext.Session.GetString(STATEKEY);
            if (serializedBeers != null)
            {
                vm.ShoppingCart = JsonConvert.DeserializeObject<List<SessionStateBeerViewModel>>(serializedBeers);
            }
            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult AddBeer(SessionStateIndexViewModel model)
        {
            //get the selected beer from form
            SessionStateBeerViewModel selectedBeer = AllBeers.FirstOrDefault(b => b.Name == model.SelectedBeerName);
            if(selectedBeer == null)
            {
                return RedirectToAction("Index");
            }

            //retrieve serialized collection from session
            string serializedBeers = HttpContext.Session.GetString(STATEKEY);
            List<SessionStateBeerViewModel> beersInShoppingList = new List<SessionStateBeerViewModel>();
            if(serializedBeers != null)
            {
                //deserialize JSON string to it's original form
                beersInShoppingList = JsonConvert.DeserializeObject<List<SessionStateBeerViewModel>>(serializedBeers);
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