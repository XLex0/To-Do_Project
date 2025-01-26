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
using System.Reflection.Metadata;


namespace CapaNegocioPro
{
    public class Inventario
    {
        private List<Task> inventarioTareas { get; set; }
        private List<Category> inventarioCategorias { get; set; }

        private CapaAccesoBD.Models.Usuario user { get; set; }

        private ProductoCreador creador { get; set; }




        public Inventario(CapaAccesoBD.Models.Usuario user)
        {
            try
            {
                this.user = user;

                creador = new TaskCreador();  
                inventarioTareas = creador.ObtenerProductos(user).Cast<Task>().ToList(); 
                creador = new CategoryCreador();
                inventarioCategorias = creador.ObtenerProductos(user).Cast<Category>().ToList();

    

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ingresar Datos Base: {e}");
            }
        }

      

        // remove task
        public bool selectRemoveTask(int i)
        {
            foreach (var item in inventarioTareas)
            {
                if (item.getIdTask() == i)
                {
                    return item.EliminarProducto();
                }
            }
            return false;
        }

        // remover categoria de un usuario 
        public bool selectRemoveCategory(int i)
        {
            foreach (var item in inventarioCategorias)
            {
                if (item.getIdLabel() == i)
                {
                    return item.EliminarProducto();
                }
            }
            return false;
        }


        // añadir tarea a un usuario 
        public bool selectAddTask(string description, string priority, string? endDate = null)
        {
            try
            {
                creador = new TaskCreador();
                var task = creador.crearProducto(description, priority, endDate) as Task;

                return task.CargarProducto(this.user.Iduser);
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        // añadir categoria de un usuario 
        public bool selectAddCategory(string name, string? description = null)
        {
            try
            {
                creador = new CategoryCreador();
               
                var category = creador.crearProducto(name,description) as Category;

                return category.CargarProducto(this.user.Iduser);
            }
            catch (Exception ex)
            {
                return false;
            }
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


        public bool chechTask(int idTask)
        {
            try
            {
                foreach (var item in inventarioTareas)
                {
                    if (item.getIdTask() == idTask)
                    {

                        item.Completado();  
                        return true;
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


        public bool AñadirCategoriaTarea(string name, int indice)
        {
            try
            {
                var tarea = inventarioTareas.FirstOrDefault(item => item.getIdTask() == indice);

                if (tarea != null)
                {
                    if (tarea.AddedCategoryToTask(name, this.user.Iduser))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool QuitarCategoriaTarea(string name, int indice)
        {
            try
            {
                var tarea = inventarioTareas.FirstOrDefault(item => item.getIdTask() == indice);

                if (tarea != null)
                {
                    if (tarea.removeCategoryTask(name))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
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
        public void filterUncompleted()
        {

            this.inventarioTareas = inventarioTareas
           .Where(item => item.getState())
           .ToList();

        }













        // json get de Tareas y Categoria
       public object ObtenerJsonInventario(string tipoInventario)
{
    switch (tipoInventario.ToLower())
    {
        case "tareas":
            if (inventarioTareas != null && inventarioTareas.Any())
            {
                return inventarioTareas.Select(item => item.RetornarJson()).ToList();
            }
            return new { mensaje = "No hay tareas en el inventario." };

        case "categorias":
            if (inventarioCategorias != null && inventarioCategorias.Any())
            {
                return inventarioCategorias.Select(item => item.RetornarJson()).ToList();
            }
            return new { mensaje = "No hay categorías en el inventario." };

        default:
            return new { mensaje = "Tipo de inventario no válido." };
    }
}



    }







}

