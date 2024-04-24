using CoreCourse.StateMgmt.Web.ViewModels.TempData;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace CoreCourse.StateMgmt.Web.Controllers
{
    public class TempDataController : Controller
    {
        public IActionResult Index(bool? keep)
        {
            var indexVm = new IndexViewModel { KeepUntilRemoved = keep ?? false };
            return View(indexVm);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                //safety: encode all html
                model.MessageToEcho = HtmlEncoder.Default.Encode(model.MessageToEcho);
                //add br tags where newlines are found
                model.MessageToEcho = model.MessageToEcho.Replace("&#xA;", "<br />");     

                //add message to TempData
                TempData["MessageToEcho"] = model.MessageToEcho;
                //redirect
                return RedirectToAction("Index", new { keep = model.KeepUntilRemoved });
            }
            else
            {
                return View(model);
            }
            
        }
    }
}