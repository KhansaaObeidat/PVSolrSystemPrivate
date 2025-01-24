using Microsoft.AspNetCore.Mvc;

namespace PVSolrSystemPrivate.Controllers
{
    public class ConsumptionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            return View();
        }

        private List<string> GetMonths()
        {
            return new List<string>
            {
                "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
            };
        }
    }
}