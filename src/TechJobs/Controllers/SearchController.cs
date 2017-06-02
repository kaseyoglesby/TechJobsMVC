using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;
using System.Net;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(string error)
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            ViewBag.error = error;
            return View();
        }

        // TODO #1 - Create a Results action method to process search request and display results
        [HttpPost]
        public IActionResult Index(string searchType, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                string error = "Please enter a valid search term";
                return Redirect("/Search/Index?error=" + error);
            }

            ViewBag.columns = ListController.columnChoices;
            ViewBag.searchType = searchType;
            ViewBag.searchTerm = WebUtility.HtmlEncode(searchTerm);
            // ViewBag.prettySearchTerm = ListController.columnChoices[searchTerm];

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            if (searchType == "all") 
            {
                jobs = JobData.FindByValue(searchTerm);
            }
            else
            {
                jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
            }
            
            ViewBag.jobs = jobs;

            return View("Index");
        }
    }
}
