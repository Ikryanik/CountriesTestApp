using CountriesTestApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CountriesTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["CapitalSort"] = sortOrder == "capital" ? "capitalDesc" : "capital";
            ViewData["CurrentSearch"] = searchString;

            var countries = new List<CountryModel>
            {
                new CountryModel
                {
                    Name = "Russia",
                    Capital = "Moscow"
                },
                new CountryModel
                {
                    Name = "Norway",
                    Capital = "Oslo"
                },
                new CountryModel
                {
                    Name = "South Korea",
                    Capital = "Seoul"
                }
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                countries = countries.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            countries = sortOrder switch
            {
                "nameDesc" => countries.OrderByDescending(с => с.Name).ToList(),
                "capitalDesc" => countries.OrderByDescending(с => с.Capital).ToList(),
                "capital" => countries.OrderBy(с => с.Capital).ToList(),
                _ => countries.OrderBy(с => с.Name).ToList()
            };
            return View(countries);
        }

        public IActionResult Details(string countryName)
        {
            var countries = new List<CountryDetailsModel>
            {
                new CountryDetailsModel
                {
                    Name = "Russia",
                    Region = "Eurasia",
                    Capital = "Moscow",
                    Languages = new Dictionary<string, string>
                    {
                        {"rus", "Russian"},
                        {"eng", "English"}
                    }
                },
                new CountryDetailsModel
                {
                    Name = "Norway",
                    Region = "Europe",
                    Capital = "Oslo",
                    Languages = new Dictionary<string, string>
                    {
                        {"spa", "Spanish"},
                        {"eng", "English"}
                    }
                },
                new CountryDetailsModel
                {
                    Name = "South Korea",
                    Region = "Asia",
                    Capital = "Seoul",
                    Languages = new Dictionary<string, string>
                    {
                        {"kor", "Korean"}
                    }
                }
            };
            var country = countries.FirstOrDefault(c => c.Name == countryName);
            return View(country);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}