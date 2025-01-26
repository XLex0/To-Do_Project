using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Task = System.Threading.Tasks.Task;
using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Json;


namespace CapaNegocioPro
{
    public class Task:IProducto
    {
        private int idtask { get; set; }
        private string description { get; set; }
        private string priority { get; set; }
        private string startDate { get; set; }
        private string endDate { get; set; }
        private bool state { get; set; }
        private List<string> category { get; set; }

       



        public Task(int id, string des, string star, bool estado, string priority)
        {
            this.idtask = id;
            this.description = des;
            this.priority = priority;
            this.startDate = star;
            this.category = new List<string> { "main" };
            this.state = estado;

        }



        public Task(string description, string priority, string? endDate)
        {
            this.description = description;
            this.priority = priority;
            this.state = true;
            this.endDate = endDate ?? null;

        }

        public bool CargarProducto (int idUser)
        {
            try
            {
                var dbContext = Context.GetInstance().GetDbContext();

                var nuevaTarea = new CapaAccesoBD.Models.Task
                {
                    Description = this.description,
                    Priority = this.priority,
                    Creationdate = DateOnly.FromDateTime(DateTime.Now),
                    Iduser = idUser,
                    Enddate = string.IsNullOrEmpty(this.endDate) ? null : DateOnly.Parse(this.endDate),
                    Estado = this.state
                };


                dbContext.Tasks.Add(nuevaTarea);
                dbContext.SaveChanges();
                return true; 

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear la tarea: {e}");
            }
            return false;
        }

           
        


        public bool EliminarProducto()
        {
            try
            {

                var dbContext = Context.GetInstance().GetDbContext();


                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask);


                var asignaciones = dbContext.Asignations.Where(a => a.Idtask == this.idtask).ToList();

                if (asignaciones.Any())
                {
                    dbContext.Asignations.RemoveRange(asignaciones);
                }
                if (tarea != null)
                {
                    dbContext.Tasks.Remove(tarea);
                }

                dbContext.SaveChanges();

                Console.WriteLine($"Tarea con ID {this.idtask} eliminada exitosamente, junto con sus asignaciones.");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar la tarea: {e.Message}");
                return false;
            }
        }



        public void setEndDate(string endDate)
        {
            try
            {
     
                var dbContext = Context.GetInstance().GetDbContext();

                // Buscar la tarea en la base de datos usando su ID
                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask);
                if (tarea == null)
                {
                    return;
                }

                tarea.Enddate = string.IsNullOrEmpty(endDate) ? null : DateOnly.Parse(endDate);

                dbContext.SaveChanges();

                this.endDate = endDate;

                Console.WriteLine("Fecha de finalización actualizada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la fecha de finalización: {ex.Message}");
            }
        }


        public void setPriority(string priority)
        {
            try
            {
                var dbContext = Context.GetInstance().GetDbContext();

                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask);
                if (tarea == null)
                {
                    return;
                }

                tarea.Priority = priority;
                dbContext.SaveChanges();

                this.priority = priority;

                Console.WriteLine("Prioridad actualizada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la prioridad: {ex.Message}");
            }
        }



        public bool AddedCategoryToTask(string name, int userId)
        {
            try
            {
                var dbContext = Context.GetInstance().GetDbContext();

                var task = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask && t.Iduser == userId);
                if (task == null)
                {
                    Console.WriteLine($"La tarea con ID {this.idtask} para el usuario {userId} no se encontró.");
                    return false;
                }

                var category = dbContext.Categorylabels.FirstOrDefault(c => c.Name == name && c.Iduser == userId);

                // Si no existe, crear la categoría
                if (category == null)
                {

                    var categ=new Category(name);
                    categ.CargarProducto(userId);

                    category = dbContext.Categorylabels.FirstOrDefault(c => c.Name == name && c.Iduser == userId);
                    if (category == null)
                    {
                        Console.WriteLine($"Error: La categoría '{name}' no se pudo crear o encontrar.");
                        return false;
                    }

                }

                var asignacionExistente = dbContext.Asignations
                    .FirstOrDefault(a => a.Idlabel == category.Idlabel && a.Idtask == this.idtask);

                if (asignacionExistente != null)
                {
                    Console.WriteLine("La categoría ya está asignada a esta tarea.");
                    return false;
                }

                var asignacion = new CapaAccesoBD.Models.Asignation
                {
                    Idlabel = category.Idlabel,
                    Idtask = this.idtask
                };

                dbContext.Asignations.Add(asignacion);
                dbContext.SaveChanges();

                Console.WriteLine($"La categoría '{name}' se ha asignado correctamente a la tarea con ID {this.idtask}.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la categoría a la tarea: {ex.Message}");
                return false;
            }
        }

        public bool removeCategoryTask(string name)
        {
            try
            {
                var db = Context.GetInstance().GetDbContext();

                var category = db.Categorylabels.FirstOrDefault(c => c.Name == name);
                
                var asignation= db.Asignations.FirstOrDefault(m => m.Idlabel == category.Idlabel && m.Idtask == this.idtask);

                if (category == null || asignation == null)
                {
                    return false;
                }
                db.Asignations.Remove(asignation);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine($"Error al eliminar la asignación: {e}");


                return false;
            }
        }


        public void setCategory(string category) { this.category.Add(category); }

        public void Completado()
        {
            try
            {
                // Obtener la instancia del contexto
                var dbContext = Context.GetInstance().GetDbContext();

                // Buscar la tarea en la base de datos
                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask);
                if (tarea == null)
                {
                    Console.WriteLine("No se encontró la tarea especificada.");
                    return;
                }

                // Actualizar el estado de la tarea a false
                tarea.Estado = false;
                dbContext.SaveChanges();


                this.state = false;

                Console.WriteLine("Tarea completada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al completar la tarea: {ex.Message}");
            }
        }


        public int getIdTask() { return this.idtask; }
        public string getPriority() { return this.priority; }
        public List<string> getCategory() { return this.category; }
        public bool getState(){return this.state;}


        public object RetornarJson()
        {
            var taskData = new
            {
                idtask = this.idtask,
                description = this.description,
                priority = this.priority,
                startDate = this.startDate,
                endDate = string.IsNullOrEmpty(this.endDate) ? null : this.endDate,
                category = this.category,
                estado = this.state
            };

            return taskData;
        }
    }
}

