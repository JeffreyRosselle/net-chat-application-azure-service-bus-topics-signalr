using System.Web.Mvc;

namespace Website.Controllers
{
    public class ServiceBusController : Controller
    {

        public ServiceBusController()
        {
        }

        [HttpGet]
        public ActionResult Topic()
        {
            return View();
        }

    }
}