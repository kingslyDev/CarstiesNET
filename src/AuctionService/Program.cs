var builder = WebApplication.CreateBuilder(args);

// Menambahkan layanan untuk controller
builder.Services.AddControllers();

// Menambahkan layanan otorisasi (jika diperlukan)
builder.Services.AddAuthorization();

var app = builder.Build();

// Menambahkan routing untuk menangani request
app.UseRouting();

// Middleware untuk otorisasi (jika diperlukan)
app.UseAuthorization();

// Memetakan controller jika Anda menggunakan controller berbasis API
app.MapControllers();

// Menambahkan rute minimal API untuk "/weatherforecast"
var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
