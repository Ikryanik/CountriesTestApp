using CountriesTestApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CountriesTestApp.Countries;

namespace CountriesTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CountryService _countryService;

        public HomeController(ILogger<HomeController> logger, CountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["CapitalSort"] = sortOrder == "capital" ? "capitalDesc" : "capital";
            ViewData["CurrentSearch"] = searchString;

            CountryModel[] countries;

            if (!string.IsNullOrEmpty(searchString))
            {
                countries = await _countryService.SearchCountriesByName(searchString);
            }
            else
            {
                countries = await _countryService.GetAll();
            }

            countries = SortCountries(countries, sortOrder);

            return View(countries);
        }

        public async Task<IActionResult> Details(string countryName)
        {
            var country = await _countryService.GetCountryByName(countryName);

            return View(country);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static CountryModel[] SortCountries(CountryModel[] countries, string sortOrder)
        {
            return sortOrder switch
            {
                "nameDesc" => countries.OrderByDescending(с => с.Name).ToArray(),
                "capitalDesc" => countries.OrderByDescending(с => с.Capital).ToArray(),
                "capital" => countries.OrderBy(с => с.Capital).ToArray(),
                _ => countries.OrderBy(с => с.Name).ToArray()
            };
        }
    }
}