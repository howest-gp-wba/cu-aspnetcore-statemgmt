using CoreCourse.StateMgmt.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreCourse.StateMgmt.Web.Controllers
{
    public class CookiesController : Controller
    {
        const string COOKIENAME = "TheCookieCookie";

        List<CookiesBiscuitViewModel> AllBiscuits = new List<CookiesBiscuitViewModel> {
            new CookiesBiscuitViewModel { Name = "Almond Thins", ImageName = "almondthins" },
            new CookiesBiscuitViewModel { Name = "Butter Crisp", ImageName = "buttercrisp" },
            new CookiesBiscuitViewModel { Name = "Coconut Biscuit", ImageName = "coconutbiscuit" },
            new CookiesBiscuitViewModel { Name = "Lace Biscuit", ImageName = "lacebiscuit" },
            new CookiesBiscuitViewModel { Name = "Speculoos", ImageName = "speculoos" },
        };

        public IActionResult Index()
        {
            //check if a cookie with this name exists
            if (Request.Cookies.ContainsKey(COOKIENAME))
            {
                string cookieImage = Request.Cookies[COOKIENAME]; //get cookie value

                //check if this biscuit is still known to us
                var biscuit = AllBiscuits.FirstOrDefault(c => c.ImageName == cookieImage);
                if (biscuit != null) return View("ShowCookie", biscuit);
            }

            //in all other cases, show the biscuit list
            var vm = new CookiesIndexViewModel();
            vm.Biscuits = AllBiscuits;
            return View("PickCookie", vm);
        }

        [HttpPost]
        public IActionResult SaveCookie(CookiesIndexViewModel model)
        {
            //send user back to Index if he didn't pick a valid cookie from the list
            if(!AllBiscuits.Select(c => c.ImageName).Contains(model.SelectedBiscuitImage)){
                return RedirectToAction("Index");
            }

            //set cookie options
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,                //javascript can't touch this.
                SameSite = SameSiteMode.Strict  //protect against XSRF attacks
            };
            if (model.IsPersistent) //set cookie to be auto-removed in 1 year from now
                cookieOptions.Expires = DateTimeOffset.Now.AddYears(1);

            //add the cookie
            Response.Cookies.Append(COOKIENAME, model.SelectedBiscuitImage, cookieOptions);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCookie()
        {
            Response.Cookies.Delete(COOKIENAME);
            return RedirectToAction("Index");
        }
    }
}