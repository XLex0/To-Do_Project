using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = System.Threading.Tasks.Task;
using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CapaNegocioPro
{
    public class Inventario
    {
        private List<Task> inventarioTareas {get; set;}
        private List<Category> inventarioCategorias { get; set; }

        private CapaAccesoBD.Models.Usuario user { get; set; } 


        public List<Category> UpdateCategory(CapaAccesoBD.Models.Usuario user)
        {

            var categoriesToReturn = new List<Category>() { new Category(0, "default")};

            try
            {
                var categories = Context.GetInstance().GetDbContext()
                                         .Categorylabels
                                         .Where(x => x.Iduser == user.Iduser)
                                         .ToList();

                foreach (var cat in categories)
                {
                    var category = new Category(cat.Idlabel, cat.Name);

                    if (!string.IsNullOrEmpty(cat.Description))
                    {
                        category.setDescription(cat.Description);
                    }

                    categoriesToReturn.Add(category);
                }

                return categoriesToReturn;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Cargando Categorias : {e}");
            }

            return categoriesToReturn;
        }

        public List<Task> UpdateTask(CapaAccesoBD.Models.Usuario user)
        {
            var tasksToReturn = new List<Task>();   

            try
            {
                var tasks = Context.GetInstance().GetDbContext()
                                      .Tasks
                                      .Where(x => x.Iduser == user.Iduser)
                                      .ToList();
                foreach(var task in tasks)
                {
                    var Task = new Task(task.Idtask, task.Description, task.Creationdate.ToString());
                   
                    if (task.Enddate != null)
                    {
                        Task.setEndDate(task.Enddate.ToString());
                    }

                    if (task.Priority != null)
                    {
                        Task.setPriority(task.Priority);
                    }

                    if (task.Asignations != null && task.Asignations.Count() > 0)
                    {
                        foreach (var asign in task.Asignations)
                        {
                                var categoria= asign.IdlabelNavigation.Name,

                                if(categoria != null)
                            {
                                Task.setCategory(categoria);
                            }
                        }
                    }
                }


            }
            catch (Exception e) {
                Console.WriteLine($"Error Cargando Tareas {e}");
            }



            return new List<Task>();
        }
        public void reloadTask(CapaAccesoBD.Models.Usuario user)
        {
            this.inventarioTareas = UpdateTask(user);
        }

        public void reloadCategory(CapaAccesoBD.Models.Usuario user)
        {
            this.inventarioCategorias = UpdateCategory(user);
        }
        public Inventario(string username, string password)
        {
           var user = Context.GetInstance().GetDbContext().Usuarios.FirstOrDefault(x => x.Username == "username");
 
            if ( user!=null && password == user.Password)
            {

                this.user = user;
                try
                {
                    // En el caso de que sea un login exitoso, vamos a cargar las Categorias
                    inventarioCategorias = UpdateCategory(user);
                    inventarioTareas = UpdateTask(user);

                }catch (Exception e) { Console.WriteLine($"Error al ingresar Datos Base: {e}"); }

            }
        }


     
    }
}
