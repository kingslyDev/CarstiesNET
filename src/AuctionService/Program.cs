using AuctionService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Menambahkan layanan untuk controller
builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Menambahkan layanan otorisasi (jika diperlukan)
builder.Services.AddAuthorization();

var app = builder.Build();

// Menambahkan routing untuk menangani request
app.UseRouting();

// Middleware untuk otorisasi (jika diperlukan)
app.UseAuthorization();

// Memetakan controller jika Anda menggunakan controller berbasis API
app.MapControllers();

app.Run();
