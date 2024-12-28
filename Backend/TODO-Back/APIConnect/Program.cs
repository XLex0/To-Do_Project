using CapaNegocioPro;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

int JWT_ID(string token)
{
    try
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

  
        var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
      if(int.TryParse(userId, out int id))
        {
            return id;
        }
    }catch(Exception e)
    { 
    }
    return -1;
}


var builder = WebApplication.CreateBuilder(args);

string key = "U7xuzS$qaa_Xw7n8nW7t9JHwhk1B9GMV9ZF=E1m:cyN";

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var signingCredential = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signKey,
    };

});

var app = builder.Build();

/*
 * POST /register
 * PAra crear un nuevo usuario debo enviar
 *      {
 *      "Username":"nombre",
 *      "Password":"password",
 *      "Email": "email"
 *      }
 */
app.MapPost("/register", (APIConnect.Modelos.UserRegister user) =>
{
   var response = Usuario.register(user.Username, user.Password, user.Email);
    return Results.Json(response);
});

/*
 * POST /login
 * EL login debe recibir un json del tipo 
 * "UserLogin":
 *      {
 *      "Username":"nombre",
 *      "Password":"password"
 *      }
 *      
 *      "usuario1", "password1"
 *      
 *  retorna un token, que me sirve para hacer varias transacciones 
 */
app.MapPost("/login", (APIConnect.Modelos.UserLogin login) =>
{
    // Simulamos la validaci�n del usuario
    var cod = Usuario.login(login.Username, login.Password);

    if (cod > 0)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", cod.ToString()) 
            }),
            Expires = DateTime.UtcNow.AddHours(8), // El token expirar� en 3 horas
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

     
        var response = new
        {
            status = 200,
            message = "Inicio de sesi�n exitoso",
            token = jwtToken 
        };
        return Results.Json(response);
    }
    else
    {
        // Respuesta en caso de error
        var response = new
        {
            status = 400,
            message = "Inicio de sesi�n fallido. Usuario o contrase�a incorrectos."
        };
        return Results.Json(response);
    }
});

/*
 * GET /test:
 * Se envia un get con Auht el token, con este verificamos que el token sea v�lido
 */
app.MapGet("/test", (HttpContext context)=>{

    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    var userId = JWT_ID(token);

    if (userId > 0)
    {
        return Results.Json(new { status = 200, message = "Token v�lido", userId });
    }
    else
    {
        return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
    };
});



// Zona de tarea ---------------------------------------CRUD
/*
 * [Requiere toke AUTHENTICATION]
 *GET /task:
 *Devuelve un Json con informaci�n de las tareas, se puede agregar filtros para 
 *devolver listas de tareas que cumplan ciertos requisitos
 *
 */

app.MapGet("/task/{filter?}/{type?}", (HttpContext context, string? filter=null, string? type=null) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);
        if (userId > 0)
        {
            var user = Usuario.cargar(userId);

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(type))
            {
                switch (filter.ToLower())
                {
                    case "priority":
                        user.getInventario().filterPriority(type);
                        break;

                    case "category":
                        user.getInventario().filterCategory(type);
                        break;

                    default:
                        return Results.Json(new { status = 400, message = "Filtro inv�lido. Usa 'priority' o 'category'." });
                }
            }

            var json = user.getInventario().ObtenerJsonDeTareas();
            return Results.Json(new { status = 200, data = json });
        }
        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
        };
    }catch(Exception e)
    {
        return Results.Json(new { status = 500, message = "Error accediendo Inventario" });
    }


}).RequireAuthorization();

/*
 * POST: /task
 * Creamos una tarea Con un json de este tipo, endDate es opcional
 * {
  "Description": "Estudiar para el examen final",
  "Priority": "High",
  "EndDate": "2024-12-31"
}

 */

app.MapPost("/task", (HttpContext context, APIConnect.Modelos.Task task ) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);

        if (userId > 0)
        {
            var user = Usuario.cargar(userId);


            if( user.getInventario().selectAddTask(task.Description, task.Priority, task.EndDate?? null))
            {
                return Results.Json(new { status = 200, data = "Tarea creada con exito" });
            }
            else
            {
                return Results.Json(new { status = 400, data = "Tarea NO creada" });
            }

        }

        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
        };
    }
    catch (Exception e)
    {
        return Results.Json(new { status = 500, message = "Error accediendo Inventario" });
    }

}).RequireAuthorization();



