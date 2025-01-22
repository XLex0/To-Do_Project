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
using System.Text.Json;
using System.Configuration;

namespace CapaNegocioPro
{
    public class Inventario
    {
        private List<Task> inventarioTareas { get; set; }
        private List<Category> inventarioCategorias { get; set; }

        private CapaAccesoBD.Models.Usuario user { get; set; }


        // se encarga de cargar la lista de las categorias del usuario
        private List<Category> UpdateCategory(CapaAccesoBD.Models.Usuario user)
        {

            var categoriesToReturn = new List<Category>() { new Category(0, "main") };

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

        // se encarga de cargar la lista de las tareas del usuario
        private List<Task> UpdateTask(CapaAccesoBD.Models.Usuario user)
        {
            var tasksToReturn = new List<Task>();
            var dbacces = Context.GetInstance().GetDbContext();

            try
            {
                var tasks = dbacces.Tasks
                             .Where(x => x.Iduser == user.Iduser)
                             .ToList();


                tasksToReturn = tasks.Select(task =>
                {
                    var taskObj = new Task(task.Idtask, task.Description, task.Creationdate.ToString(), task.Estado);

                    if (task.Enddate != null)
                    {
                        taskObj.setEndDate(task.Enddate.ToString());
                    }

                    if (task.Priority != null)
                    {
                        taskObj.setPriority(task.Priority);
                    }

                    // Obtener categorías directamente asociadas a la tarea
                    var categories = dbacces.Asignations
                                            .Where(asig => asig.Idtask == task.Idtask)
                                            .SelectMany(asig => dbacces.Categorylabels.Where(c => c.Idlabel == asig.Idlabel))
                                            .Select(c => c.Name)
                                            .ToList();

                    // Asignar todas las categorías a la tarea
                    categories.ForEach(categoryName => taskObj.setCategory(categoryName));

                    return taskObj;
                }).ToList();

                return tasksToReturn;
            }
            catch (Exception e) {
                Console.WriteLine($"Error Cargando Tareas {e}");
            }
            return new List<Task>();
        }

        //carga el inventario
        public Inventario(CapaAccesoBD.Models.Usuario user)
        {
            try
            {
                this.user = user;
                inventarioCategorias = UpdateCategory(user);
                inventarioTareas = UpdateTask(user);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ingresar Datos Base: {e}");
            }
        }

      


        public bool selectRemoveTask(int i)
        {
            foreach (var item in inventarioTareas)
            {
                if (item.getIdTask() == i)
                {
                    return item.RemoveTask();
                }
            }
            return false;
        }

        // añadir tarea a un usuario 
        public bool selectAddTask(string description, string priority, string? endDate = null)
        {

            return Task.CreateTask(description, priority, this.user.Iduser, endDate);


        }


        // remover categoria de un usuario 
        public bool selectRemoveCategory(int i)
        {
            foreach (var item in inventarioCategorias)
            {
                if (item.getIdLabel() == i)
                {
                    return Category.RemoveCategory(i);
                }
            }
            return false;
        }

        // añadir categoria de un usuario 
        public bool selectAddCategory(string name, int idUser, string? description = null)
        {
            return Category.CreateCategory(name, this.user.Iduser, description);
        }

        public bool SetInfoTask(string option, int idTask,  string content)
        {
            try
            {
                foreach (var item in inventarioTareas)
                {
                    if (item.getIdTask() == idTask)
                    {
                        switch (option.ToLower())
                        {
                            case "priority":

                                if (content == "High" || content == "Medium" || content == "Low")
                                {
                                    item.setPriority(content); 
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case "date":
                                if (DateTime.TryParse(content, out DateTime newEndDate))
                                {
                                    item.setEndDate(newEndDate.ToString()); 
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                        }
                    }
                }
                return false;
            
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear la tarea: {e}");
                return false;
            }
        }

        public bool AddedCategoryToTask(string name, int taskId, int userId)
        {

            try
            {
                // Buscamo la tarea
                var task = inventarioTareas.FirstOrDefault(t => t.getIdTask() == taskId);
                if (task == null)
                {
                    return false;
                }

                // Buscar o agregar la categoría
                var category = inventarioCategorias.FirstOrDefault(c => c.getName() == name);
                if (category == null)
                {
                    if (!selectAddCategory(name, userId))
                    {
                        Console.WriteLine($"No se pudo agregar la categoría '{name}'.");
                        return false;
                    }

                    category = inventarioCategorias.FirstOrDefault(c => c.getName() == name);
                    if (category == null)
                    {
                        Console.WriteLine($"La categoría '{name}' no se encontró después de intentar agregarla.");
                        return false;
                    }
                }

                // Realizar la asignación
                var db = Context.GetInstance().GetDbContext();
                var asignacion = new CapaAccesoBD.Models.Asignation
                {
                    Idlabel = category.getIdLabel(),
                    Idtask = taskId
                };

                db.Asignations.Add(asignacion);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la categoría a la tarea: {ex.Message}");
                return false;
            }
        }

        //quitar asignacion
        public bool removeCategoryTask(int i, int j)
        {
            try
            {

                var category = inventarioCategorias.FirstOrDefault(c => c.getIdLabel() == i);
                var task = inventarioTareas.FirstOrDefault(t => t.getIdTask() == j);


                if (category == null || task == null)
                {
                    return false;
                }


                var db = Context.GetInstance().GetDbContext();
                var asignacion = db.Asignations.FirstOrDefault(a => a.Idlabel == i && a.Idtask == j);


                if (asignacion == null)
                {
                    return false;
                }


                db.Asignations.Remove(asignacion);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine($"Error al eliminar la asignación: {e}");


                return false;
            }
        }


        // filtros 
        public void filterPriority(string priority)
        {

            this.inventarioTareas = inventarioTareas
           .Where(item => item.getPriority() == priority)
           .ToList();
        }

        public void filterCategory(string category)
        {

            this.inventarioTareas = inventarioTareas
           .Where(item => item.getCategory().Contains(category))
           .ToList();

        }

        public void filterCompleted()
        {

            this.inventarioTareas = inventarioTareas
           .Where(item => !item.getState())
           .ToList();

        }
        public void filterActual()
        {

            this.inventarioTareas = inventarioTareas
           .Where(item => item.getState())
           .ToList();

        }













        // json get de Tareas y Categoria
        public object ObtenerJsonDeTareas()
        {
            if (inventarioTareas != null && inventarioTareas.Any())
            {
                var listaDeTareas = inventarioTareas.Select(item => item.getTaskJson()).ToList();
                return listaDeTareas;
            }
            else
            {
                return new { mensaje = "No hay tareas en el inventario." };
            }
        }

        public object ObtenerJsonDeCategory()
        {
            if (inventarioCategorias != null && inventarioCategorias.Any())
            {
                var listadeCategorias = inventarioCategorias.Select(item => item.getCategoryJson()).ToList();
                return listadeCategorias;
            }
            else
            {
                return new { mensaje = "No hay categorias en el inventario." };
            }
        }


    }







}

