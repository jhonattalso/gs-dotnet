using Microsoft.EntityFrameworkCore;
using SyncMe.Data; // Importante

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// CONFIGURAÇÃO DO ORACLE
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
