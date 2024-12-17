using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = System.Threading.Tasks.Task;
using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CapaNegocioPro
{
    public class Inventario
    {
        private List<Task> inventarioTareas {get; set;}
        private List<Category> inventarioCategorias { get; set; }

        private CapaAccesoBD.Models.Usuario user { get; set; }

        public bool login {  get; set; }

        private List<Category> UpdateCategory(CapaAccesoBD.Models.Usuario user)
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

        private List<Task> UpdateTask(CapaAccesoBD.Models.Usuario user)
        {
            var tasksToReturn = new List<Task>();
            var dbacces = Context.GetInstance().GetDbContext();

            try
            {
                var tasks = dbacces.Tasks
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

                    var asignaciones = dbacces.Asignations.Where(x => x.Idtask == task.Idtask).ToList();
                    foreach (var asig in asignaciones) {
                        var categories = dbacces.Categorylabels.Where(x => x.Idlabel == asig.Idlabel).ToList();

                        foreach (var category in categories) {

                            Task.setCategory(category.Name);
                         }
                    }

                    tasksToReturn.Add(Task);
                }

                return tasksToReturn;
            }
            catch (Exception e) {
                Console.WriteLine($"Error Cargando Tareas {e}");
            }


            return new List<Task>();
        }

        public void selectRemoveTask(int i)
        {
            if (this.login) {
                var task = inventarioTareas[i];
                task.RemoveTask(task.getIdTask());
                reloadTask();
            }
        }
        public void selectAddTask(string description, string priority,  string? endDate = null)
        {
            if (this.login)
            {
                Task.CreateTask(description,  priority, this.user.Iduser, endDate );
                reloadTask();
            }
        }
        public Inventario(string username, string password)
        {
           var user = Context.GetInstance().GetDbContext().Usuarios.FirstOrDefault(x => x.Username == username);
 
            if ( user!=null && password == user.Password)
            {

                this.user = user;
                try
                {
                    // En el caso de que sea un login exitoso, vamos a cargar las Categorias
                    inventarioCategorias = UpdateCategory(user);
                    inventarioTareas = UpdateTask(user);

                }catch (Exception e) { Console.WriteLine($"Error al ingresar Datos Base: {e}"); }

                this.login = true;
            }
            else
            {
                this.login = false;
            }
        }



        private void reloadTask()
        {
            this.inventarioTareas = UpdateTask(this.user);
        }

        private void reloadCategory()
        {
            this.inventarioCategorias = UpdateCategory(this.user);
        }
        public void imprimirTask()
        {
            if (inventarioTareas != null)
            {
                foreach (var item in this.inventarioTareas)
                {
                    item.Imprimir();
                }
            }
            else
            {
                Console.WriteLine("NO ingreso");
            }
        }
     
    }
}
