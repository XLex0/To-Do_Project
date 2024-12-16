using CapaAccesoBD.Models;  // Asegúrate de usar tu clase de modelo
using CapaNegocioPro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class Context
{
    private static Context _instance = null;
    private readonly BaseToDoContext _context;


    // Constructor privado para el patrón Singleton
    private Context()
    {
        var options = new DbContextOptionsBuilder<BaseToDoContext>()
            .UseNpgsql($"Host={zConfig.Default.Host};Database={zConfig.Default.Database};Username={zConfig.Default.Username};Password={zConfig.Default.Password}")
            .Options;

        _context = new BaseToDoContext(options);
    }


    // Método para obtener la instancia única
    public static Context GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Context();
        }
        return _instance;
    }

    // Método para obtener el DbContext
    public BaseToDoContext GetDbContext()
    {
        return _context;
    }

}

