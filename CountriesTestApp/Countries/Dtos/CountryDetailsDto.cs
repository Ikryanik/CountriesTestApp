using Newtonsoft.Json;

namespace CountriesTestApp.Countries.Dtos;

public class CountryDetailsDto
{
    [JsonProperty("name")]
    public NameDto NameDto { get; set; } = null!;

    [JsonProperty("region")]
    public string Region { get; set; } = string.Empty;

    [JsonProperty("capital")]
    public string[] Capital { get; set; } = Array.Empty<string>();

    [JsonProperty("languages")]
    public Dictionary<string, string> Languages { get; set; } = new();
}