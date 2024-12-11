using CapaAccesoBD.Models;  // Asegúrate de usar tu clase de modelo
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

class Program
{
    static async Task Main(string[] args)
    {
        // Configura el contexto de la base de datos directamente en el Main
        var options = new DbContextOptionsBuilder<BaseToDoContext>()
            .UseNpgsql("Host=localhost;Database=Base_TO_DO;Username=postgres;Password=A20x21M76v")
            .Options;

        // Crea una instancia de BaseToDoContext con las opciones de configuración
        using (var context = new BaseToDoContext(options))
        {
            // Crea una instancia de TodoService
            var todoService = new TodoService(context);

            // Llama al método ListTasksAsync para obtener las tareas y mostrarlas
            await todoService.ListTasksAsync();
        }
    }
}

public class TodoService
{
    private readonly BaseToDoContext _context;

    public TodoService(BaseToDoContext context)
    {
        _context = context;
    }

    // Cambié el nombre de 'Task' a 'MyTask' para evitar la ambigüedad
    public async Task ListTasksAsync()
    {
        // Obtener todas las tareas desde la base de datos
        var tasks = await _context.Tasks.ToListAsync();

        // Imprimir los detalles de cada tarea
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Idtask}, Description: {task.Description}, Priority: {task.Priority}");
        }
    }
}

