using Newtonsoft.Json;

namespace CountriesTestApp.Countries.Dtos;

public class NameDto
{
    [JsonProperty("official")]
    public string Official { get; set; } = string.Empty;
}