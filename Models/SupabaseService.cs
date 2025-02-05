using Newtonsoft.Json;
using Supabase;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SupabaseService
{
    private readonly Supabase.Client _supabase;

    public SupabaseService(string supabaseUrl, string supabaseKey)
    {
        _supabase = new Supabase.Client(supabaseUrl, supabaseKey, new SupabaseOptions());
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _supabase.InitializeAsync();
            Console.WriteLine("Supabase initialized successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Supabase: {ex.Message}");
        }
    }

    public async Task<List<DeptModel>> GetDeptDataAsync()
    {
        try
        {
            Console.WriteLine("Fetching department data from Supabase...");

            var response = await _supabase.From<DeptModel>().Select("*").Get();

            Console.WriteLine($"Supabase Response: {JsonConvert.SerializeObject(response.Models, Formatting.Indented)}");

            return response.Models;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching department data: {ex.Message}");
            return new List<DeptModel>();
        }
    }


}
