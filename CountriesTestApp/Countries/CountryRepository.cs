using CountriesTestApp.Common;
using System.Xml.Linq;
using Newtonsoft.Json;
using CountriesTestApp.Countries.Dtos;

namespace CountriesTestApp.Countries;

public class CountryRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public CountryRepository(HttpClient httpClient, AppConfig appConfig)
    {
        _httpClient = httpClient;
        _apiUrl = appConfig.ApiConfig.Url;
    }

    public async Task<CountryDto[]> GetAll()
    {
        var result = await _httpClient.GetAsync(_apiUrl + "all?fields=name,capital");

        if (!result.IsSuccessStatusCode) throw new Exception("Ошибка при получении стран");

        var content = await result.Content.ReadAsStringAsync();
        var countryList = JsonConvert.DeserializeObject<CountryDto[]>(content);

        return countryList ?? throw new Exception("Ошибка при десериализации стран");
    }

    public async Task<CountryDto[]> SearchCountriesByName(string name)
    {
        var result = await _httpClient.GetAsync(_apiUrl + $"/name/{name}");

        if (!result.IsSuccessStatusCode) throw new Exception("Ошибка при получении стран");

        var content = await result.Content.ReadAsStringAsync();
        var countryList = JsonConvert.DeserializeObject<CountryDto[]>(content);

        return countryList ?? throw new Exception("Ошибка при десериализации стран");
    }

    public async Task<CountryDetailsDto> GetCountryByName(string officialName)
    {
        var result = await _httpClient.GetAsync(_apiUrl + $"name/{officialName}?fullText=true");

        if (!result.IsSuccessStatusCode) throw new Exception("Ошибка при получении страны");

        var content = await result.Content.ReadAsStringAsync();
        var country = JsonConvert.DeserializeObject<CountryDetailsDto[]>(content);

        return country?.FirstOrDefault() ?? throw new Exception("Ошибка при десериализации страны");
    }
}