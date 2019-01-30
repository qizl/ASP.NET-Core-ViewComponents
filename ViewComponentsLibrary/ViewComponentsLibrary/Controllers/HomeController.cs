using EnjoyCodes.ViewComponentsLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnjoyCodes.ViewComponentsLibrary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int? page, string keyWords)
        {
            ViewBag.CurrentPage = page ?? 1;
            ViewBag.PageSize = 10;
            ViewBag.TotalItemCount = 500;
            ViewBag.KeyWords = keyWords;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
