using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using W24_TP2_Maxime_Caron.Models;
using W24_TP2_Maxime_Caron.ViewModels;


namespace W24_TP2_Maxime_Caron.Controllers
{
    public class HomeController : Controller
    {      
        private readonly ILogger<HomeController> _logger;
        private readonly forumContext _context;

        public HomeController(ILogger<HomeController> logger, forumContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var HomeCats = _context.Categories.Select(c => 
            new HomeCat
            {
                CatId = c.CatId,
                CatNom = c.CatNom,
                CatDescription = c.CatDescription,
                CatActif = c.CatActif,
                TopSujet = c.Sujets.OrderByDescending(p => p.SujetId).Take(3)
            });

            return View(HomeCats);
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