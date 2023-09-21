using CountriesTestApp.Countries.Dtos;
using CountriesTestApp.Models;

namespace CountriesTestApp.Countries;

public class CountryService
{
    private readonly CountryRepository _countryRepository;

    public CountryService(CountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<CountryModel[]> GetAll()
    {
        var countries = await _countryRepository.GetAll();
        return Map(countries);
    }

    public async Task<CountryModel[]> SearchCountriesByName(string name)
    {
        var countries = await _countryRepository.SearchCountriesByName(name);
        return Map(countries);
    }

    public async Task<CountryDetailsModel> GetCountryByName(string officialName)
    {
        var countryDto = await _countryRepository.GetCountryByName(officialName);
        return Map(countryDto);
    }

    private CountryDetailsModel Map(CountryDetailsDto countryDto)
    {
        return new CountryDetailsModel
        {
            Name = countryDto.NameDto.Official,
            Languages = countryDto.Languages.Values.ToArray(),
            Capital = string.Join(", ", countryDto.Capital),
            Region = countryDto.Region
        };
    }

    private CountryModel[] Map(CountryDto[] countries)
    {
        return countries
            .Select(c => 
                new CountryModel
                {
                    Capital = string.Join(", ", c.Capital), 
                    Name = c.NameDto.Official
                })
            .ToArray();
    }
}