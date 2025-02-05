using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trySupa.Models;

public class HomeController : Controller
{
    private readonly SupabaseService _supabaseService;

    public HomeController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            await _supabaseService.InitializeAsync();

            Console.WriteLine("Fetching department data from Supabase...");
            var data = await _supabaseService.GetDeptDataAsync(); // ✅ Fetch DeptModel data

            Console.WriteLine($"Data fetched: {JsonConvert.SerializeObject(data, Formatting.Indented)}");

            return View(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in HomeController: {ex.Message}");
            return View(new List<DeptModel>());
        }
    }


}
