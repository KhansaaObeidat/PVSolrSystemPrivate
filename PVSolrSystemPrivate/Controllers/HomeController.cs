using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVSolrSystemPrivate.Data;
using PVSolrSystemPrivate.Models;

namespace PVSolrSystemPrivate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // to find total customers
            var totalOfCustomers = await _context.customers.CountAsync();
            // var totalCapacity= await _context.systems.SumAsync(s=>s.Capacity);
            var totalCapacity = 0;
            ViewBag.TotalCustomers = totalOfCustomers;
            ViewBag.TotalCapacity = totalCapacity;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