app.MapPatch("/task/priority", (HttpContext context, APIConnect.Modelos.Priority pri) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);

        if (userId > 0)
        {
            var user = Usuario.cargar(userId);


            if(user.getInventario().SetInfoTask(pri.Option, pri.IdTask, pri.Content))
            {
                return Results.Json(new { status = 200, message = "Actualizacion correct" });
            }
            else
            {
                return Results.Json(new { status = 200, message = "Actualizacion fallida" });
            }

        }

        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
        };
    }
    catch (Exception e)
    {
        return Results.Json(new { status = 500, message = "Error accediendo Inventario" });
    }


}).RequireAuthorization(); 

app.MapPatch("task/category/{option}", (HttpContext context, string option, APIConnect.Modelos.Asignacion asig) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);

        if (userId > 0)
        {
            var user = Usuario.cargar(userId);
            bool sucess = false;
            string actionMessage = string.Empty;

            switch (option.ToLower())
            {
                 case "add":
                   sucess=  user.getInventario().addedCategoryTask(asig.IdCategory, asig.IdTask);
                    actionMessage = "agrego";
                    break;

                case "remove":
                    sucess= user.getInventario().removeCategoryTask(asig.IdCategory, asig.IdTask);
                    actionMessage = "quito";

                    break;

                default:
                    return Results.Json(new { status = 400, message = "Filtro inv�lido. Usa 'add' o 'remove'." });
            }
            if (sucess)
            {
                return Results.Json(new { status = 200, message = $"Se {actionMessage} la categor�a " });
            }
            else
            {
                return Results.Json(new { status = 401, message = $"No se {actionMessage} la categor�a " });
            }
        }
        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
        };
    }
    catch (Exception e)
    {
        return Results.Json(new { status = 500, message = "NO se pudo acceder a inventario" });
    }

}).RequireAuthorization();


app.MapDelete("/task/{id}", (HttpContext context, int id) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);

        if (userId > 0)
        {

            var user = Usuario.cargar(userId);
            if (user.getInventario().selectRemoveTask(id))
            {

                return Results.Json(new { status = 200, message = "Tarea eliminada correctamente" });
            }
            else
            {
                return Results.Json(new { status = 200, message = "Tarea no encontrada" });
            }

        }
        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
        }
    }
    catch (Exception e)
    {
        // Loguea el error
        Console.WriteLine($"Error: {e.Message}");
        return Results.Json(new { status = 500, message = "Error al eliminar la categor�a" });
    }

}).RequireAuthorization();


//ZONA CATEGORIAS _________________________________________ CR_D
/*
 *[Requiere toke AUTHENTICATION]
 * GET /categories
 * retorna una lista de categorias asociadas al usuario
 * en formato Json
 */
app.MapGet("/categories", (HttpContext context) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);
        if(userId > 0)
        {
            var user = Usuario.cargar(userId);

            var json = user.getInventario().ObtenerJsonDeCategory();
            return Results.Json(new { status = 200, data = json });
        }
        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });

        }
    }
    catch (Exception e)
    {
        return Results.Json(new { status = 500, message = "Error accediendo Inventario" });
    }


}).RequireAuthorization();

/*
 * [Requiere toke AUTHENTICATION]
 * POST /categories
 * Creacion de nuevas categorias para el usuario
 */
app.MapPost("/categories",(HttpContext context, APIConnect.Modelos.Category category) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);
        if ( userId > 0)
        {
            var user = Usuario.cargar(userId);

            if (user.getInventario().selectAddCategory(category.name, userId, category.description ?? ""))
            {
                return Results.Json(new { status = 200, data = "Categoria agregada correctamente" });
            }
            else
            {
                return Results.Json(new { status = 401, data = "Categoria no creada, error en JSON" });
            }
        }
        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });

        }
    }
    catch (Exception e)
    {
        return Results.Json(new { status = 500, message = "Error accediendo Inventario" });
    }

}).RequireAuthorization();

app.MapDelete("/categories/{id}", (HttpContext context, int id) =>
{
    try
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = JWT_ID(token);

        if (userId > 0)
        {

            var user = Usuario.cargar(userId) ;
            if (user.getInventario().selectRemoveCategory(id))
            {

                return Results.Json(new { status = 200, message = "Categor�a eliminada correctamente" });
            }
            else
            {
                return Results.Json(new { status = 200, message = "Categor�a no encontrada" });
            }
    
        }
        else
        {
            return Results.Json(new { status = 401, message = "Token inv�lido o expirado" });
        }
    }
    catch (Exception e)
    {
        // Loguea el error
        Console.WriteLine($"Error: {e.Message}");
        return Results.Json(new { status = 500, message = "Error al eliminar la categor�a" });
    }

}).RequireAuthorization();

app.Run();
