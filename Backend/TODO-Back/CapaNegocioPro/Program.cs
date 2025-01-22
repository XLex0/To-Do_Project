using System;
using CapaAccesoBD.Models;
using Task = System.Threading.Tasks.Task;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Obtén la instancia única del Context
            var contextInstance = Context.GetInstance();
            
            var dbContext = contextInstance.GetDbContext();


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
