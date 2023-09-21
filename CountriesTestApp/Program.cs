using CountriesTestApp.Common;
using CountriesTestApp.Countries;

var builder = WebApplication.CreateBuilder(args);

var appConfig = new AppConfig(args);
builder.Services.AddSingleton(appConfig);
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<CountryRepository>();
builder.Services.AddHttpClient<CountryRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{countryName?}");

app.Run();
