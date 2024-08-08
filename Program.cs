using Microsoft.EntityFrameworkCore;
using SuperNumberProject.Components;
using SuperNumberProject.Models;
using SuperNumberProject.Services;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Agrega servicios al contenedor
builder.Services.AddDbContext<SuperNumberdbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers(); // Añade servicios de MVC y API
builder.Services.AddRazorPages(); // Añade soporte para Razor Pages, si es necesario

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<RegistroServices>();
builder.Services.AddScoped<SuperNumberServices>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
