using HtmlAgilityPack;
using Maplestory_Patch_Logs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Maplestory_Patch_Logs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(Models.HomeModel home)
        {
            HomeModel Home = new HomeModel();
            var Head = new System.Text.StringBuilder();
            var Body = new System.Text.StringBuilder();
            var Bottom = new System.Text.StringBuilder();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(home.Html);

            int i = 0;

            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("(./ul/li)"))
            {
                int ii = 0;
                i++;
                HtmlNode titleElement = node.SelectSingleNode("(//ul/li[" + i + "]//strong)");


                foreach (HtmlNode update in htmlDoc.DocumentNode.SelectNodes("(//ul/li[" + i + "]/ul/li)"))
                {
                    int iii = 0;
                    ii++;

                    HtmlNode anchorElement = update.SelectSingleNode("(//ul/li[" + i + "]/ul/li[" + ii + "]//a)");

                    Body.AppendLine("     - " + titleElement.InnerText + ": " + anchorElement.InnerText);

                }
            }

            Head.AppendLine("<details>");
            Head.AppendLine("     <summary>");
            Head.AppendLine("            " + home.Version + " (" + home.Date + ")");
            Head.AppendLine("     </summary>" + System.Environment.NewLine);

            Bottom.AppendLine("     ");
            Bottom.AppendLine("</details>");
            Bottom.AppendLine("");

            Home.Github = Head.ToString() + Body.ToString() + Bottom.ToString();
            ViewBag.home = Home;

            return View("Result");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}