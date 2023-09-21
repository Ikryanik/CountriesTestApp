using Newtonsoft.Json;

namespace CountriesTestApp.Countries.Dtos;

public class CountryDto
{
    [JsonProperty("name")]
    public NameDto NameDto { get; set; } = null!;

    [JsonProperty("capital")]
    public string[] Capital { get; set; } = Array.Empty<string>();
}