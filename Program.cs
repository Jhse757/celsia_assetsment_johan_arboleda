using celsia_assetsment_johan_arboleda.App.Interfaces;
using celsia_assetsment_johan_arboleda.App.Services;
using celsia_assetsment_johan_arboleda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar el DbContext con la cadena de conexión
var connectonString = builder.Configuration.GetConnectionString("MySqlConnection");
if (string.IsNullOrEmpty(connectonString))
{
    throw new InvalidOperationException("La conexión con la base de datos no esta configurada");
}

builder.Services.AddDbContext<ManagementContext>(options =>
options.UseMySql(connectonString, ServerVersion.AutoDetect(connectonString)));

// Configurar los repositorios
builder.Services.AddScoped<IExcel, ExcelService>();
//builder.Services.AddScoped<ITransaction, TransactionService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
