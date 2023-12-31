﻿namespace CountriesTestApp.Models;

public class CountryDetailsModel
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
    public string[] Languages { get; set; } = Array.Empty<string>();
}