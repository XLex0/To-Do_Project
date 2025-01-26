using System;
using CapaAccesoBD.Models;
using System.Linq;
using System.Threading.Tasks;
using CapaNegocioPro;
using Usuario = CapaNegocioPro.Usuario;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        try
        {
            // Obtén la instancia única del Context
            var contextInstance = Context.GetInstance();

            // Obtén el DbContext de la instancia
            var dbContext = contextInstance.GetDbContext();

            // Opción para registrar un usuario
            Console.WriteLine("¿Deseas registrar un nuevo usuario? (si/no)");
            string registroRespuesta = Console.ReadLine();

            if (registroRespuesta.ToLower() == "si")
            {
                Console.Write("Introduce el nombre de usuario: ");
                string username = Console.ReadLine();
                Console.Write("Introduce la contraseña: ");
                string password = Console.ReadLine();
                Console.Write("Introduce el correo electrónico: ");
                string email = Console.ReadLine();

                var registroResultado = Usuario.register(username, password, email);
                Console.WriteLine($"Resultado del registro: {registroResultado}");
            }

            // Opción para iniciar sesión
            Console.WriteLine("\n¿Deseas iniciar sesión? (si/no)");
            string loginRespuesta = Console.ReadLine();

            if (loginRespuesta.ToLower() == "si")
            {
                Console.Write("Introduce el nombre de usuario para iniciar sesión: ");
                string loginUsername = Console.ReadLine();
                Console.Write("Introduce la contraseña: ");
                string loginPassword = Console.ReadLine();

                int userId = Usuario.login(loginUsername, loginPassword);
                if (userId != -1)
                {
                    Console.WriteLine($"Bienvenido, usuario con ID: {userId}.");
                }
                else
                {
                    Console.WriteLine("Error en las credenciales.");
                }
            }

            // Opción para gestionar categorías de productos
            Console.WriteLine("\n¿Quieres gestionar categorías de productos? (si/no)");
            string categoriaRespuesta = Console.ReadLine();

            if (categoriaRespuesta.ToLower() == "si")
            {
                // Crear nueva categoría
                Console.Write("Introduce el nombre de la categoría: ");
                string categoryName = Console.ReadLine();
                Console.Write("Introduce la descripción de la categoría (opcional): ");
                string categoryDescription = Console.ReadLine();

                // Supongamos que el usuario tiene un ID 1 (esto depende de tu lógica de inicio de sesión)
                int userIdParaCategoria = 1; // Este ID debería provenir del login o la sesión del usuario

                Category category = new Category(categoryName, categoryDescription);
                bool categoriaCreada = category.CargarProducto(userIdParaCategoria);

                if (categoriaCreada)
                {
                    Console.WriteLine("Categoría creada exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al crear la categoría.");
                }

                // Eliminar una categoría
                Console.WriteLine("\n¿Quieres eliminar una categoría? (si/no)");
                string eliminarCategoriaRespuesta = Console.ReadLine();

                if (eliminarCategoriaRespuesta.ToLower() == "si")
                {
                    Console.Write("Introduce el ID de la categoría a eliminar: ");
                    int categoryIdEliminar = int.Parse(Console.ReadLine());

                    // Crear la categoría con el ID para eliminarla
                    Category categoryEliminar = new Category(categoryIdEliminar, "", userIdParaCategoria, "");

                    bool categoriaEliminada = categoryEliminar.EliminarProducto();

                    if (categoriaEliminada)
                    {
                        Console.WriteLine("Categoría eliminada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Error al eliminar la categoría.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
