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
using System.Runtime.CompilerServices;

namespace CapaNegocioPro
{
    public  class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {


            var login = Usuario.login("usuario1", "password1");

            var user = Usuario.cargar(1);



            Console.WriteLine(
            user.getInventario().ObtenerJsonDeTareas());

            //user.getInventario().selectRemoveTask(0);

        }
    }

    }

