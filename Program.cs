using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// Register SupabaseService with DI
builder.Services.AddSingleton<SupabaseService>(provider =>
{
    var supabaseUrl = configuration["Supabase:Url"];
    var supabaseKey = configuration["Supabase:Key"];

    if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
    {
        throw new Exception("Supabase credentials are missing from appsettings.json");
    }

    return new SupabaseService(supabaseUrl, supabaseKey);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
});

app.Run();
