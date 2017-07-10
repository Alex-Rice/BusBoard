using System.Web.Mvc;
using BusBoard.Api;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BusInfo(PostcodeSelection selection)
        {
            var apiOutput = ApiClass.MakeApiCalls(selection.Postcode);
            var info = new BusInfo(selection.Postcode, apiOutput);
            return View(info);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Information about this site";

            return View();
        }

        public ActionResult BusTimes(PostcodeSelection selection)
        {
            var apiOutput = ApiClass.MakeApiCalls(selection.Postcode);
            var info = new BusInfo(selection.Postcode, apiOutput);
            return PartialView(info);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us!";

            return View();
        }
    }
}