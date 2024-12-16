using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using CapaAccesoBD.Models;

namespace CapaNegocioPro
{
    public  class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {


            var dbContext = Context.GetInstance().GetDbContext();

            var tasks = await dbContext.Tasks.ToListAsync();





            var usuario = dbContext.Usuarios.FirstOrDefault(x => x.Username == "usuario1");
              



            Console.WriteLine($"hola {usuario.Password}");
            //        // Imprimir los detalles de cada tarea
            foreach (var task in tasks)
                    {
                       Console.WriteLine($"ID: {task.Idtask}, Description: {task.Description}, Priority: {task.Priority}, fecha:{task.Creationdate}");
                   }


        }
    }

    }

